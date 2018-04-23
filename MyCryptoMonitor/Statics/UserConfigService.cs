using MyCryptoMonitor.Configs;
using Newtonsoft.Json;
using System.IO;

namespace MyCryptoMonitor.Statics
{
    public class UserConfigService
    {
        #region Public Variables
        public static string Currency { get { return UserConfig.Currency; } set { UserConfig.Currency = value.ToUpper(); Save(); MainService.SetCurrencySymbol(); } }
        public static bool Encrypted { get { return UserConfig.Encrypted; } set { UserConfig.Encrypted = value; Save(); } }
        public static string StartupPortfolio { get { return UserConfig.StartupPortfolio; } set { UserConfig.StartupPortfolio = value; Save(); } }
        public static string EncryptionCheck { get { return UserConfig.EncryptionCheck; } set { UserConfig.EncryptionCheck = value; Save(); } }
        #endregion

        #region Private Variables
        private static UserConfig UserConfig { get; set; }
        private const string FILENAME = "User.config";
        #endregion

        #region Manage
        public static void Create()
        {
            File.WriteAllText(FILENAME, JsonConvert.SerializeObject(new UserConfig()));
            Load();
        }

        private static void Save()
        {
            File.WriteAllText(FILENAME, JsonConvert.SerializeObject(UserConfig));
        }

        public static void Load()
        {
            if (File.Exists(FILENAME))
                UserConfig = JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText(FILENAME));
            else
                Create();
        }

        public static void Delete()
        {
            if (File.Exists(FILENAME))
                File.Delete(FILENAME);

            Create();
        }
        #endregion
    }
}
