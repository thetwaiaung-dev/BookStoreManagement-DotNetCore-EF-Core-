using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Encrypt
{
    public class StringEncryptionService
    {
        private byte[] DeriveKeyFromPassword(string password)
        {
            var emptySalt = Array.Empty<byte>();
            var iteration = 1000;
            var desiredKeyLength = 16;
            var hashMethod = HashAlgorithmName.SHA384;

            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                                emptySalt,
                                                iteration,
                                                hashMethod,
                                                desiredKeyLength);
        }

        private byte[] IV =
        {
                 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };

        public async Task<byte[]> EncryptAsync(string clearText, string passPhrase)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(passPhrase);
            aes.IV = IV;

            using MemoryStream output = new();
            using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);

            await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(clearText));
            await cryptoStream.FlushFinalBlockAsync();

            return output.ToArray();
        }

        public async Task<string> DecryptAsync(byte[] encrypted, string passPhrase)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(passPhrase);
            aes.IV = IV;

            using MemoryStream input = new(encrypted);
            using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

            using MemoryStream outPut = new();
            await cryptoStream.CopyToAsync(outPut);

            return Encoding.Unicode.GetString(outPut.ToArray());
        }
    }
}
