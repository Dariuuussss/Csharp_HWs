using System;

namespace Task2_1
{
    public interface ICombatant
    {
        string Name { get; }
        int HP { get; set; }
        bool IsDead { get; }
        int Damage { get; }
        void Attack(ICombatant target);
        void TakeDamage(int amount);
    }

    public class Knight : ICombatant
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Damage { get; } = 25;
        public bool IsDead { get { return HP <= 0; } }

        public Knight(string name)
        {
            Name = name;
            HP = 150;
        }

        public void Attack(ICombatant target)
        {
            if (IsDead)
            {
                Console.WriteLine($"Рыцарь {Name} не может атаковать: он мёртв.");
                return;
            }

            if (target.IsDead)
            {
                Console.WriteLine($"Рыцарь {Name} не может атаковать мёртвую цель.");
                return;
            }

            Console.WriteLine($"Рыцарь {Name} атакует!");
            target.TakeDamage(Damage);
        }

        public void TakeDamage(int amount)
        {
            HP = Math.Max(0, HP - amount);
            Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
        }
    }

    public class Mage : ICombatant
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Damage { get; } = 40;
        public bool IsDead { get { return HP <= 0; } }

        public Mage(string name)
        {
            Name = name;
            HP = 80;
        }

        public void Attack(ICombatant target)
        {
            if (IsDead)
            {
                Console.WriteLine($"Маг {Name} не может атаковать: он мёртв.");
                return;
            }

            if (target.IsDead)
            {
                Console.WriteLine($"Маг {Name} не может атаковать мёртвую цель");
                return;
            }

            Console.WriteLine($"Маг {Name} атакует двойным заклинанием!");
            target.TakeDamage(Damage);

            if (!target.IsDead)
            {
                target.TakeDamage(Damage);
            }
        }

        public void TakeDamage(int amount)
        {
            HP = Math.Max(0, HP - amount);
            Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Knight knight = new Knight("Артур");
            Mage mage = new Mage("Мерлин");

            Console.WriteLine("Начинается бой!\n");

            while (!knight.IsDead && !mage.IsDead)
            {
                knight.Attack(mage);
                if (mage.IsDead)
                {
                    break;
                }

                mage.Attack(knight);
                Console.WriteLine();
            }

            if (knight.IsDead)
            {
                Console.WriteLine($"Победил маг {mage.Name}!");
            }
            else
            {
                Console.WriteLine($"Победил рыцарь {knight.Name}!");
            }
        }
    }
}
