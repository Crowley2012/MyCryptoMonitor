using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        public static bool ConfirmReset()
        {
            return MessageBox.Show($"This will delete all saved files (portfolios, alerts, etc) and remove encryption. Do you want to continue?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes;
        }

        public static void CheckAlerts(List<Coin> coins)
        {
            List<AlertDataSource> removeAlerts = new List<AlertDataSource>();

            if (AlertService.Alerts.Count <= 0)
                return;

            foreach (AlertDataSource alert in AlertService.Alerts)
            {
                var coinData = coins.Where(c => c.ShortName.Equals(alert.Coin) && UserConfigService.Currency.Equals(alert.Currency)).FirstOrDefault();

                if (coinData == null)
                    continue;

                if ((alert.Operator == AlertService.Operators.GreaterThan && coinData.Price > alert.Price) || (alert.Operator == AlertService.Operators.LessThan && coinData.Price < alert.Price))
                {
                    AlertService.SendAlert(alert);
                    removeAlerts.Add(alert);
                }
            }

            AlertService.Remove(removeAlerts);
        }
    }
}
