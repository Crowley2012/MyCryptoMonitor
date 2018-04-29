using MyCryptoMonitor.Objects;

namespace MyCryptoMonitor.Configs
{
    public class UserConfig
    {
        public string Currency { get; set; } = "USD";
        public string StartupPortfolio { get; set; } = string.Empty;
        public Theme Theme { get; set; } = new Theme();
        public bool Encrypted { get; set; } = false;
        public string EncryptionCheck { get; set; } = string.Empty;
    }
}