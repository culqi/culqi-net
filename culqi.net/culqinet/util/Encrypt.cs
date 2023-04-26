using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO.Pem;

namespace culqinet.util
{
	public class Encrypt
	{
		public Encrypt()
		{
		}


        public async Task<Dictionary<string, object>> EncryptWithAESRSA(string data, string publicKey, bool isJson)
        {

            byte[] key = GenerateRandomBytes(32);
            byte[] iv = GenerateRandomBytes(16);

            byte[] encryptedData;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            if (isJson)
                            {
                                string jsonData = JsonConvert.SerializeObject(data);

                                streamWriter.Write(jsonData);
                            }
                            else
                            {
                                streamWriter.Write(data.ToString());
                            }
                        }
                    }
                    encryptedData = memoryStream.ToArray();
                }
            }

            string encryptedDataBase64 = Convert.ToBase64String(encryptedData);

            RSA rsaPublicKey = RSA.Create();
            string pubKey = "<RSAKeyValue><Modulus>" + publicKey + "</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            rsaPublicKey.FromXmlString(pubKey);

            byte[] encryptedKey = rsaPublicKey.Encrypt(key, RSAEncryptionPadding.OaepSHA1);
            byte[] encryptedIv = rsaPublicKey.Encrypt(iv, RSAEncryptionPadding.OaepSHA1);

            string encryptedKeyBase64 = Convert.ToBase64String(encryptedKey);
            string encryptedIvBase64 = Convert.ToBase64String(encryptedIv);

          
            return new Dictionary<string, object>
                        {
                            { "encrypted_data", encryptedDataBase64 },
                            { "encrypted_key", encryptedKeyBase64 },
                            { "encrypted_iv", encryptedIvBase64 }
                        };



        }
        private static byte[] GenerateRandomBytes(int length)
        {
            byte[] randomBytes = new byte[length];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomBytes);
            }
            return randomBytes;
        }

    }

    
}

