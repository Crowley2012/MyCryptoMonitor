using MyCryptoMonitor.Configs;
using Newtonsoft.Json;
using System.IO;

namespace MyCryptoMonitor.Statics
{
    public class UserConfigService
    {
        #region Public Variables
        public static UserConfig UserConfig { get; set; }
        #endregion

        #region Methods
        public static void LoadUserConfig()
        {
            UserConfig = File.Exists("UserConfig") ? JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("UserConfig")) : new UserConfig();
            SaveUserConfig();
        }

        public static void SaveUserConfig()
        {
            File.WriteAllText("UserConfig", JsonConvert.SerializeObject(UserConfig));
        }
        #endregion
    }
}
