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
        public static string SendAddress { get { return AlertConfig.SendAddress; } set { AlertConfig.SendAddress = value; } }
        public static string SendPassword { get { return AlertConfig.SendPassword; } set { AlertConfig.SendPassword = value; } }
        public static string ReceiveAddress { get { return AlertConfig.ReceiveAddress; } set { AlertConfig.ReceiveAddress = value; } }
        public static string ReceiveType { get { return AlertConfig.ReceiveType; } set { AlertConfig.ReceiveType = value; } }
        public static List<AlertDataSource> Alerts { get { return AlertConfig.Alerts; } set { AlertConfig.Alerts = value; } }
        #endregion

        #region Private Variables
        private static AlertConfig AlertConfig { get; set; }
        private const string FILENAME = "Alert.config";
        #endregion

        #region Manage
        public static void Create()
        {
            File.WriteAllText(FILENAME, JsonConvert.SerializeObject(new AlertConfig()));
            Load();
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

        public static void Load()
        {
            if (File.Exists(FILENAME))
                AlertConfig = UserConfigService.Encrypted 
                    ? JsonConvert.DeserializeObject<AlertConfig>(EncryptionService.AesDecryptString(File.ReadAllText(FILENAME))) 
                    : JsonConvert.DeserializeObject<AlertConfig>(File.ReadAllText(FILENAME));
            else
                Create();
        }

        public static void Delete()
        {
            if (File.Exists(FILENAME))
                File.Delete(FILENAME);
        }

        public static void Remove(List<AlertDataSource> alerts)
        {
            foreach (AlertDataSource alert in alerts)
                Alerts.RemoveAll(c => c.Coin.Equals(alert.Coin) && c.Operator.Equals(alert.Operator) && c.Price.Equals(alert.Price));

            Save();
        }
        #endregion

        #region Methods
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
