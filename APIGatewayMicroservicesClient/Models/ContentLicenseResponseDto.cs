using LicenseService.Models;
using System.Net;

namespace APIGatewayEntities.Entities
{
    public class ContentLicenseResponseDto
    {
        public Guid Uuid { get; set; }
        public Guid FileId { get; set; }
        public EncryptionKeyDto KeyData { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? MaxPlayCount { get; set; }
        public int? MaxPlaybackDuration { get; set; }
        public Guid UserId { get; set; }
    }
}