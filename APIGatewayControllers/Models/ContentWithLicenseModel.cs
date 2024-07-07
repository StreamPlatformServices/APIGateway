﻿using APIGatewayControllers.Models.Requests.Content;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models
{
    public class ContentWithLicenseModel
    {
        [Required]
        public UploadContentRequestModel ContentModel { get; set; }

        [Required]
        public LicenseRulesModel LicenseModel { get; set; }
    }
}
