using MyCryptoMonitor.API;
using MyCryptoMonitor.Statics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MyCryptoMonitor.Services
{
    public class ApplicationLayer
    {
        #region Private Fields

        private readonly DataService _dataService = new DataService();

        #endregion Private Fields

        #region Public Methods

        public List<string> GetCoinNames()
        {
            var response = _dataService.GetCryptoCompareCoins();
            var formattedResponse = $"[{JObject.Parse(response)["Data"]}]";
            var coins = JsonConvert.DeserializeObject<List<Dictionary<string, ApiCryptoCompareCoin>>>(formattedResponse);
            var coinNames = coins[0].Keys.ToList();

            coinNames.AddRange(UserConfigService.CustomCoins);

            return coinNames.OrderBy(x => x).ToList();
        }

        #endregion Public Methods
    }
}