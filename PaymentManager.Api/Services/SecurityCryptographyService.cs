﻿using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using PaymentManager.Api.Services.Interfaces;

namespace PaymentManager.Api.Services
{
    public class SecurityCryptographyService : ISecurityCryptographyService
    {
        public IConfiguration Configuration { get; }

        public SecurityCryptographyService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string DecryptStringAes(string cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("plainText");

            var key = Configuration.GetSection("AesData:Key").Value;
            var byteKey = Encoding.UTF8.GetBytes(key);
            try
            {
                var fullCipher = Convert.FromBase64String(cipherText);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(byteKey, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Exception: {ex.Message}");
                return string.Empty;
            }
        }

        public string EncryptStringAes(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            var key = Configuration.GetSection("AesData:Key").Value;
            var byteKey = Encoding.UTF8.GetBytes(key);
            try
            {
                using (var aesAlg = Aes.Create())
                {
                    using (var encryptor = aesAlg.CreateEncryptor(byteKey, aesAlg.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }

                            var iv = aesAlg.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            var str = Convert.ToBase64String(result);
                            var fullCipher = Convert.FromBase64String(str);
                            return str;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Exception: {ex.Message}");
                return string.Empty;
            }
        }

        public string DecryptStringRsa(string cipherText)
        {
            var key = Configuration.GetSection("RsaData:PrivateKey").Value;
            using RSA rsa = RSA.Create();
            var helperArray = new byte[256];
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(key), out _);
            var decrypted = rsa.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1);

            return Encoding.UTF8.GetString(decrypted);
        }

        public string EncryptStringRsa(string plainText)
        {
            var key = Configuration.GetSection("RsaData:PublicKey").Value; 
            using RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(key), out _);
            var encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(encrypted);
        }
    }
}