using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PasswordManger
{
    public static class PromptStart
    {
        private static readonly string filePath = "passwords.json";

        public static void StartMenu()
        {
            string choice;
            string currentPassword = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Pick an option");
                Console.WriteLine("1. Generate password");
                Console.WriteLine("2. Save password");
                Console.WriteLine("3. Password manager");
                Console.WriteLine("4. Clear manger");
                Console.WriteLine("0. Exit");

                choice = Console.ReadLine() ?? "0";

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        currentPassword = GeneratePassword.RandomCombine(16);
                        Console.WriteLine($"Generated password: {currentPassword}");
                        break;

                    case "2":
                        if (string.IsNullOrEmpty(currentPassword))
                        {
                            Console.WriteLine("Generate a password first!");
                            break;
                        }

                        List<string> passwords = new List<string>();

                        if (File.Exists(filePath))
                        {
                            string existingJson = File.ReadAllText(filePath);

                            passwords = JsonSerializer.Deserialize<List<string>>(existingJson)
                                        ?? new List<string>();
                        }

                        passwords.Add(currentPassword);

                        string json = JsonSerializer.Serialize(passwords,
                            new JsonSerializerOptions { WriteIndented = true });

                        File.WriteAllText(filePath, json);

                        Console.WriteLine("Password saved!");
                        currentPassword = "";
                        break;

                    case "3":
                        if (File.Exists(filePath))
                        {
                            string savedJson = File.ReadAllText(filePath);

                            List<string> savedPasswords =
                                JsonSerializer.Deserialize<List<string>>(savedJson)
                                ?? new List<string>();

                            foreach (string password in savedPasswords)
                            {
                                Console.WriteLine(password);
                            }
                        }

                        else
                        {
                            Console.WriteLine("No saved passwords found.");
                        }
                        break;

                    case "4":
                        File.WriteAllText(filePath, "[]");
                        Console.WriteLine("Password manger has been cleared");
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nPress any key to go back...");
                    Console.ReadKey();
                }

            } while (choice != "0");
        }
    }
}
