using MyCryptoMonitor.Configs;
using MyCryptoMonitor.DataSources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public class AlertService
    {
        #region Private Fields

        private const string FILENAME = "Alert.config";

        #endregion Private Fields

        #region Public Enums

        public enum Operators {[Description("Greater Than")] GreaterThan, [Description("Less Than")] LessThan, [Description("Both")] Both }

        public enum Types {[Description("Email")] Email, [Description("Verizon")] Verizon, [Description("AT&T")] ATT, [Description("Sprint")] Sprint, [Description("Boost Mobile")] Boost, [Description("T-Mobile")] TMobile, [Description("US Cellular")] USCellular, [Description("Virgin Mobile")] VirginMobile }

        #endregion Public Enums

        #region Public Properties

        public static List<AlertDataSource> Alerts { get { return AlertConfig.Alerts; } set { AlertConfig.Alerts = value; } }
        public static string ReceiveAddress { get { return AlertConfig.ReceiveAddress; } set { AlertConfig.ReceiveAddress = value; } }
        public static string ReceiveType { get { return AlertConfig.ReceiveType; } set { AlertConfig.ReceiveType = value; } }
        public static string SendAddress { get { return AlertConfig.SendAddress; } set { AlertConfig.SendAddress = value; } }
        public static string SendPassword { get { return AlertConfig.SendPassword; } set { AlertConfig.SendPassword = value; } }

        #endregion Public Properties

        #region Private Properties

        private static AlertConfig AlertConfig { get; set; }

        #endregion Private Properties

        #region Public Methods

        public static void Create()
        {
            if (UserConfigService.Encrypted)
                File.WriteAllText(FILENAME, EncryptionService.AesEncryptString(JsonConvert.SerializeObject(new AlertConfig())));
            else
                File.WriteAllText(FILENAME, JsonConvert.SerializeObject(new AlertConfig()));

            Load();
        }

        public static void Delete()
        {
            if (File.Exists(FILENAME))
                File.Delete(FILENAME);
        }

        public static void Load()
        {
            if (File.Exists(FILENAME))
                AlertConfig = UserConfigService.Encrypted
                    ? JsonConvert.DeserializeObject<AlertConfig>(EncryptionService.AesDecryptString(File.ReadAllText(FILENAME)))
                    : JsonConvert.DeserializeObject<AlertConfig>(File.ReadAllText(FILENAME));
            else
                Create();

            AlertConfig.Alerts.ForEach(x => x.LastOperator = null);
        }

        public static void Save()
        {
            if (UserConfigService.Encrypted)
            {
                File.WriteAllText(FILENAME, EncryptionService.AesEncryptString(JsonConvert.SerializeObject(AlertConfig)));
            }
            else
            {
                SendAddress = string.Empty;
                SendPassword = string.Empty;
                ReceiveAddress = string.Empty;
                ReceiveType = string.Empty;
                File.WriteAllText(FILENAME, JsonConvert.SerializeObject(AlertConfig));
            }
        }

        public static void SendAlert(List<AlertDataSource> alerts)
        {
            if (alerts.Count <= 0)
                return;

            Task.Factory.StartNew(() =>
            {
                var first = alerts.FirstOrDefault();
                var op = first.LastOperator == Operators.GreaterThan ? ">" : first.Operator == Operators.GreaterThan ? ">" : "<";
                var title = $"{first.Coin} {op} {first.Price}";
                var message = string.Empty;

                foreach (var alert in alerts)
                {
                    string condition = alert.LastOperator == Operators.GreaterThan ? "greater than" : alert.Operator == Operators.GreaterThan ? "greater than" : "less than";
                    message += $"{alert.Coin} is {condition} than {alert.Price}\r\n";

                    if (UserConfigService.DeleteAlerts)
                        Alerts.Remove(alert);
                }

                MessageBox.Show(message, title);
                SendEmail(message);
                Save();
            });
        }

        #endregion Public Methods

        #region Private Methods

        private static string GetContactAddress(string address, string type)
        {
            switch ((Types)Enum.Parse(typeof(Types), type))
            {
                case Types.Email:
                    return address;

                case Types.ATT:
                    return $"{address}@txt.att.net";

                case Types.Boost:
                    return $"{address}@myboostmobile.com";

                case Types.Sprint:
                    return $"{address}@messaging.sprintpcs.com";

                case Types.Verizon:
                    return $"{address}@vtext.com";

                case Types.TMobile:
                    return $"{address}@tmomail.net";

                case Types.USCellular:
                    return $"{address}@email.uscc.net";

                case Types.VirginMobile:
                    return $"{address}@vmobl.com";

                default:
                    return string.Empty;
            }
        }

        private static void SendEmail(string alertMessage)
        {
            if (string.IsNullOrEmpty(SendAddress) || string.IsNullOrEmpty(SendPassword) || string.IsNullOrEmpty(ReceiveAddress) || string.IsNullOrEmpty(ReceiveType))
                return;

            try
            {
                var sendAddress = new MailAddress(SendAddress);
                var receiveAddress = new MailAddress(GetContactAddress(ReceiveAddress, ReceiveType));
                var fromPassword = SendPassword;
                var subject = "My Crypto Monitor Alert";
                var body = alertMessage;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sendAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(sendAddress, receiveAddress) { Subject = subject, Body = body })
                    smtp.Send(message);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ensure that 'allow less secure apps' is enabled for gmail, your email and password are correct, and that port 25 is not blocked on your network.\n\nError Message: {e.Message}", "Error sending email alert");
            }
        }

        #endregion Private Methods
    }
}