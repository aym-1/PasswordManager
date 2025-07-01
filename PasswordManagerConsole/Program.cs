using System;
//Entry point of the apllication which does the main user interaction loop.

namespace AndrewCo.LearningProjects.PasswordManager.PasswodManagerConsole;

public class Program
{
    private const int TIMEOUT_SECONDS = 300;

    public static void Main(string[] args)
    {
        if (!EncryptionService.KeyExists())
            EncryptionService.WriteKey();

        byte[] key = EncryptionService.LoadKey();

        if (!AuthService.Authenticate())
        {
            Console.WriteLine("Access denied. Exiting.");
            return;
        }

        PasswordManager manager = new PasswordManager(key);
        DateTime startTime = DateTime.Now;

        while (true)
        {
            if ((DateTime.Now - startTime).TotalSeconds > TIMEOUT_SECONDS)
            {
                Console.WriteLine("Session timed out. Logging out.");
                break;
            }

            Console.WriteLine("\nOptions:\n1. Add\n2. Get\n3. Delete\n4. List\n5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine().Trim();
            startTime = DateTime.Now;

            switch (choice)
            {
                case "1": manager.AddPassword(); break;
                case "2": manager.GetPassword(); break;
                case "3": manager.DeletePassword(); break;
                case "4": manager.ListPasswords(); break;
                case "5": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }
}
