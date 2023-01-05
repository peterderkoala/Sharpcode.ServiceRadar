using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public static class RsaController<T>
    {
        private const int _size = 1024;
        private static JsonSerializerOptions serializationOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            MaxDepth = 0
        };

        public static string GetPublicRSA()
        {
            if (File.Exists("private.xml") && File.Exists("public.xml"))
                return File.ReadAllText("public.xml");

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_size);

            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            File.WriteAllText("public.xml", publicKey, Encoding.UTF8);
            File.WriteAllText("private.xml", privateKey, Encoding.UTF8);

            rsa.Dispose();

            return publicKey;
        }

        public static string Encrypt(T data)
        {
            string json = JsonSerializer.Serialize(data, serializationOptions);
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_size);

            rsa.FromXmlString(File.ReadAllText("private.xml", Encoding.UTF8));
            byte[] encryptedJson = rsa.Encrypt(Encoding.UTF8.GetBytes(json), true);

            return Convert.ToBase64String(encryptedJson);
        }

        public static T Decrypt(string encryptedJsonBase64)
        {
            // Create a new RSA object and load the public key
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_size))
            {
                rsa.FromXmlString(File.ReadAllText("public.xml", Encoding.UTF8));

                // Convert the encrypted JSON from a base64 string to a byte array
                byte[] encryptedJson = Convert.FromBase64String(encryptedJsonBase64);

                // Decrypt the JSON string using the public key
                string json = Encoding.UTF8.GetString(rsa.Decrypt(encryptedJson, true));

                // Deserialize the JSON string to an object of type T
                return JsonSerializer.Deserialize<T>(json, serializationOptions);
            }
        }

        public static string PrivatePublicEncrypt(T data, string receiverPublicKey)
        {
            string innerEncryptionString = Encrypt(data);

            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_size);
            {
                rsa.FromXmlString(receiverPublicKey);
                byte[] encryptedString = rsa.Encrypt(Encoding.UTF8.GetBytes(innerEncryptionString), true);

                return Convert.ToBase64String(encryptedString);
            }
        }

        public static T PublicPrivateDecrypt(string encryptedJsonBase64, string senderPublicKey)
        {
            // Create a new RSA object and load the public key
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_size))
            {
                rsa.FromXmlString(senderPublicKey);

                // Convert the encrypted JSON from a base64 string to a byte array
                byte[] encryptedString = Convert.FromBase64String(encryptedJsonBase64);

                // Decrypt the JSON string using the public key
                string outerDecryptString = Encoding.UTF8.GetString(rsa.Decrypt(encryptedString, true));

                return Decrypt(outerDecryptString);
            }
        }
    }
}
