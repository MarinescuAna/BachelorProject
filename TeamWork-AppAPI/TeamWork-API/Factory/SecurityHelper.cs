using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;

namespace TeamWork_API.Factory
{
    public class SecurityHelper
    {
        private readonly string keyString;
        public SecurityHelper(IConfiguration _configuration)
        {
            keyString = _configuration[Constants.SecurityKey];
        }
        public string EncryptString(string text)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using var aesAlg = Aes.Create();
            using var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }

            var iv = aesAlg.IV;

            var decryptedContent = msEncrypt.ToArray();

            var result = new byte[iv.Length + decryptedContent.Length];

            Buffer.BlockCopy(iv, Number.Number_0, result, Number.Number_0, iv.Length);
            Buffer.BlockCopy(decryptedContent, Number.Number_0, result, iv.Length, decryptedContent.Length);

            return Convert.ToBase64String(result);
        }

        public string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[Number.Number_16];
            var cipher = new byte[Number.Number_16];

            Buffer.BlockCopy(fullCipher, Number.Number_0, iv, Number.Number_0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, Number.Number_0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using var aesAlg = Aes.Create();
            using var decryptor = aesAlg.CreateDecryptor(key, iv);
            string result;
            using (var msDecrypt = new MemoryStream(cipher))
            {
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                result = srDecrypt.ReadToEnd();
            }

            return result;
        }
    }
}
