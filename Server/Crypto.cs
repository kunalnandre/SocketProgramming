using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
    private static byte[] Key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32 bytes for AES-256
    private static byte[] IV = Encoding.UTF8.GetBytes("ABCDEF0123456789"); // 16 bytes

    public static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            byte[] encrypted;
            using (var ms = new System.IO.MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encrypted = ms.ToArray();
            }
            return Convert.ToBase64String(encrypted);
        }
    }

    public static string Decrypt(string cipherText)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes aes = Aes.Create())
        {
            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            using (var ms = new System.IO.MemoryStream(cipherBytes))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var reader = new System.IO.StreamReader(cs))
            {
                return reader.ReadToEnd();
            }
        }
    }
}