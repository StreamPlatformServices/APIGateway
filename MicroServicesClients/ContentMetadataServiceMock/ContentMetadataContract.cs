﻿using APIGatewayCoreUtilities.CommonExceptions;
using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;
using AuthorizationServiceAPI.DataMappers;
using ContentMetadataServiceMock.Persistance;
using ContentMetadataServiceMock.Persistance.Data;
using Microsoft.Data.Sqlite;
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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //TODO:* REMOVE FROM FILE ID'S FROM FILE RETENTION TABLE
                    await _context.Contents.AddAsync(content.ToContentData());
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    //TODO: Check if it was saved successfully
                    return content.Uuid;
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException is SqliteException sqliteEx)
                    {
                        if (sqliteEx.SqliteErrorCode == SQLitePCL.raw.SQLITE_CONSTRAINT)
                        {
                            _logger.LogError($"A conflict occurred while updating the database: {sqliteEx.Message}");
                            throw new ConflictException($"A conflict occurred while updating the database: {sqliteEx.Message}");
                        }
                    }

                    if (ex.InnerException is SqlException sqlEx)
                    {
                        if (sqlEx.Number == UNIQUE_CONSTRAINT_VIOLATION_ERROR_NUMBER || sqlEx.Number == PRIMARY_KEY_VIOLATION_ERROR_NUMBER)
                        {
                            _logger.LogError($"A conflict accourd while updating the database: {sqlEx.Message}");
                            throw new ConflictException($"A conflict occurred while updating the database: {sqlEx.Message}");
                        }
                    }

                    await transaction.RollbackAsync();
                    await RemoveFileAsync(content.ImageFileId);
                    await RemoveFileAsync(content.VideoFileId);

                    throw;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await RemoveFileAsync(content.ImageFileId);
                    await RemoveFileAsync(content.VideoFileId); //TODO: It will be needed, when retention will be ready??
                    throw;
                }
            }
        }

        private async Task RemoveFileAsync(Guid fileId)
        {
            //TODO:* Remove file from streamgateway/streamservice/encryptionservice
        }

        public async Task DeleteContentMetadataAsync(Guid contentId)
        {
            var content = await _context.Contents.FindAsync(contentId);
            if (content == null)
            {
                throw new NotFoundException($"Content with id: {contentId} not found!");
            }

            //TODO: Try catch
            _context.Contents.Remove(content); //TODO: How many state entries should return when successfull?
            await _context.SaveChangesAsync();
        }

        public async Task EditContentMetadataAsync(Guid contentId, Content updatedContent)
        { 
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

            var updatedLicenseRules = updatedContent.LicenseRules.Select(c => c.ToLicenseRulesData()).ToList();

            //TODO: should get rules by id and edit them. integrate with frontend (check if the solution will be efiecient and simple)
            //TODO: It is always tricky because the elements not visible in requestModel list should be removed... 
            if (content.LicenseRules != null && content.LicenseRules.Any())
            {
                _context.LicenseRules.RemoveRange(content.LicenseRules);
            }

            if (updatedLicenseRules != null && updatedLicenseRules.Any())
            {
                foreach (var licenseRule in updatedLicenseRules) 
                {
                    licenseRule.ContentId = contentId;
                    await _context.LicenseRules.AddAsync(licenseRule);
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Content>> GetAllContentsAsync(int limit, int offset)
        {
            //TODO: maybe filter contents in APIGatewayEntity component?
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
