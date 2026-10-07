using System;
namespace DZ2._5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Random random = new Random();
            int[] array = new int[20];
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(10, 100);
            }
            for(int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
            // Знаходимо максимальну суму трьох сусідніх елементів
            int maxsum = 0;
            for(int i = 0; i < array.Length - 2; i++)
            {
                int sum = array[i] + array[i + 1] + array[i + 2];
                if (sum > maxsum)
                {
                    maxsum = sum;
                }
            }
            Console.WriteLine("Максимальная сумма трьох сосідніх елементів: " + maxsum);
            // Знаходимо найбільшу сумму з трьох елементів, які не є сусідніми
            int maxnumi1 = 0;
            int maxnumi2 = 0;
            int maxnumi3 = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > array[maxnumi1])
                {
                    maxnumi3 = maxnumi2;
                    maxnumi2 = maxnumi1;
                    maxnumi1 = i;
                }
                else if (array[i] > array[maxnumi2] && array[i] != array[maxnumi1])
                {
                    maxnumi3 = maxnumi2;
                    maxnumi2 = i;
                }
                else if (array[i] > array[maxnumi3] && array[i] != array[maxnumi1] && array[i] != array[maxnumi2])
                {
                    maxnumi3 = i;
                }
            }
            Console.WriteLine($"Найбільша сума трьох елементів, які не є сусідніми: {array[maxnumi1]}+{array[maxnumi2]}+{array[maxnumi3]}={array[maxnumi1] + array[maxnumi2] + array[maxnumi3]}");
        }
    }
}
