using System;
namespace DZ3._2
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
        static Dictionary<string, string> contacts = new Dictionary<string, string>();
        public void CreateContact(string name, string phone)
        {
            if (name.Length > 2 && phone.Length >= 10 && phone.Length <= 12)
            {
                contacts.Add(name, phone);
            }
            else
            {
                Console.WriteLine("Некоректні дані.");
                return;
            }
        }
        public void UpdateContact(Dictionary<string, string> contact, string name, string phone)
        {
            if (contact.ContainsKey(name))
            {
                string change = MessageHelper.GetString("Що ви хочете змінити?(ім'я/телефон) ").ToLower();
                if (change == "ім'я")
                {
                    string newName = MessageHelper.GetString("Введіть нове ім'я: ");
                    contact.Remove(name);
                    contact.Add(newName, phone);
                }
                else if (change == "телефон")
                {
                    string newPhone = MessageHelper.GetString("Введіть новий телефон: ");
                    contact[name] = newPhone;
                }
                else
                {
                    Console.WriteLine("Некоректний вибір.");
                }
            }
            else
            {
                Console.WriteLine("Контакт не знайдено.");
            }
        }
        public void DeleteContact(Dictionary<string, string> contact, string name)
        {
            if (contact.ContainsKey(name))
            {
                contact.Remove(name);
                Console.WriteLine("Контакт видалено.");
            }
            else
            {
                Console.WriteLine("Контакт не знайдено.");
            }
        }
        public void SearchContact(Dictionary<string, string> contact, string name)
        {
            bool found = false;
            foreach (var pair in contact)
            {
                if (pair.Key.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"Ім'я: {pair.Key}, Телефон: {pair.Value}");
                    found = true;
                }
            }
            if(!found)
            {
                Console.WriteLine("Контакт не знайдено.");
            }
        }
        public void DisplayContacts(Dictionary<string, string> contact)
        {
            if (contact.Count == 0)
            {
                Console.WriteLine("Список контактів порожній.");
                return;
            }
            Console.WriteLine("Список контактів:");
            foreach (var item in contact)
            {
                Console.WriteLine($"Ім'я: {item.Key}, Телефон: {item.Value}");
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("         ТЕЛЕФОННА КНИГА");
                Console.WriteLine("========================================");
                Console.WriteLine("  1. Додати контакт");
                Console.WriteLine("  2. Редагувати контакт");
                Console.WriteLine("  3. Видалити контакт");
                Console.WriteLine("  4. Шукати контакт");
                Console.WriteLine("  5. Показати всі контакти");
                Console.WriteLine("  6. Вийти");
                Console.WriteLine("========================================");
                Console.WriteLine($"  Контактів у базі: {contacts.Count}");
                Console.WriteLine("========================================");
                string choice = MessageHelper.GetString("Виберіть дію: ");
                if (choice == "1")
                {
                    Console.WriteLine("--- ДОДАТИ КОНТАКТ ---");
                    string name = MessageHelper.GetString("Введіть ім'я: ");
                    string phone = MessageHelper.GetString("Введіть телефон: ");
                    new Program().CreateContact(name, phone);
                    Console.WriteLine("Контакт додано.");
                }
                else if (choice == "2")
                {
                    Console.WriteLine("--- РЕДАГУВАТИ КОНТАКТ ---");
                    string name = MessageHelper.GetString("Введіть ім'я контакту для редагування: ");
                    string phone = contacts.ContainsKey(name) ? contacts[name] : null;
                    new Program().UpdateContact(contacts, name, phone);
                    Console.WriteLine("Контакт оновлено.");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("--- ВИДАЛИТИ КОНТАКТ ---");
                    string name = MessageHelper.GetString("Введіть ім'я контакту для видалення: ");
                    new Program().DeleteContact(contacts, name);
                }
                else if (choice == "4")
                {
                    Console.WriteLine("--- ШУКАТИ КОНТАКТ ---");
                    string name = MessageHelper.GetString("Введіть ім'я контакту для пошуку: ");
                    new Program().SearchContact(contacts, name);
                }
                else if (choice == "5")
                {
                    Console.WriteLine("--- ВСІ КОНТАКТИ ---");
                    new Program().DisplayContacts(contacts);
                }
                else if (choice == "6")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Некоректний вибір.");
                }
                Console.ReadKey();
            }
        }
    }
}
