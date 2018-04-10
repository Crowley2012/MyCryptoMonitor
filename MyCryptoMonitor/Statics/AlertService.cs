using MyCryptoMonitor.Configs;
using MyCryptoMonitor.DataSources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public class AlertService
    {
        #region Public Variables
        public static AlertConfig AlertConfig { get; set; }
        #endregion

        #region Methods
        public static AlertConfig LoadAlerts()
        {
            if (UserConfigService.Encrypted)
                return LoadAlertsEncrypted();
            else
                return LoadAlertsUnencrypted();
        }

        public static AlertConfig LoadAlertsEncrypted()
        {
            return JsonConvert.DeserializeObject<AlertConfig>(Encryption.AesDecryptString(File.ReadAllText("Alerts")));
        }

        public static AlertConfig LoadAlertsUnencrypted()
        {
            AlertConfig alertConfig = JsonConvert.DeserializeObject<AlertConfig>(File.ReadAllText("Alerts"));
            alertConfig.SendAddress = string.Empty;
            alertConfig.SendPassword = string.Empty;

            return alertConfig;
        }

        public static void SaveAlerts(AlertConfig alertConfig)
        {
            //Update the cached alerts
            AlertConfig = alertConfig;

            if (UserConfigService.Encrypted)
                SaveAlertsEncrypted(alertConfig);
            else
                SaveAlertsUnencrypted(alertConfig);
        }

        public static void SaveAlertsEncrypted(AlertConfig alertConfig)
        {
            File.WriteAllText("Alerts", Encryption.AesEncryptString(JsonConvert.SerializeObject(alertConfig)));
        }

        public static void SaveAlertsUnencrypted(AlertConfig alertConfig)
        {
            alertConfig.SendAddress = string.Empty;
            alertConfig.SendPassword = string.Empty;
            File.WriteAllText("Alerts", JsonConvert.SerializeObject(alertConfig));
        }

        public static void EncryptAlerts()
        {
            if (File.Exists("Alerts"))
                File.WriteAllText("Alerts", Encryption.AesEncryptString(JsonConvert.SerializeObject(LoadAlertsUnencrypted())));
        }

        public static void DecryptAlerts()
        {
            if (File.Exists("Alerts"))
                File.WriteAllText("Alerts", JsonConvert.SerializeObject(LoadAlertsEncrypted()));
        }

        public static void RemoveAlerts(List<AlertDataSource> alerts)
        {
            AlertConfig alertConfig = LoadAlerts();

            foreach (AlertDataSource alert in alerts)
                alertConfig.Alerts.RemoveAll(c => c.Coin.Equals(alert.Coin) && c.Operator.Equals(alert.Operator) && c.Price.Equals(alert.Price));

            SaveAlerts(alertConfig);
        }

        public static string GetContactAddress(string address, string type)
        {
            switch ((Globals.Types)Enum.Parse(typeof(Globals.Types), type))
            {
                case Globals.Types.Email:
                    return address;
                case Globals.Types.ATT:
                    return $"{address}@txt.att.net";
                case Globals.Types.Boost:
                    return $"{address}@myboostmobile.com";
                case Globals.Types.Sprint:
                    return $"{address}@messaging.sprintpcs.com";
                case Globals.Types.Verizon:
                    return $"{address}@vtext.com";
                case Globals.Types.TMobile:
                    return $"{address}@tmomail.net";
                case Globals.Types.USCellular:
                    return $"{address}@email.uscc.net";
                case Globals.Types.VirginMobile:
                    return $"{address}@vmobl.com";
                default:
                    return string.Empty;
            }
        }

        public static void SendAlert(AlertDataSource alert)
        {
            string alertMessage = $"{alert.Coin} is {alert.Operator} than {alert.Price}";

            Task.Factory.StartNew(() => { MessageBox.Show(alertMessage, "Alert"); });

            Thread sendEmail = new Thread(new ParameterizedThreadStart(sendEmailThread));
            sendEmail.IsBackground = true;
            sendEmail.Start(alertMessage);
        }

        public static void sendEmailThread(object alertMessage)
        {
            try
            {
                if (!string.IsNullOrEmpty(AlertConfig.SendAddress) && !string.IsNullOrEmpty(AlertConfig.SendPassword))
                {
                    var sendAddress = new MailAddress(AlertConfig.SendAddress);
                    var receiveAddress = new MailAddress(GetContactAddress(AlertConfig.ReceiveAddress, AlertConfig.ReceiveType));
                    string fromPassword = AlertConfig.SendPassword;
                    string subject = "My Crypto Monitor Alert";
                    string body = Convert.ToString(alertMessage);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sendAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(sendAddress, receiveAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ensure that 'allow less secure apps' is enabled for gmail, your email and password are correct, and that port 25 is not blocked on your network.\n\nError Message: {e.Message}", "Error sending external alert");
            }
        }
        #endregion
    }
}
