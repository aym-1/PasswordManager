//It checks the stength of the password, generates a salt, reads the password from the console, and generates a random password.
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AndrewCo.LearningProjects.PasswordManager.PasswodManagerConsole;


public static class PasswordUtils
{
    public static string ReadPassword()
    {
        StringBuilder password = new();
        ConsoleKeyInfo key;

        while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Length--;
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password.Append(key.KeyChar);
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return password.ToString();
    }

    public static string GenerateSalt()
    {
        byte[] salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return BitConverter.ToString(salt).Replace("-", "").ToLower();
    }

    public static bool ValidatePassword(string password) =>
        password.Length >= 8 &&
        password.Any(char.IsUpper) &&
        password.Any(char.IsLower) &&
        password.Any(char.IsDigit) &&
        password.Any(c => !char.IsLetterOrDigit(c));

    public static string GeneratePassword(int length = 12)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";
        using var rng = RandomNumberGenerator.Create();
        return new string(Enumerable.Range(0, length).Select(_ => chars[RandomNumberGenerator.GetInt32(chars.Length)]).ToArray());
    }
}

