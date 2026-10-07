using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("How many steps do you want to take per day? ");
            int goal = Convert.ToInt32(Console.ReadLine());
            Console.Write("How many steps have you taken today? ");
            int steps = Convert.ToInt32(Console.ReadLine());
            if (goal >0 && steps > 0)
            {
                int persent = steps * 100 / goal;
                Console.WriteLine($"You have completed {persent}% of your daily goal.");
                if (persent <= 70)
                {
                    Console.WriteLine("You need to walk more. Go for a walk!");
                }
                if (persent > 70 && persent <= 90)
                {
                    Console.WriteLine("You're doing great!");
                }
                if (persent > 90 && persent < 99)
                {
                    Console.WriteLine("You're almost there!");
                }
                if (persent >= 100 && persent < 200)
                {
                    Console.WriteLine("Congratulations! You've reached your goal.");
                }
                if (persent >= 200)
                {
                    Console.WriteLine("Wow! You've exceeded your goal by a lot!");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number of steps.");
            }
        }
    }
}