using System;

namespace Task2_3
{
    public interface IDamageCalculator
    {
        int CalculateDamage(int baseDamage, int targetArmor);
    }

    public class PhysicalDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int damage = baseDamage - targetArmor / 2;
            return Math.Max(1, damage);
        }
    }

    public class MagicDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int damage = baseDamage - targetArmor / 4;
            damage = Math.Max(1, damage);

            if (targetArmor == 0)
            {
                damage *= 2;
            }

            return damage;
        }
    }

    public class FireDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int damage = baseDamage - targetArmor / 10;
            damage = Math.Max(1, damage);

            if (baseDamage > 50)
            {
                damage += 10;
            }

            return damage;
        }
    }

    public class Unit
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Armor { get; }

        public Unit(string name, int hp, int armor)
        {
            Name = name;
            HP = hp;
            Armor = armor;
        }
    }

    public class DamageSystem
    {
        private IDamageCalculator calculator;

        public DamageSystem(IDamageCalculator calculator)
        {
            this.calculator = calculator;
        }

        public void ApplyDamage(Unit target, int baseDamage)
        {
            int damage = calculator.CalculateDamage(baseDamage, target.Armor);
            target.HP = Math.Max(0, target.HP - damage);
            Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Unit unit = new Unit("Голем", 300, 20);
            int baseDamage = 60;

            Console.WriteLine($"Цель: {unit.Name}, HP: {unit.HP}, броня: {unit.Armor}");
            Console.WriteLine($"Базовый урон каждой атаки: {baseDamage}\n");

            DamageSystem physicalSystem = new DamageSystem(new PhysicalDamageCalculator());
            DamageSystem magicSystem = new DamageSystem(new MagicDamageCalculator());
            DamageSystem fireSystem = new DamageSystem(new FireDamageCalculator());

            Console.WriteLine("Физическая атака:");
            physicalSystem.ApplyDamage(unit, baseDamage);

            Console.WriteLine("\nМагическая атака:");
            magicSystem.ApplyDamage(unit, baseDamage);

            Console.WriteLine("\nОгненная атака:");
            fireSystem.ApplyDamage(unit, baseDamage);
        }
    }
}
