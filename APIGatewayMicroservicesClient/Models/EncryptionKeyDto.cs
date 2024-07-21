namespace LicenseService.Models
{
    public class EncryptionKeyDto
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
