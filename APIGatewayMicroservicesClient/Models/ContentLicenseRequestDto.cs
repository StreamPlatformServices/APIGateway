using LicenseService.Models;

namespace APIGatewayEntities.Entities
{
    public class ContentLicenseRequestDto
    {
        public Guid FileId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? MaxPlayCount { get; set; }
        public int? MaxPlaybackDuration { get; set; }
        public Guid UserId { get; set; }
    }
}