using System;
using System.Collections.Generic;

namespace Task2_2
{
    public enum Rarity
    {
        Common,
        Rare,
        Legendary
    }

    public interface IUsableItem
    {
        string Name { get; }
        string Description { get; }
        Rarity Rarity { get; }
        void Use(Player player);
    }

    public class Player
    {
        public string Name;
        public int HP;
        public int Damage;

        public Player(string name)
        {
            Name = name;
            HP = 100;
            Damage = 10;
        }

        public void Heal(int amount)
        {
            HP += amount;
        }

        public void IncreaseDamage(int amount)
        {
            Damage += amount;
        }
    }

    public class HealthPotion : IUsableItem
    {
        public string Name { get { return "Зелье здоровья"; } }
        public string Description { get { return "Восстанавливает 50 HP"; } }
        public Rarity Rarity { get { return Rarity.Common; } }

        public void Use(Player player)
        {
            player.Heal(50);
            Console.WriteLine("Вы выпили зелье и восстановили 50 HP.");
        }
    }

    public class Sword : IUsableItem
    {
        public string Name { get { return "Меч"; } }
        public string Description { get { return "Увеличивает урон на 10"; } }
        public Rarity Rarity { get { return Rarity.Rare; } }

        public void Use(Player player)
        {
            player.IncreaseDamage(10);
            Console.WriteLine("Вы использовали меч и увеличили урон на 10.");
        }
    }

    public class AncientScroll : IUsableItem
    {
        public string Name { get { return "Древний свиток"; } }
        public string Description { get { return "Отнимает 20 HP и добавляет 30 урона"; } }
        public Rarity Rarity { get { return Rarity.Legendary; } }

        public void Use(Player player)
        {
            player.HP = Math.Max(0, player.HP - 20);
            player.IncreaseDamage(30);
            Console.WriteLine("Свиток проклял вас: -20 HP, но ваш урон вырос на 30!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Герой");

            List<IUsableItem> items = new List<IUsableItem>
            {
                new HealthPotion(),
                new Sword(),
                new AncientScroll()
            };

            Console.WriteLine($"Игрок: {player.Name}. HP: {player.HP}, урон: {player.Damage}\n");

            foreach (IUsableItem item in items)
            {
                Console.WriteLine($"Предмет: {item.Name} ({item.Rarity})");
                Console.WriteLine(item.Description);
                item.Use(player);
                Console.WriteLine();
            }

            Console.WriteLine("Итоговые характеристики:");
            Console.WriteLine($"Имя: {player.Name}");
            Console.WriteLine($"HP: {player.HP}");
            Console.WriteLine($"Урон: {player.Damage}");

            if (player.HP > 0)
            {
                Console.WriteLine("Состояние: жив");
            }
            else
            {
                Console.WriteLine("Состояние: мёртв");
            }
        }
    }
}
