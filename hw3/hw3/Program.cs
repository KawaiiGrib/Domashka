using System;
using System.Collections.Generic;
namespace Energy
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("Введіть кількість спожитих кіловат-годин (кВт·год): ");
            int kilowattHours = Convert.ToInt32(Console.ReadLine());
            double price = 0;
            if (kilowattHours.GetType() == typeof(int) && kilowattHours > 0)
            {
                if (kilowattHours > 0 && kilowattHours <= 100)
                {
                    price = kilowattHours * 1.44;

                }
                else
                {
                    price = 100 * 1.44;
                    double rest = kilowattHours - 100;
                    if (rest <=500)
                    {
                        price += rest * 1.68;
                    }
                    else
                    {
                        price += 500 * 1.68;
                        rest = rest - 500;
                        price += rest * 1.92;
                    }
                }
                Console.WriteLine($"Вартість електроенергії: {price:F2} грн");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Введено некоректне значення кількості спожитих кіловат-годин.");
                Console.ReadKey();
            }
        }
    }
}