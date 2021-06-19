using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TeamWork.ApplicationLogger;
using TeamWork.Common.ConstantNumbers;
using TeamWork.Common.ConstantStrings;

namespace TeamWork_API.Factory
{
    public class SecurityHelper
    {
        private readonly string keyString;
        private readonly ILoggerService _loggerService;
        public SecurityHelper(IConfiguration _configuration, ILoggerService loggerService)
        {
            keyString = _configuration[Constants.SecurityKey];
            _loggerService = loggerService;
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
            string result = string.Empty;

            using (var msDecrypt = new MemoryStream(cipher))
            {
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                try
                {
                    _loggerService.LogInfo("Decryptation trying");
                    result = srDecrypt.ReadToEnd();
                }
                catch (Exception ex)
                {
                    _loggerService.LogError(ex.Message);
                    if (ex.InnerException!=null)
                    {
                        _loggerService.LogError($"Inner Exception Message: {ex.InnerException.Message}");
                    }
                    return string.Empty;
                }
            }

            return result;
        }
    }
}
