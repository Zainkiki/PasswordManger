using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Transactions;

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

                        Console.WriteLine("Enter the login username");
                        string userName = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter the website name");
                        string websiteName = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter the Email");
                        string email = Console.ReadLine() ?? "";

                        Console.WriteLine("Write a note if needed");
                        string note = Console.ReadLine() ?? "";

                        PasswordEntry newEntry = new PasswordEntry
                        {
                            Password = currentPassword,
                            Username = userName,
                            Website = websiteName,
                            Email = email,
                            Note = note
                        };

                        List<PasswordEntry> passwords = new List<PasswordEntry>();

                        if (File.Exists(filePath))
                        {
                            string existingJson = File.ReadAllText(filePath);

                            passwords = JsonSerializer.Deserialize<List<PasswordEntry>>(existingJson)
                                        ?? new List<PasswordEntry>();
                        }

                        passwords.Add(newEntry);

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

                            List<PasswordEntry> savedPasswords =
                                JsonSerializer.Deserialize<List<PasswordEntry>>(savedJson)
                                ?? new List<PasswordEntry>();

                            foreach (PasswordEntry entry in savedPasswords)
                            {
                                Console.WriteLine("-------------------------");
                                Console.WriteLine($"Password: {entry.Password}");
                                Console.WriteLine($"Username: {entry.Username}");
                                Console.WriteLine($"Website: {entry.Website}");
                                Console.WriteLine($"Email: {entry.Email}");
                                Console.WriteLine($"Note: {entry.Note}");
                                Console.WriteLine("-------------------------");
                            }
                        }

                        else
                        {
                            Console.WriteLine("No saved passwords found.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("You are about to clear the Password manger!");
                        Console.WriteLine("To continute pick one of the options");
                        Console.WriteLine("1. Clear");
                        Console.WriteLine("2. Cancel");

                        string pick = Console.ReadLine() ?? "0";


                        while (pick != "1" && pick != "2")
                        {
                            Console.WriteLine("\nPress any key to go back...");
                            Console.WriteLine("To continute pick one of the options");
                            Console.WriteLine("1. Clear");
                            Console.WriteLine("2. Cancel");

                            pick = Console.ReadLine() ?? "0";
                        }
                        if (pick == "1")
                        {
                            File.WriteAllText(filePath, "[]");
                            Console.WriteLine("Password manger has been cleared");
                        }
                        else if (pick == "2")
                        {
                            Console.WriteLine("Canceled");
                        }
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
