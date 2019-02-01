using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Forms;
using MyCryptoMonitor.Objects;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public class MainService
    {
        #region Public Properties

        public static string CurrencySymbol { get; private set; }
        public static bool Unsaved { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static void CheckAlerts(List<Coin> coins)
        {
            if (AlertService.Alerts.Count <= 0)
                return;

            var triggeredAlerts = new List<AlertDataSource>();

            foreach (AlertDataSource alert in AlertService.Alerts)
            {
                var coinData = coins.Where(c => c.ShortName.ExtEquals(alert.Coin) && UserConfigService.Currency.ExtEquals(alert.Currency)).FirstOrDefault();

                if (coinData == null)
                    continue;

                if ((alert.Operator == AlertService.Operators.GreaterThan && coinData.Price > alert.Price && alert.Enabled)
                    || (alert.Operator == AlertService.Operators.LessThan && coinData.Price < alert.Price && alert.Enabled))
                {
                    triggeredAlerts.Add(alert);
                    alert.Enabled = false;
                }
                else if ((alert.Operator == AlertService.Operators.GreaterThan && coinData.Price < alert.Price && !alert.Enabled)
                   || (alert.Operator == AlertService.Operators.LessThan && coinData.Price > alert.Price && !alert.Enabled))
                {
                    alert.Enabled = true;
                }
                else if (alert.Operator == AlertService.Operators.Both && alert.LastOperator == AlertService.Operators.LessThan && coinData.Price > alert.Price && alert.Enabled)
                {
                    alert.LastOperator = AlertService.Operators.GreaterThan;
                    triggeredAlerts.Add(alert);
                }
                else if (alert.Operator == AlertService.Operators.Both && alert.LastOperator == AlertService.Operators.GreaterThan && coinData.Price < alert.Price && alert.Enabled)
                {
                    alert.LastOperator = AlertService.Operators.LessThan;
                    triggeredAlerts.Add(alert);
                }
                else if (alert.Operator == AlertService.Operators.Both && alert.LastOperator == null && coinData.Price > alert.Price && alert.Enabled)
                {
                    alert.LastOperator = AlertService.Operators.GreaterThan;
                }
                else if (alert.Operator == AlertService.Operators.Both && alert.LastOperator == null && coinData.Price < alert.Price && alert.Enabled)
                {
                    alert.LastOperator = AlertService.Operators.LessThan;
                }
            }

            AlertService.SendAlert(triggeredAlerts);
        }

        public static bool ConfirmReset()
        {
            return MessageBox.Show($"This will delete all saved files (portfolios, alerts, etc) and remove encryption. Do you want to continue?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes;
        }

        public static void Reset()
        {
            UserConfigService.Delete();
            AlertService.Delete();
            PortfolioService.DeleteAll();
        }

        public static void SetCurrencySymbol()
        {
            switch (UserConfigService.Currency)
            {
                case "BTC": CurrencySymbol = "\u0E3F"; break;
                case "ETH": CurrencySymbol = "\u039E"; break;
                case "ALL": CurrencySymbol = "\u004c"; break;
                case "AFN": CurrencySymbol = "\u060b"; break;
                case "AED": CurrencySymbol = "\u062f" + "\u002e" + "\u0625"; break;
                case "ARS": CurrencySymbol = "\u0024"; break;
                case "AWG": CurrencySymbol = "\u0192"; break;
                case "AUD": CurrencySymbol = "\u0024"; break;
                case "AZN": CurrencySymbol = "\u20bc"; break;
                case "BSD": CurrencySymbol = "\u0024"; break;
                case "BBD": CurrencySymbol = "\u0024"; break;
                case "BDT": CurrencySymbol = "\u09F3"; break;
                case "BHD": CurrencySymbol = "دب‏"; break;
                case "BIF": CurrencySymbol = "\u07A8"; break;
                case "BYR": CurrencySymbol = "\uA037"; break;
                case "BYN": CurrencySymbol = "\u0042"; break;
                case "BZD": CurrencySymbol = "\u0042"; break;
                case "BMD": CurrencySymbol = "\u0024"; break;
                case "BOB": CurrencySymbol = "\u0024"; break;
                case "BAM": CurrencySymbol = "\u004b"; break;
                case "BWP": CurrencySymbol = "\u0050"; break;
                case "BGN": CurrencySymbol = "\u043b"; break;
                case "BRL": CurrencySymbol = "\u0052"; break;
                case "BND": CurrencySymbol = "\u0024"; break;
                case "KHR": CurrencySymbol = "\u17db"; break;
                case "CAD": CurrencySymbol = "\u0024"; break;
                case "KYD": CurrencySymbol = "\u0024"; break;
                case "CLP": CurrencySymbol = "\u0024"; break;
                case "CNY": CurrencySymbol = "\u00a5"; break;
                case "COP": CurrencySymbol = "\u0024"; break;
                case "CRC": CurrencySymbol = "\u20a1"; break;
                case "HRK": CurrencySymbol = "\u006b"; break;
                case "CUP": CurrencySymbol = "\u20b1"; break;
                case "CZK": CurrencySymbol = "\u004b"; break;
                case "DKK": CurrencySymbol = "\u006b"; break;
                case "DZD": CurrencySymbol = "دج‏"; break;
                case "DOP": CurrencySymbol = "\u0052"; break;
                case "XCD": CurrencySymbol = "\u0024"; break;
                case "EGP": CurrencySymbol = "\u00a3"; break;
                case "ETB": CurrencySymbol = "Br"; break;
                case "SVC": CurrencySymbol = "\u0024"; break;
                case "EUR": CurrencySymbol = "\u20ac"; break;
                case "FKP": CurrencySymbol = "\u00a3"; break;
                case "FJD": CurrencySymbol = "\u0024"; break;
                case "GHS": CurrencySymbol = "\u00a2"; break;
                case "GIP": CurrencySymbol = "\u00a3"; break;
                case "GTQ": CurrencySymbol = "\u0051"; break;
                case "GEL": CurrencySymbol = "\u16CB"; break;
                case "GGP": CurrencySymbol = "\u00a3"; break;
                case "GYD": CurrencySymbol = "\u0024"; break;
                case "HNL": CurrencySymbol = "\u004c"; break;
                case "HKD": CurrencySymbol = "\u0024"; break;
                case "HUF": CurrencySymbol = "\u0046"; break;
                case "ISK": CurrencySymbol = "\u006b"; break;
                case "INR": CurrencySymbol = "\u20B9"; break;
                case "IDR": CurrencySymbol = "\u0052"; break;
                case "IRR": CurrencySymbol = "\ufdfc"; break;
                case "IMP": CurrencySymbol = "\u00a3"; break;
                case "ILS": CurrencySymbol = "\u20aa"; break;
                case "IQD": CurrencySymbol = "دع"; break;
                case "JMD": CurrencySymbol = "\u004a"; break;
                case "JPY": CurrencySymbol = "\u00a5"; break;
                case "JEP": CurrencySymbol = "\u00a3"; break;
                case "JOD": CurrencySymbol = "دأ‏"; break;
                case "KZT": CurrencySymbol = "\u043b"; break;
                case "KPW": CurrencySymbol = "\u20a9"; break;
                case "KRW": CurrencySymbol = "\u20a9"; break;
                case "KES": CurrencySymbol = "Ksh"; break;
                case "KWD": CurrencySymbol = "دك"; break;
                case "KGS": CurrencySymbol = "\u043b"; break;
                case "LAK": CurrencySymbol = "\u20ad"; break;
                case "LBP": CurrencySymbol = "\u00a3"; break;
                case "LRD": CurrencySymbol = "\u0024"; break;
                case "MKD": CurrencySymbol = "\u0434"; break;
                case "MAD": CurrencySymbol = "دم"; break;
                case "MDL": CurrencySymbol = "MDL"; break;
                case "MGA": CurrencySymbol = "\uA234"; break;
                case "MMK": CurrencySymbol = "K"; break;
                case "MOP": CurrencySymbol = "\uA0C0"; break;
                case "MYR": CurrencySymbol = "\u0052"; break;
                case "MUR": CurrencySymbol = "\u20a8"; break;
                case "MXN": CurrencySymbol = "\u0024"; break;
                case "MNT": CurrencySymbol = "\u20ae"; break;
                case "MZN": CurrencySymbol = "\u004d"; break;
                case "NAD": CurrencySymbol = "\u0024"; break;
                case "NPR": CurrencySymbol = "\u20a8"; break;
                case "ANG": CurrencySymbol = "\u0192"; break;
                case "NZD": CurrencySymbol = "\u0024"; break;
                case "NIO": CurrencySymbol = "\u0043"; break;
                case "NGN": CurrencySymbol = "\u20a6"; break;
                case "NOK": CurrencySymbol = "\u006b"; break;
                case "OMR": CurrencySymbol = "\ufdfc"; break;
                case "PKR": CurrencySymbol = "\u20a8"; break;
                case "PAB": CurrencySymbol = "\u0042"; break;
                case "PYG": CurrencySymbol = "\u0047"; break;
                case "PEN": CurrencySymbol = "\u0053"; break;
                case "PHP": CurrencySymbol = "\u20b1"; break;
                case "PLN": CurrencySymbol = "\u007a"; break;
                case "QAR": CurrencySymbol = "\ufdfc"; break;
                case "RON": CurrencySymbol = "\u006c"; break;
                case "RUB": CurrencySymbol = "\u20bd"; break;
                case "RWF": CurrencySymbol = "FR"; break;
                case "SHP": CurrencySymbol = "\u00a3"; break;
                case "SAR": CurrencySymbol = "\ufdfc"; break;
                case "RSD": CurrencySymbol = "\u0414"; break;
                case "SCR": CurrencySymbol = "\u20a8"; break;
                case "SGD": CurrencySymbol = "\u0024"; break;
                case "SBD": CurrencySymbol = "\u0024"; break;
                case "SOS": CurrencySymbol = "\u0053"; break;
                case "ZAR": CurrencySymbol = "\u0052"; break;
                case "LKR": CurrencySymbol = "\u20a8"; break;
                case "SEK": CurrencySymbol = "\u006b"; break;
                case "CHF": CurrencySymbol = "\u0043"; break;
                case "SRD": CurrencySymbol = "\u0024"; break;
                case "SYP": CurrencySymbol = "\u00a3"; break;
                case "TWD": CurrencySymbol = "\u004e"; break;
                case "THB": CurrencySymbol = "\u00e3f"; break;
                case "TND": CurrencySymbol = "دت"; break;
                case "TOP": CurrencySymbol = "T$"; break;
                case "TTD": CurrencySymbol = "\u0054"; break;
                case "TRY": CurrencySymbol = "\u20ba"; break;
                case "TVD": CurrencySymbol = "\u0024"; break;
                case "TZS": CurrencySymbol = "TSh"; break;
                case "UAH": CurrencySymbol = "\u20b4"; break;
                case "GBP": CurrencySymbol = "\u00a3"; break;
                case "USD": CurrencySymbol = "\u0024"; break;
                case "UYU": CurrencySymbol = "\u0024"; break;
                case "UGX": CurrencySymbol = "USh"; break;
                case "UZS": CurrencySymbol = "\u043b"; break;
                case "VEF": CurrencySymbol = "\u0042"; break;
                case "VND": CurrencySymbol = "\u20ab"; break;
                case "XAF": CurrencySymbol = "FCFA"; break;
                case "XOF": CurrencySymbol = "CFA"; break;
                case "YER": CurrencySymbol = "\ufdfc"; break;
                case "ZWD": CurrencySymbol = "\u005a"; break;
                default: CurrencySymbol = string.Empty; break;
            }
        }

        public static void Startup()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

            UserConfigService.Load();

            if (UserConfigService.Encrypted)
                EncryptionService.Unlock();

            AlertService.Load();

            if (!UserConfigService.TutorialCompleted)
                using (Tutorial form = new Tutorial())
                    form.ShowDialog();
        }

        #endregion Public Methods
    }
}