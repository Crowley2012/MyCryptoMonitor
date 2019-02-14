using MyCryptoMonitor.Objects;
using System.Net;

namespace MyCryptoMonitor.Services
{
    public class DataService
    {
        #region Public Methods

        public string GetCryptoCompareCoins()
        {
            using (var webClient = new WebClient())
                return webClient.DownloadString(Addresses.API_CRYPTO_COMPARE_COINS);
        }

        #endregion Public Methods
    }
}