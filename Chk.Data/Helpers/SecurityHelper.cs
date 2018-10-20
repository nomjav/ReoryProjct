using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AcademyLockSmith.Data.Helpers
{
    /// <summary>
    /// 	Represents a security helper
    /// </summary>
    public static class SecurityHelper
    {
        private static readonly string EncryptionPrivateKey = "TESTKEY!@#ALSDKJ1238";

        #region Utilities

        private static byte[] EncryptTextToMemory(string Data, byte[] Key, byte[] IV)
        {
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV),
                                                    CryptoStreamMode.Write);
            byte[] toEncrypt = new UnicodeEncoding().GetBytes(Data);
            cStream.Write(toEncrypt, 0, toEncrypt.Length);
            cStream.FlushFinalBlock();
            byte[] ret = mStream.ToArray();
            cStream.Close();
            mStream.Close();
            return ret;
        }

        private static string DecryptTextFromMemory(byte[] Data, byte[] Key, byte[] IV)
        {
            MemoryStream msDecrypt = new MemoryStream(Data);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV),
                                                      CryptoStreamMode.Read);
            StreamReader sReader = new StreamReader(csDecrypt, new UnicodeEncoding());
            return sReader.ReadLine();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 	Decrypts text
        /// </summary>
        /// <param name="CipherText"> Cipher text </param>
        /// <returns> Decrypted string </returns>
        public static string Decrypt(string CipherText)
        {
            return Decrypt(CipherText, EncryptionPrivateKey);
        }

        /// <summary>
        /// 	Decrypts text
        /// </summary>
        /// <param name="CipherText"> Cipher text </param>
        /// <param name="encryptionPrivateKey"> Encryption private key </param>
        /// <returns> Decrypted string </returns>
        private static string Decrypt(string CipherText, string encryptionPrivateKey)
        {
            if (String.IsNullOrEmpty(CipherText))
                return CipherText;

            TripleDESCryptoServiceProvider tDesAlg = new TripleDESCryptoServiceProvider
            {
                Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16)),
                IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8))
            };

            byte[] buffer = Convert.FromBase64String(CipherText);
            string result = DecryptTextFromMemory(buffer, tDesAlg.Key, tDesAlg.IV);
            return result;
        }

        /// <summary>
        /// 	Encrypts text
        /// </summary>
        /// <param name="PlainText"> Plaint text </param>
        /// <returns> Encrypted string </returns>
        public static string Encrypt(string PlainText)
        {
            return Encrypt(PlainText, EncryptionPrivateKey);
        }

        /// <summary>
        /// 	Encrypts text
        /// </summary>
        /// <param name="PlainText"> Plaint text </param>
        /// <param name="encryptionPrivateKey"> Encryption private key </param>
        /// <returns> Encrypted string </returns>
        private static string Encrypt(string PlainText, string encryptionPrivateKey)
        {
            if (String.IsNullOrEmpty(PlainText))
                return PlainText;

            TripleDESCryptoServiceProvider tDesAlg = new TripleDESCryptoServiceProvider
            {
                Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16)),
                IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8))
            };

            byte[] encryptedBinary = EncryptTextToMemory(PlainText, tDesAlg.Key, tDesAlg.IV);
            string result = Convert.ToBase64String(encryptedBinary);
            return result;
        }


        /// <summary>
        /// Convert a String to the HASH using the SHA1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSHA1Hash(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            string rethash = "";
            try
            {
                SHA1 hash = SHA1.Create();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] combined = encoder.GetBytes(str);
                hash.ComputeHash(combined);
                rethash = Convert.ToBase64String(hash.Hash);
            }
            catch (Exception exc)
            {
                //Debug.WriteLine(exc.Message);
                Trace.WriteLine("##Error## " + DateTime.Now.ToString() + Environment.NewLine + "Method: ToSHA1Hash" + Environment.NewLine + exc);
            }
            return rethash;
        }

        public static string ApplyCreditCardMask(string unmaskedCardNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(unmaskedCardNumber))
                {
                    char[] listOfCharacters = unmaskedCardNumber.ToCharArray();
                    for (int i = 0; i < listOfCharacters.Length - 4; i++)
                    {
                        listOfCharacters[i] = '*';
                    }

                    return new string(listOfCharacters);
                }
            }
            catch (Exception exception)
            {
                //Debug.WriteLine(exception.Message);
                Trace.WriteLine("##Error## " + DateTime.Now.ToString() + Environment.NewLine + "Method: ApplyCreditCardMask" + Environment.NewLine + exception);

                return string.Empty;
            }
            return string.Empty;
        }

        public static bool IsEncrypted(string text)
        {
            try
            {
                SecurityHelper.Decrypt(text);
                return true;
            }
#pragma warning disable
            catch (Exception ex)
            {
                return false;
            }
        }


        #endregion
    }

}
