using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomat
{
    class ATM
    {
        private const int MAX_NOTES_PER_WITHDRAW = 30;

        // Сховище купюр: номінал → кількість
        private Dictionary<int, int> cassettes;

        // Доступні номінали купюр
        private readonly int[] denominations = { 50, 100, 200, 500, 1000 };

        public ATM()
        {
            this.cassettes = new Dictionary<int, int>();
            Random random = new Random();

            foreach (int d in denominations)
            {
                this.cassettes[d] = random.Next(0, 101);   // від 0 до 100 купюр
            }
        }

        // Показати стан каси (для налагодження / меню)
        public void ShowCassettes()
        {
            Console.WriteLine("Стан каси банкомата:");
            foreach (var pair in this.cassettes)
                Console.WriteLine($"  {pair.Key} грн — {pair.Value} шт.");
        }

        // Перевіряє, чи можна видати суму, і якщо так — повертає план видачі
        public Dictionary<int, int> PlanWithdraw(decimal amount)
        {
            if (amount <= 0 || amount % 50 != 0)
                throw new Exception("Сума має бути кратна 50.");

            int remaining = (int)amount;
            Dictionary<int, int> plan = new Dictionary<int, int>();
            int totalNotes = 0; // для перевірки ліміту кількості купюр

            // Ідем від крупних до малих
            for (int i = denominations.Length - 1; i >= 0; i--)
            {
                int denom = denominations[i];

                if (remaining < denom) continue; // якщо залишок менший за номінал, пропускаємо цей номінал

                int need = remaining / denom;
                int available = this.cassettes[denom];
                int take = Math.Min(need, available);

                if (take > 0)
                {
                    plan[denom] = take;
                    remaining -= take * denom;
                    totalNotes += take;
                }
            }

            if (remaining > 0)
                throw new Exception("Банкомат не може видати таку суму (немає потрібних купюр).");

            if (totalNotes > MAX_NOTES_PER_WITHDRAW)
                throw new Exception($"Перевищено ліміт у {MAX_NOTES_PER_WITHDRAW} купюр за одну операцію.");

            return plan;
        }

        // Видача грошей: спочатку плануємо, потім списуємо купюри
        public Dictionary<int, int> Withdraw(decimal amount)
        {
            Dictionary<int, int> plan = this.PlanWithdraw(amount);

            foreach (var pair in plan)
            {
                this.cassettes[pair.Key] -= pair.Value;
            }

            return plan;
        }
    }
}
