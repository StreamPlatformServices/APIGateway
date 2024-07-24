namespace APIGatewayEntities.Entities
{
    public class ContentLicense
    {
        public Guid Uuid { set; get; }
        public Guid UserId { set; get; }
        public Guid FileId { set; get; }
        public Guid ContentId { set; get; } //TODO: maybe pass by function param
        public EncryptionKey? KeyData { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}