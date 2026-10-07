using System;
namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Random random = new Random();
            int[] masiv = new int[1000];
            for (int i = 0; i < masiv.Length; i++)
            {
                masiv[i] = random.Next(1, 11);
            }
            int[] k = new int[1000];
            for (int j = 0; j < masiv.Length; j++)
            {
                Console.Write(masiv[j] + " ");
                for (int i = 0; i < masiv.Length; i++)
                {
                    if (masiv[j] == masiv[i])
                    {
                        k[j]++;
                    }
                }
            }
            Console.WriteLine();
            int min = k[0];
            int num = masiv[0];
            for (int i = 1; i < k.Length; i++)
            {
                if (k[i] < min)
                {
                    min = k[i];
                    num = masiv[i];
                }
            }
            Console.WriteLine("Найменьша кількість повторень: " + min);
            Console.WriteLine("Число, яке зустрічається найменшу кількість разів: " + num);
        }
    }
}