using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManger
{
    public static class GeneratePassword
    {
        public static string RandomCombine(int size)
        {
            string fullPassword = "";

            for (int i = 0; i < size; i++)
            {
                int choice = RNG.random.Next(0, 3);

                if (choice == 0)
                {
                    int number = RNG.random.Next(0, 26);
                    char letter = 'a';
                    letter += (char)number;

                    fullPassword += letter;
                }
                else if (choice == 1)
                {
                    int number = RNG.random.Next(0, 26);
                    char letter = 'A';
                    letter += (char)number;

                    fullPassword += letter;
                }
                else
                {
                    int number = RNG.random.Next(0, 10);
                    fullPassword += number;
                }
            }

            return fullPassword;
        }
    }
}
