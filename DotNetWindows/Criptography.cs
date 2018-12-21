using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Jitsukawa.Extensions.Criptography
{
    /// <summary>
    /// Criptografia de texto compatível com a mesma classe da biblioteca para WinRT.
    /// (Adaptado de http://janhannemann.wordpress.com/2013/11/10/simple-encryption-for-windows-winrt-and-windows-phone/)
    /// </summary>
    public static class Criptography
    {
        public static byte[] AesEncrypt(this string text, string password, string salt)
        {
            AesManaged aes = null;
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;

            try
            {
                //Generate a Key based on a Password, Salt and HMACSHA1 pseudo-random number generator 
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

                //Create AES algorithm with 256 bit key and 128-bit block size 
                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                rfc2898.Reset(); //needed for WinRT compatibility
                aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                //Create Memory and Crypto Streams 
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

                //Encrypt Data 
                byte[] data = Encoding.Unicode.GetBytes(text);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                //Return encrypted data 
                return memoryStream.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            {
                if (cryptoStream != null)
                    cryptoStream.Close();

                if (memoryStream != null)
                    memoryStream.Close();

                if (aes != null)
                    aes.Clear();
            }
        }

        public static string AesDecrypt(this byte[] encrypted, string password, string salt)
        {
            AesManaged aes = null;
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            string decryptedText = "";
            try
            {
                //Generate a Key based on a Password, Salt and HMACSHA1 pseudo-random number generator 
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

                //Create AES algorithm with 256 bit key and 128-bit block size 
                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                rfc2898.Reset(); //neede to be WinRT compatible
                aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                //Create Memory and Crypto Streams 
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                //Decrypt Data 
                cryptoStream.Write(encrypted, 0, encrypted.Length);
                cryptoStream.FlushFinalBlock();

                //Return Decrypted String 
                byte[] decryptBytes = memoryStream.ToArray();
                decryptedText = Encoding.Unicode.GetString(decryptBytes, 0, decryptBytes.Length);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (cryptoStream != null)
                    cryptoStream.Close();

                if (memoryStream != null)
                    memoryStream.Close();

                if (aes != null)
                    aes.Clear();
            }
            return decryptedText;
        }

        public static byte[] MD5(this byte[] data)
        {
            HashAlgorithm hash = new MD5CryptoServiceProvider();
            hash.Initialize();
            return hash.ComputeHash(data);
        }
    }
}
