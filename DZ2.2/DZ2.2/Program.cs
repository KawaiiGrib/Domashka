using System;
namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Random random = new Random();
            Console.Write("Введіть кількість елементів масиву:");
            int n = Convert.ToInt32(Console.ReadLine());
            int[] array = new int[n];
            Console.Write("Введіть найменьший елемент масиву:");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введіть найбільший елемент масиву:");
            int max = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(min, max + 1);
            }
            Console.WriteLine("Масив:");
            for (int i = 0; i < n; i++)
            {
                Console.Write(array[i] + " ");
            }
            List<int> list = new List<int>(array);
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (list[i] == list[j])
                    {
                        list.RemoveAt(j);
                        j--;
                        n--;
                    }
                }
            }
            Console.WriteLine("\nМасив без повторюваних елементів:");
            for (int i = 0; i < n; i++)
            {
                Console.Write(list[i] + " ");
            }
        }
    }
}