// It does the adding, retrieving, deleting, and listing the already stored passwords.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AndrewCo.LearningProjects.PasswordManager.PasswodManagerConsole;


public class PasswordManager
{
    private const string DATA_FILE = "storage.json";
    private readonly byte[] encryptionKey;

    public PasswordManager(byte[] key)
    {
        encryptionKey = key;
    }

    private Dictionary<string, PasswordEntry> Load()
    {
        try
        {
            if (File.Exists(DATA_FILE))
            {
                string json = File.ReadAllText(DATA_FILE);
                return JsonSerializer.Deserialize<Dictionary<string, PasswordEntry>>(json);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading data: {e.Message}");
        }

        return new Dictionary<string, PasswordEntry>();
    }

    private void Save(Dictionary<string, PasswordEntry> data)
    {
        try
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DATA_FILE, json);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving data: {e.Message}");
        }
    }

    public void AddPassword()
    {
        Console.Write("Website: ");
        string site = Console.ReadLine();

        Console.Write("Username: ");
        string username = Console.ReadLine();

        Console.Write("Generate password? (y/n): ");
        string pass = Console.ReadLine().ToLower() == "y"
            ? PasswordUtils.GeneratePassword()
            : PasswordUtils.ReadPassword();

        string encrypted = EncryptionService.Encrypt(pass, encryptionKey);
        var data = Load();

        data[site] = new PasswordEntry
        {
            Username = username,
            Password = encrypted
        };

        Save(data);
        Console.WriteLine("Password saved.");
    }

    public void GetPassword()
    {
        Console.Write("Enter website: ");
        string site = Console.ReadLine();

        var data = Load();

        if (data.TryGetValue(site, out var entry))
        {
            try
            {
                string decrypted = EncryptionService.Decrypt(entry.Password, encryptionKey);
                Console.WriteLine($"\nWebsite: {site}\nUsername: {entry.Username}\nPassword: {decrypted}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decryption failed: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("No entry found for that website.");
        }
    }

    public void DeletePassword()
    {
        Console.Write("Website to delete: ");
        string site = Console.ReadLine();

        var data = Load();

        if (data.ContainsKey(site))
        {
            Console.Write($"Are you sure you want to delete '{site}'? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                data.Remove(site);
                Save(data);
                Console.WriteLine("Password deleted.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }
        else
        {
            Console.WriteLine("No entry found.");
        }
    }

    public void ListPasswords()
    {
        var data = Load();

        if (data.Count == 0)
        {
            Console.WriteLine("No passwords stored.");
            return;
        }

        Console.WriteLine("\nStored Entries:");
        foreach (var site in data.Keys)
        {
            Console.WriteLine($"- {site}");
        }
    }
}
