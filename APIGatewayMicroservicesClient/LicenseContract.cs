﻿using APIGatewayCoreUtilities.CommonExceptions;
using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;
using LicenseProxyAPI.DataMappers;
using LicenseProxyAPI.Helpers;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;

namespace LicenseProxyAPI
{
    public class LicenseContract : ILicenseContract
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LicenseContract> _logger;
        private readonly ILicenseTimeCalculator _licenseTimeCalculator;

        private const string LICENSE_ENDPOINT = "license";
        private const string FILE_ID_PARAM = "fileId=";
        private const string USER_ID_PARAM = "userId=";

        
        public LicenseContract(
            ILogger<LicenseContract> logger,
            HttpClient httpClient,
            ILicenseTimeCalculator licenseTimeCalculator) 
        {
            _logger = logger;
            _httpClient = httpClient;
            _licenseTimeCalculator = licenseTimeCalculator;
        }

        public async Task<ContentLicense> GetLicenseAsync(Guid userId, Guid fileId, string token)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.GetAsync($"{LICENSE_ENDPOINT}?{USER_ID_PARAM}{userId}&{FILE_ID_PARAM}{fileId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var licenseDto = JsonConvert.DeserializeObject<ContentLicenseResponseDto>(responseContent);

                    if (licenseDto == null)
                    {
                        _logger.LogError("Deserialized user object is empty.");
                        throw new Exception("User object is empty after deserialization");
                    }

                    return licenseDto.ToContentLicense();
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException(response.ReasonPhrase);
                    case HttpStatusCode.Unauthorized:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response while trying to get user data. Message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to get user data.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to get user data.");
                throw;
            }
        }
        public Task IssueLicenseAsync(ContentLicense license, string token)
        {
            //TODO: NOW!!!!!!!!!
            throw new NotImplementedException();
        }
        public Task ExtendLicenseAsync(ContentLicense license, string token)
        {
            throw new NotImplementedException();
        }

        

        public Task DeleteLicenseAsync(Guid licenseId)
        {
            throw new NotImplementedException();
        }
    }
}
