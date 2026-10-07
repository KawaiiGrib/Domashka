using System;
using System.Collections.Generic;
namespace ElectronicShop
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Для коректного відображення кирилиці в консолі
                                                                // Список товарів: назва -> (ціна, кількість)
            Dictionary<string, (int price, int stock)> shopItems = new Dictionary<string, (int, int)>()
        {
            {"Ноутбук", (25000, 4)},
            {"Смартфон", (18000, 6)},
            {"Навушники", (3500, 10)},
            {"Клавіатура", (1800, 8)},
            {"Монітор", (12000, 3)},
            {"Мишка бездрітівна", (800, 15)},
            {"Зовнішній жорсткий диск", (4500, 5)},
            {"Зарядний пристрій", (1200, 12)},
            {"Чехол для телефона", (500, 20)},
            {"Планшет", (22000, 3)}
        };

            // Корзина: товар -> кількість
            Dictionary<string, int> cart = new Dictionary<string, int>();

            Console.WriteLine("Ласкаво просимо в магазин электроніки!");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            bool shopping = true;

            while (shopping)
            {
                Console.WriteLine("\n--- Доступні товари ---");
                Console.WriteLine("№  | Назва                 | Ціна  | В наявності");

                int index = 1;
                List<string> itemsList = new List<string>();
                foreach (var item in shopItems)
                {
                    Console.WriteLine($"{index,2} | {item.Key,-23} | {item.Value.price,5} | {item.Value.stock}"); // цифри для вирівнювання таблиці
                    itemsList.Add(item.Key);
                    index++;
                }

                Console.WriteLine();

                // Показуємо корзину, якщо вона не пуста
                if (cart.Count > 0)
                {
                    Console.WriteLine("Ваша корзина:");
                    int totalCart = 0;
                    foreach (var item in cart)
                    {
                        int price1 = shopItems[item.Key].price; // ціна за 1 шт
                        int subtotal = price1 * item.Value; // ціна за всі штуки
                        totalCart += subtotal; // загальна сума в корзині
                        Console.WriteLine($"  {item.Key} - {item.Value} шт. = {subtotal} грн"); // вивід товарів в корзині
                    }
                    Console.WriteLine($"Загальна сума: {totalCart} грн");
                    Console.WriteLine();

                    Console.WriteLine("Що хочете зробити?");
                    Console.WriteLine("1 - Додати товар");
                    Console.WriteLine("2 - Видалити товар");
                    Console.WriteLine("3 - Перейти до оплати");
                    string choice = Console.ReadLine();

                    if (choice != "1" && choice != "2" && choice != "3")
                    {
                        Console.WriteLine("Невірний вибір. Будь ласка, введіть 1, 2 або 3.");
                        continue;  // возвращаемся в начало цикла — просим ввести заново
                    }
                    if (choice == "2")
                    {
                        // Показываем товары с номерами
                        Console.WriteLine("Товари в корзині:");
                        int i = 1;
                        List<string> cartItems = new List<string>(cart.Keys);
                        foreach (string item in cartItems)
                        {
                            Console.WriteLine($"{i}. {item} - {cart[item]} шт.");
                            i++;
                        }

                        // Просимо ввести номер
                        Console.Write("Введіть номер товару для видалення: ");
                        string input1 = Console.ReadLine();

                        if (int.TryParse(input1, out int num1) && num1 >= 1 && num1 <= cartItems.Count)
                        {
                            string removeItem = cartItems[num1 - 1];
                            int currentQuantity = cart[removeItem]; // скільки зараз є в корзині

                            Console.Write($"В корзині {currentQuantity} шт. Скільки бажаєте видалити? ");
                            string quantityInput = Console.ReadLine();

                            if (int.TryParse(quantityInput, out int removeCount) && removeCount > 0 && removeCount <= currentQuantity)
                            {
                                // Видаляємо тільки вказане кількість
                                cart[removeItem] -= removeCount;

                                // Якщо кількість стала 0 — видаляємо товар з корзини повністю
                                if (cart[removeItem] == 0)
                                {
                                    cart.Remove(removeItem);
                                    Console.WriteLine($"Товар '{removeItem}' повністю видалено з корзини.");
                                }
                                else
                                {
                                    Console.WriteLine($"Видалено {removeCount} шт. '{removeItem}'. Залишилось {cart[removeItem]} шт.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Невірна кількість. Введіть число від 1 до {currentQuantity}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Невірний номер.");
                        }
                        continue;
                    }
                    else if (choice == "3")
                    {
                        shopping = false;
                        continue;
                    }
                }

                // Вибір товару для додавання
                Console.Write("Введіть номер або назву товару, який хочете придбати: ");
                string input = Console.ReadLine();

                string selectedItem = "";

                // Перевіряємо, номер це чи назва
                if (int.TryParse(input, out int num))
                {
                    if (num >= 1 && num <= itemsList.Count)
                    {
                        selectedItem = itemsList[num - 1];
                    }
                    else
                    {
                        Console.WriteLine("Невірний номер.");
                        continue;
                    }
                }
                else
                {
                    if (shopItems.ContainsKey(input))
                    {
                        selectedItem = input;
                    }
                    else
                    {
                        Console.WriteLine("Товар не знайдено.");
                        continue;
                    }
                }

                // Перевіряємо наявність
                int stock = shopItems[selectedItem].stock;
                if (stock <= 0)
                {
                    Console.WriteLine("Товара немає в наявності.");
                    continue;
                }

                Console.Write($"Скільки штук? (в наявності {stock}): ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0) // ! - не
                {
                    Console.WriteLine("Введіть коректне число.");
                    continue;
                }

                if (quantity > stock)
                {
                    Console.WriteLine($"Доступно тільки {stock} шт.");
                    continue;
                }

                // Добавляемо в корзину
                if (cart.ContainsKey(selectedItem))
                    cart[selectedItem] += quantity;
                else
                    cart[selectedItem] = quantity;

                // Обновляем остаток
                int price = shopItems[selectedItem].price;
                shopItems[selectedItem] = (price, stock - quantity);

                Console.WriteLine($"Товар додано в корзину.");
            }

            // ----- ОПЛАТА -----
            Console.WriteLine("\n=== ОФОРМЛЕННЯ ЗАКАЗУ ===");

            if (cart.Count == 0)
            {
                Console.WriteLine("Корзина пуста. До побачення!");
                return;
            }

            int total = 0;
            foreach (var item in cart)
            {
                total += shopItems[item.Key].price * item.Value;
            }

            Console.WriteLine($"Сумма покупки: {total} грн.");

            // Питаємо про карту
            Console.Write("У вас є карта лояльности? (так/ні): ");
            string cardAnswer = Console.ReadLine();

            // Розрахунок скидки та кешбека
            int cashbackPercent = 0;
            int discountPercent = 0;

            if (total > 2000)
                cashbackPercent = 1;
            if (total > 10000)
                cashbackPercent = 5;
            if (cardAnswer != "так" || cardAnswer != "Так")
            {
                discountPercent = 3;
                if (total > 20000)
                    discountPercent = 5;
            }
            double cashback = total * cashbackPercent / 100.0;
            double discount = total * discountPercent / 100.0;
            double finalTotal = total - discount - cashback;

            Console.WriteLine("\n=== РЕЗУЛЬТАТ ===");
            Console.WriteLine($"Скидка по карті: {discountPercent}% → {discount:F2} грн");
            Console.WriteLine($"Кешбек: {cashbackPercent}% → {cashback:F2} грн");
            Console.WriteLine($"Кінцева сума: {finalTotal:F2} грн");
            Console.WriteLine("\nДякуємо за покупку!");
        }
    }
}