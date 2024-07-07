using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;
using AuthorizationServiceAPI.DataMappers;
using ContentMetadataServiceMock.Persistance;
using ContentMetadataServiceMock.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace ContentMetadataServiceMock
{
    public class ContentMetadataContract : IContentMetadataContract
    {
        private readonly ContentMetadataDatabaseContext _context;

        public ContentMetadataContract(ContentMetadataDatabaseContext context)
        {
            _context = context;
        }

        async Task<Content> IContentMetadataContract.GetContentMetadataByIdAsync(Guid contentId)
        {   
           
            var content = await _context.Contents
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(c => c.ContentId == contentId); ;

            if (content == null)
            {
                return new Content { Uuid = Guid.Empty };
            }

            return content.ToContent();
        }

        public async Task<bool> AddContentMetadataAsync(Content content)
        {
            try
            {
                await _context.Contents.AddAsync(content.ToContentData());
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception (not implemented here)
                return false;
            }
        }

        public async Task<bool> DeleteContentMetadataAsync(Guid contentId)
        {
            try
            {
                var content = await _context.Contents.FindAsync(contentId);
                if (content == null)
                {
                    return false;
                }

                _context.Contents.Remove(content);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception (not implemented here)
                return false;
            }
        }

        public async Task<bool> EditContentMetadataAsync(Guid contentId, Content updatedContent)
        {
            try
            {
                // Pobierz zawartość i powiązane reguły licencyjne
                var content = await _context.Contents
                    .Include(e => e.LicenseRules)
                    .FirstOrDefaultAsync(e => e.ContentId == contentId);

                if (content == null)
                {
                    Console.WriteLine($"Content with ID {contentId} not found.");
                    return false;
                }

                // Aktualizuj właściwości zawartości
                content.Title = updatedContent.Title;
                content.Description = updatedContent.Description;
                //content.ReleaseDate = updatedContent.ReleaseDate;
                //content.Duration = updatedContent.Duration;

                if (updatedContent.LicenseRules != null && updatedContent.LicenseRules.Any())
                {
                    var updatedLicenseRules = updatedContent.LicenseRules.Select(c => c.ToLicenseRulesData()).ToList();
                    content.LicenseRules = updatedLicenseRules;
                }
                else
                {
                    content.LicenseRules = new List<LicenseRulesData>();
                }


                // Logowanie przed zapisaniem zmian
                Console.WriteLine($"Updating content with ID {contentId}.");

                // Zapisz zmiany w kontekście
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Logowanie wyjątku współbieżności
                Console.WriteLine("Concurrency exception: " + ex.Message);
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Content)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        if (databaseValues != null)
                        {
                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];
                                Console.WriteLine($"Property: {property.Name}, Proposed: {proposedValue}, Database: {databaseValue}");
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                //Log message
                Console.WriteLine(ex.Message); // lub inny mechanizm logowania
                return false;
            }
        }

        public async Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset)
        {
            var contentsData = new List<ContentData>();
            if (limit == 0)
            {
                contentsData = await _context.Contents
                    .Skip(offset)
                    .ToListAsync();
            }
            else
            {
                contentsData = await _context.Contents
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            }
           

            var contents = contentsData?.Select(c => c.ToContent()).ToList() ?? new List<Content>();

            return contents;
        }

        public async Task<Content> GetContentMetadataByNameAsync(string contentName)
        {
            //TODO: Include Comments
            //TODO: How to manage Not found
            var contentData = await _context.Contents
                .FirstOrDefaultAsync(c => c.Title == contentName);

            if (contentData == null)
            {
                throw new KeyNotFoundException();
            }

            return contentData.ToContent();
        }
    }
}
