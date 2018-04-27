namespace MyCryptoMonitor.Configs
{
    public class UserConfig
    {
        public string Currency { get; set; } = "USD";
        public string StartupPortfolio { get; set; } = string.Empty;
        public string Theme { get; set; } = "Default";
        public bool Encrypted { get; set; } = false;
        public string EncryptionCheck { get; set; } = string.Empty;
    }
}