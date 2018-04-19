using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Objects;
using System.Collections.Generic;
using System.Linq;

namespace MyCryptoMonitor.Statics
{
    public class MainService
    {
        public static void Startup()
        {
            UserConfigService.Load();

            if (UserConfigService.Encrypted)
                EncryptionService.Unlock();

            AlertService.Load();
        }

        public static void Reset()
        {
            PortfolioService.DeleteAll();
            AlertService.Delete();
            UserConfigService.Delete();
        }

        public static void CheckAlerts(List<Coin> coins)
        {
            if (AlertService.Alerts.Count > 0)
            {
                foreach (AlertDataSource coin in AlertService.Alerts)
                {
                    var coinData = coins.Where(c => c.ShortName.Equals(coin.Coin) && UserConfigService.Currency.Equals(coin.Currency)).FirstOrDefault();

                    if (coinData == null)
                        continue;

                    if ((coin.Operator == AlertService.Operators.GreaterThan && coinData.Price > coin.Price) || (coin.Operator == AlertService.Operators.LessThan && coinData.Price < coin.Price))
                        AlertService.SendAlert(coin);
                }
            }
        }
    }
}
