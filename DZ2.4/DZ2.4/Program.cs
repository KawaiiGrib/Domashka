using System;
namespace task4
{
    class MessageHelper
    {
        public static string GetString(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                throw new FormatException("Input cannot be empty!");

            return input;
        }

        public static int GetInt(string message)
        {
            if (!int.TryParse(GetString(message), out int num))
                throw new FormatException("Failed to parse input!");
            return num;
        }

        public static float GetFloat(string message)
        {
            if (!float.TryParse(GetString(message), out float num))
                throw new FormatException("Failed to parse input!");

            return num;
        }

        public static bool GetBoolean(string message)
        {
            switch (GetString(message + "(Y/n)").ToLower()[0])
            {
                case 'y': return true;
                case 'n': return false;
                default: throw new Exception("Input cannot be empty!");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            string input = MessageHelper.GetString("Введіть рядок: ").ToLower();
            List<string> words = new List<string>();
            string currentWord = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != ' ')
                {
                    currentWord += input[i];
                }
                else
                {
                    if (currentWord != "")
                    {
                        words.Add(currentWord);
                        currentWord = "";
                    }
                }
            }
            if (currentWord != "")
            {
                words.Add(currentWord);
            }
            Console.WriteLine($"\nКоличество слов: {words.Count}");
            Console.WriteLine("Слова в списке:");

            for (int i = 0; i < words.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {words[i]}");
            }
            //List<char> uniqueChars = new List<char>();
            //for (int i = 0; i < input.Length; i++)
            //{
            //    int k = 1;
            //    for (int j = 0; j < input.Length; j++)
            //    {
            //        if (input[i] == input[j] && i != j)
            //        {
            //            k += 1;
            //        }

            //    }
            //    if (input[i] != ' ' && !uniqueChars.Contains(input[i]))
            //    {
            //        if (k == 1)
            //        {
            //            Console.WriteLine($"Символ '{input[i]}' зустрічається лише один раз.");
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Символ '{input[i]}' зустрічається {k} разів.");
            //        }
            //    }
            //    uniqueChars.Add(input[i]);
            //}
        }
    }
}
