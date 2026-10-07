using System;
namespace Bankomat
{
    internal class Program
    {
        static void ShowBalance(BankCard card)
        {
            Console.Write("Введіть PIN-код ще раз: ");
            string pin = Console.ReadLine();

            try
            {
                decimal balance = card.GetBalance(pin);
                Console.WriteLine($"Ваш баланс: {balance:F2} грн");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        // Зняти готівку
        static void WithdrawCash(BankCard card, ATM atm)
        {
            Console.Write("Введіть PIN-код ще раз: ");
            string pin = Console.ReadLine();

            Console.Write("Введіть суму для зняття: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Некоректна сума.");
                return;
            }

            try
            {
                if (!card.CheckPin(pin))
                {
                    Console.WriteLine("Невірний PIN.");
                    return;
                }

                Dictionary<int, int> plan = atm.Withdraw(amount);
                card.Withdraw(pin, amount);

                Console.WriteLine("Видано купюрами:");
                foreach (var pair in plan)
                    Console.WriteLine($"  {pair.Key} грн × {pair.Value}");

                Console.WriteLine($"Залишок на карті: {card.GetBalance(pin):F2} грн");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Відмова: {ex.Message}");
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            BankCardManager manager = BankCardManager.GetInstance();
            ATM atm = new ATM();

            while (true)   // цикл «вставка картки»
            {
                Console.Clear();
                Console.WriteLine("=== БАНКОМАТ ===");
                Console.WriteLine("Вставте картку (введіть номер) або 'exit' для виходу:");
                string cardNumber = Console.ReadLine().Trim();

                if (cardNumber.ToLower() == "exit") break;

                BankCard card;
                try
                {
                    card = manager.GetBankCard(cardNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                    Console.ReadLine();
                    continue;
                }

                if (card.IsBlocked())
                {
                    Console.WriteLine("Картка заблокована.");
                    Console.ReadLine();
                    continue;
                }

                // Введення PIN-кода
                bool pinOk = false;
                for (int attempt = 1; attempt <= 3; attempt++)
                {
                    Console.Write("Введіть PIN-код: ");
                    string pin = Console.ReadLine();

                    try
                    {
                        if (card.CheckPin(pin))
                        {
                            pinOk = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Невірний PIN. Залишилось спроб: {3 - attempt}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                        break;
                    }
                }

                if (!pinOk)
                {
                    Console.WriteLine("Повернення картки...");
                    Console.ReadLine();
                    continue;
                }

                // Меню операцій
                bool sessionActive = true;
                while (sessionActive)
                {
                    Console.Clear();
                    Console.WriteLine($"Вітаємо, {card.GetOwnerName()}!");
                    Console.WriteLine("1 — Переглянути баланс");
                    Console.WriteLine("2 — Зняти готівку");
                    Console.WriteLine("0 — Завершити (повернути картку)");
                    Console.Write("Ваш вибір: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ShowBalance(card);
                            break;

                        case "2":
                            WithdrawCash(card, atm);
                            break;

                        case "0":
                            sessionActive = false;
                            break;

                        default:
                            Console.WriteLine("Невірний вибір.");
                            break;
                    }

                    if (sessionActive)
                    {
                        Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
                        Console.ReadLine();
                    }
                }

                Console.WriteLine("Картку повернено. Дякуємо!");
                Console.ReadLine();
            }
        }
    }
}
