using System;
using System.Collections.Generic;
using System.IO;

namespace TextMining.Helpers
{
    public static class UserInputHandler
    {
        public static string GetPathInputFromUser()
        {
            while (true)
            {
                Console.Write("input: ");
                var userInput = Console.ReadLine();
                Console.WriteLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    ConsoleWriteLineWithColor("Invalid input. Must provide a valid directory path.", ConsoleColor.DarkRed);
                    continue;
                }

                if (!Directory.Exists(userInput))
                {
                    ConsoleWriteLineWithColor("Invalid input. Given directory could not be found.", ConsoleColor.DarkRed);
                    continue;
                }

                return userInput;
            }
        }

        public static int GetNumberInputFromUser(List<int> expectedValues = null)
        {
            var errorMessage = expectedValues != null
                ? $"Invalid input, expected a number equal to one of these: [{string.Join(',', expectedValues)}]"
                : "Invalid input, expected a valid number";

            while (true)
            {
                Console.Write("input: ");
                var userInput = Console.ReadLine();
                Console.WriteLine();

                if (int.TryParse(userInput, out var number))
                {
                    if (expectedValues != null && expectedValues.Contains(number))
                    {
                        return number;
                    }

                    if (expectedValues == null)
                    {
                        return number;
                    }
                }

                ConsoleWriteLineWithColor(errorMessage, ConsoleColor.DarkRed);
            }
        }

        private static void ConsoleWriteLineWithColor(string message, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            var oldForeGroundColor = Console.ForegroundColor;

            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);

            Console.ForegroundColor = oldForeGroundColor;
        }
    }
}