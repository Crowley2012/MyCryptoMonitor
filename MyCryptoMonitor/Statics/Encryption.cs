using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/*
 * Credit for this class goes to the author of this article
 * https://msftstack.wordpress.com/2014/12/31/simple-aes-byte-encryption-and-decryption-routines-in-c/
 */
namespace MyCryptoMonitor.Statics
{
    public static class Encryption
    {
        #region Constants
        private const string SALT = "QM4436DL3A259EFXYNZEW4TCVVY5QZJG9CXFEKFW";
        #endregion

        #region Encrypt
        public static string AesEncryptString(string clearText)
        {
            return AesEncryptString(clearText, EncryptionService.Password, SALT);
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
            return AesDecryptString(cryptText, EncryptionService.Password, SALT);
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
