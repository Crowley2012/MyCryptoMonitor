using MyCryptoMonitor.Configs;
using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Objects;
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
    public static class AlertService
    {
        #region Private Fields

        private const string FILENAME = "Alert.config";

        #endregion Private Fields

        #region Public Properties

        public static List<AlertDataSource> Alerts { get { return AlertConfig.Alerts; } set { AlertConfig.Alerts = value; } }
        public static string ReceiveAddress { get { return AlertConfig.ReceiveAddress; } set { AlertConfig.ReceiveAddress = value; } }
        public static Constants.Types ReceiveType { get { return AlertConfig.ReceiveType; } set { AlertConfig.ReceiveType = value; } }
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
                ReceiveType = Constants.Types.ATT;
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
                var op = first.LastOperator == Constants.Operators.GreaterThan ? ">" : first.Operator == Constants.Operators.GreaterThan ? ">" : "<";
                var title = $"{first.Coin} {op} {first.Price}";
                var message = string.Empty;

                foreach (var alert in alerts)
                {
                    string condition = alert.LastOperator == Constants.Operators.GreaterThan ? "greater than" : alert.Operator == Constants.Operators.GreaterThan ? "greater than" : "less than";
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

        private static string GetContactAddress(string address, Constants.Types type)
        {
            switch (type)
            {
                case Constants.Types.Email:
                    return address;

                case Constants.Types.ATT:
                    return $"{address}@txt.att.net";

                case Constants.Types.Boost:
                    return $"{address}@myboostmobile.com";

                case Constants.Types.Sprint:
                    return $"{address}@messaging.sprintpcs.com";

                case Constants.Types.Verizon:
                    return $"{address}@vtext.com";

                case Constants.Types.TMobile:
                    return $"{address}@tmomail.net";

                case Constants.Types.USCellular:
                    return $"{address}@email.uscc.net";

                case Constants.Types.VirginMobile:
                    return $"{address}@vmobl.com";

                default:
                    return string.Empty;
            }
        }

        private static void SendEmail(string alertMessage)
        {
            if (string.IsNullOrEmpty(SendAddress) || string.IsNullOrEmpty(SendPassword) || string.IsNullOrEmpty(ReceiveAddress))
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