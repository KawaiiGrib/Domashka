using System;
namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.Write("Введіть кількість елементів масиву: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Помилка: введено некоректне значення.");
                return;
            }
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(100, 901);
            }
            for (int i = 0; i < n; i++)
            {
                Console.Write(array[i] + " ");
            }
            List<int> indexes = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    if (array[i] >= array[i + 1])
                    {
                        indexes.Add(i);
                    }
                }
                else if (i == n - 1)
                {
                    if (array[i] >= array[i - 1])
                    {
                        indexes.Add(i);
                    }
                }
                else
                {
                    if (array[i] >= array[i - 1] && array[i] >= array[i + 1])
                    {
                        indexes.Add(i);
                    }
                }
            }
            Console.WriteLine();
            for (int i = 0; i < indexes.Count; i++)
            {
                Console.WriteLine($"Індекс піку: {indexes[i]}, значення: {array[indexes[i]]}");
            }
        }
    }
}