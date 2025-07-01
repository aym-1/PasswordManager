//Does the encryption/decryption of the passwords.
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace AndrewCo.LearningProjects.PasswordManager.PasswodManagerConsole;

public static class EncryptionService
{
    private const string KEY_FILE = "secret.key";

    public static bool KeyExists() => File.Exists(KEY_FILE);

    public static void WriteKey()
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] key = new byte[32]; // 256-bit key
        rng.GetBytes(key);
        File.WriteAllBytes(KEY_FILE, key);
    }

    public static byte[] LoadKey() => File.ReadAllBytes(KEY_FILE);

    public static string Encrypt(string data, byte[] key)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(data), 0, data.Length);

        byte[] combined = aes.IV.Concat(encrypted).ToArray();
        return Convert.ToBase64String(combined);
    }

    public static string Decrypt(string data, byte[] key)
    {
        byte[] combined = Convert.FromBase64String(data);
        byte[] iv = combined.Take(16).ToArray();
        byte[] encrypted = combined.Skip(16).ToArray();

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        byte[] decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
        return Encoding.UTF8.GetString(decrypted);
    }
}
