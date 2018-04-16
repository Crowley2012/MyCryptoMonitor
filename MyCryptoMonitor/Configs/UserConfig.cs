namespace MyCryptoMonitor.Configs
{
    public class UserConfig
    {
        public string Currency { get; set; } = string.Empty;
        public string StartupPortfolio { get; set; } = string.Empty;
        public bool Encrypted { get; set; } = false;
        public string EncryptionCheck { get; set; } = string.Empty;
    }
}