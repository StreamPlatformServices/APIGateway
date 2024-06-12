using APIGatewayRouting.Data;
using System.ComponentModel.DataAnnotations;

namespace APIGatewayController.Models
{
    public class ContentLicenseModel
    {
        public Guid UserId { set; get; }
        public Guid ContentId { set; get; }
        public string Description { set; get; }
        public DateTime UploadTime { set; get; }
        [Required]
        public DateTime ExpirationTime { set; get; }
        [Required]
        public LicenseType LicenseType { set; get; }
        public LicenseStatus LicenseStatus { set; get; }
    }
}