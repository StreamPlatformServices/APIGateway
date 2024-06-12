namespace APIGatewayRouting.Data
{
    public enum LicenseType
    { 
        Unknown,
        Infinit,
        Limited
    }

    public enum LicenseStatus
    {
        Unknown,
        Active,
        Expired
    }

    public class ContentLicense
    {
        public Guid Uuid { set; get; }
        public Guid UserId { set; get; }
        public Guid ContentId { set; get; }
        public string Description { set; get; }
        public DateTime UploadTime { set; get; }
        public DateTime ExpirationTime { set; get; }
        public LicenseType LicenseType { set; get; }
        public LicenseStatus LicenseStatus { set; get; }
    }
}