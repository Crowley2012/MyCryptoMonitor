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
        public bool DeleteAlerts { get; set; } = false;
        public bool TutorialCompleted { get; set; } = false;
        public int RefreshTime { get; set; } = 5000;
        public string SaltKey { get; set; } = "QM4436DL3A259EFXYNZEW4TCVVY5QZJG9CXFEKFW";
        public string[] CustomCoins { get; set; } = new string[] { "$$$", "MRK" };
    }
}
