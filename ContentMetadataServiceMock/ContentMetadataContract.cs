using APIGatewayCoreUtilities.CommonExceptions;
using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;
using AuthorizationServiceAPI.DataMappers;
using ContentMetadataServiceMock.Persistance;
using ContentMetadataServiceMock.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace ContentMetadataServiceMock
{
    public class ContentMetadataContract : IContentMetadataContract
    {
        private readonly ILogger<ContentMetadataContract> _logger;
        private readonly ContentMetadataDatabaseContext _context;

        private const int UNIQUE_CONSTRAINT_VIOLATION_ERROR_NUMBER = 2627;
        private const int PRIMARY_KEY_VIOLATION_ERROR_NUMBER = 2601;

        public ContentMetadataContract(
            ILogger<ContentMetadataContract> logger,
            ContentMetadataDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        async Task<Content> IContentMetadataContract.GetContentMetadataByIdAsync(Guid contentId)
        {
            var content = await _context.Contents
                .Include(c => c.Comments)
                .Include(c => c.LicenseRules)
                .FirstOrDefaultAsync(c => c.ContentId == contentId);
                
            if (content == null)
            {
                _logger.LogError($"Content with id: {contentId} not found!");
                throw new NotFoundException($"Content with id: {contentId} not found!");
            }

            return content.ToContent();
        }

        public async Task<Guid> AddContentMetadataAsync(Content content)
        {
            try
            {
                await _context.Contents.AddAsync(content.ToContentData());
                await _context.SaveChangesAsync();
                return content.Uuid;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == UNIQUE_CONSTRAINT_VIOLATION_ERROR_NUMBER || sqlEx.Number == PRIMARY_KEY_VIOLATION_ERROR_NUMBER)
                    {
                        _logger.LogError($"A conflict accourd while updating the database: {sqlEx.Message}");
                        throw new ConflictException($"A conflict accourd while updating the database: {sqlEx.Message}");
                    }
                }

                throw;
            }
        }

        public async Task DeleteContentMetadataAsync(Guid contentId)
        {
            var content = await _context.Contents.FindAsync(contentId);
            if (content == null)
            {
                throw new NotFoundException($"Content with id: {contentId} not found!");
            }

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
        }

        public async Task EditContentMetadataAsync(Guid contentId, Content updatedContent)
        {
            if (updatedContent.LicenseRules == null && !updatedContent.LicenseRules.Any())
            {
                _logger.LogError("Bad input. License rules should not be empty.");
                throw new Exception("Bad input. License rules should not be empty.");
            }

            var content = await _context.Contents
                .Include(e => e.LicenseRules)
                .FirstOrDefaultAsync(e => e.ContentId == contentId);

            if (content == null)
            {
                throw new NotFoundException($"Content with id: {contentId} not found!");
            }

            content.Title = updatedContent.Title;
            content.Description = updatedContent.Description;
            content.Duration = updatedContent.Duration;

            //TODO: Update in spearate methods
            content.ContentStatus = updatedContent.ContentStatus;
            content.ImageStatus = updatedContent.ImageStatus;
            //-------------------------------------------------------

            var updatedLicenseRules = updatedContent.LicenseRules.Select(c => c.ToLicenseRulesData()).ToList();
            content.LicenseRules = updatedLicenseRules;
   
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset)
        {
            //TODO: maybe filter contents in APIGatewayEntity component?
            var contentsData = new List<ContentData>();

            if (limit == 0)
            {
                contentsData = await _context.Contents
                    .Where(e => e.ContentStatus == UploadState.Success && e.ImageStatus == UploadState.Success)
                    .Skip(offset)
                    .ToListAsync();
            }
            else
            {
                contentsData = await _context.Contents
                    .Where(e => e.ContentStatus == UploadState.Success && e.ImageStatus == UploadState.Success)
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();
            }
           
            var contents = contentsData?.Select(c => c.ToContent()).ToList() ?? new List<Content>();

            return contents;
        }

        public async Task<IEnumerable<Content>> GetContentMetadataByOwnerIdAsync(Guid ownerId)
        {
            var contentsData = await _context.Contents
                .Include(c => c.LicenseRules)
                .Where(c => c.OwnerId == ownerId)
                .ToListAsync();

            if (contentsData == null)
            {
                throw new NotFoundException($"Content with owner id: {ownerId} not found!");
            }

            return contentsData.ToContents();
        }
    }
}
