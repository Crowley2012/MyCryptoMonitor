using MyCryptoMonitor.Forms;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public static class EncryptionService
    {
        #region Private Variables
        private const string SALT = "QM4436DL3A259EFXYNZEW4TCVVY5QZJG9CXFEKFW";
        private const string CHECKFILE = "Encryption";
        private const string CHECKVALUE = "Success";
        private static string _password = string.Empty;
        #endregion

        #region Methods
        public static bool CheckPassword(string password)
        {
            return AesDecryptString(File.ReadAllText(CHECKFILE), password).Equals(CHECKVALUE);
        }

        public static void Unlock()
        {
            if (!UserConfigService.Encrypted)
                return;

            using (InputPassword form = new InputPassword())
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (!CheckPassword(form.PasswordInput))
                        Unlock();

                    _password = form.PasswordInput;
                }
                else if (result == DialogResult.Abort)
                {
                    if (MessageBox.Show($"This will delete all saved files (portfolios, alerts, etc) and remove encryption. Do you want to continue?", "Forgot Password", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        Reset();
                    else
                        Unlock();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        public static void EncryptFiles(string password)
        {
            _password = password;
            UserConfigService.Encrypted = true;

            File.WriteAllText(CHECKFILE, AesEncryptString(CHECKVALUE));

            UserConfigService.Save();
            PortfolioService.EncryptPortfolios();
            AlertService.EncryptAlerts();
        }

        public static void DecryptFiles()
        {
            UserConfigService.Encrypted = false;

            DeleteCheckFile();

            UserConfigService.Save();
            PortfolioService.DecryptPortfolios();
            AlertService.DecryptAlerts();
        }

        public static void DeleteCheckFile()
        {

            if (File.Exists(CHECKFILE))
                File.Delete(CHECKFILE);
        }

        public static void Reset()
        {
            DeleteCheckFile();

            PortfolioService.DeleteAll();
            AlertService.Delete();
            UserConfigService.Delete();
        }
        #endregion




        #region Encrypt
        public static string AesEncryptString(string clearText)
        {
            return AesEncryptString(clearText, _password, SALT);
        }

        public static string AesEncryptString(string clearText, string passText)
        {
            return AesEncryptString(clearText, passText, SALT);
        }

        public static string AesEncryptString(string clearText, string passText, string saltText)
        {
            try
            {
                byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
                byte[] passBytes = Encoding.UTF8.GetBytes(passText);
                byte[] saltBytes = Encoding.UTF8.GetBytes(saltText);

                return Convert.ToBase64String(AESEncryptBytes(clearBytes, passBytes, saltBytes));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static byte[] AESEncryptBytes(byte[] clearBytes, byte[] passBytes, byte[] saltBytes)
        {
            byte[] encryptedBytes = null;
            var key = new Rfc2898DeriveBytes(passBytes, saltBytes, 32768);

            using (Aes aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        #endregion

        #region Decrypt
        public static string AesDecryptString(string cryptText)
        {
            return AesDecryptString(cryptText, _password, SALT);
        }

        public static string AesDecryptString(string cryptText, string passText)
        {
            return AesDecryptString(cryptText, passText, SALT);
        }

        public static string AesDecryptString(string cryptText, string passText, string saltText)
        {
            try
            {
                byte[] cryptBytes = Convert.FromBase64String(cryptText);
                byte[] passBytes = Encoding.UTF8.GetBytes(passText);
                byte[] saltBytes = Encoding.UTF8.GetBytes(saltText);

                return Encoding.UTF8.GetString(AESDecryptBytes(cryptBytes, passBytes, saltBytes));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static byte[] AESDecryptBytes(byte[] cryptBytes, byte[] passBytes, byte[] saltBytes)
        {
            byte[] clearBytes = null;
            var key = new Rfc2898DeriveBytes(passBytes, saltBytes, 32768);

            using (Aes aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cryptBytes, 0, cryptBytes.Length);
                        cs.Close();
                    }
                    clearBytes = ms.ToArray();
                }
            }
            return clearBytes;
        }
        #endregion
    }
}
