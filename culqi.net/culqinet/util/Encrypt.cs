using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace culqinet.util
{
	public class Encrypt
	{
		public Encrypt()
		{
		}


        public async Task<Dictionary<string, object>> EncryptWithAESRSA(string data, string publicKey, bool isJson)
        {

            // Serialize the JSON object to a string
            string jsonData = JsonConvert.SerializeObject(data);

            // Convert the string to a byte array
            var jsonBytes = Encoding.UTF8.GetBytes(data);
            string encryptedData = "";
            // Generate random key and IV
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            // Encrypt plaintext with AES-256 in CBC mode
            byte[] cipherText;
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                {
                    cipherText = encryptor.TransformFinalBlock(jsonBytes, 0, jsonBytes.Length);
                    encryptedData = Convert.ToBase64String(cipherText);
                }
            }
            //END AES ENCRYPT

            //RSA ENCRYPT SHA256
            string publicKeyString = publicKey;

            byte[] publicKeyBytes = Convert.FromBase64String(publicKeyString);

            // create an RSA object and import the public key bytes
            RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

            // encrypt the data using RSA OAEP SHA256 padding
            byte[] encryptedKey = rsa.Encrypt(key, RSAEncryptionPadding.OaepSHA256);
            byte[] encryptedIv = rsa.Encrypt(iv, RSAEncryptionPadding.OaepSHA256);

            // convert the encrypted data to a base64 string
            string encryptedKeyToJson = Convert.ToBase64String(encryptedKey);
            string encryptedIvToJson = Convert.ToBase64String(encryptedIv);

            // print the encrypted data

             
            return new Dictionary<string, object>
                        {
                            { "encrypted_data", encryptedData },
                            { "encrypted_key", encryptedKeyToJson },
                            { "encrypted_iv", encryptedIvToJson }
                        };




        }

    }

    
}

