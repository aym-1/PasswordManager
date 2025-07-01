// It handles a the master password setup and the hashing.
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace AndrewCo.LearningProjects.PasswordManager.PasswodManagerConsole;

public static class AuthService
{
    private const string SALT_FILE = "salt.txt";
    private const string MASTER_FILE = "master_hash.txt";

    public static bool Authenticate()
    {
        if (!File.Exists(SALT_FILE) || !File.Exists(MASTER_FILE))
            SetupMasterPassword();

        for (int attempts = 3; attempts > 0; attempts--)
        {
            Console.Write("Enter master password: ");
            var input = PasswordUtils.ReadPassword();
            var salt = File.ReadAllText(SALT_FILE);
            var hash = Hash(input, salt);

            if (hash == File.ReadAllText(MASTER_FILE)) return true;

            Console.WriteLine("Incorrect password.");
            Thread.Sleep(2000 * (3 - attempts));
        }
        return false;
    }

    private static void SetupMasterPassword()
    {
        while (true)
        {
            Console.Write("Set master password: ");
            string pass = PasswordUtils.ReadPassword();
            if (!PasswordUtils.ValidatePassword(pass))
            {
                Console.WriteLine("Must include upper, lower, number, symbol, 8+ chars.");
                continue;
            }

            Console.Write("Confirm: ");
            string confirm = PasswordUtils.ReadPassword();

            if (pass == confirm)
            {
                string salt = PasswordUtils.GenerateSalt();
                File.WriteAllText(SALT_FILE, salt);
                File.WriteAllText(MASTER_FILE, Hash(pass, salt));
                break;
            }
            Console.WriteLine("Mismatch. Try again.");
        }
    }

    private static string Hash(string password, string salt)
    {
        using var sha = SHA256.Create();
        byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(salt + password));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}
