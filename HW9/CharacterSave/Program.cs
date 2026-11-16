using System;
using System.Text.Json;

GameCharacter character = new GameCharacter
{
    Name = "Alex",
    Level = 5,
    Health = 87.5,
    Inventory = new string[] { "Меч", "Щит", "Зелье здоровья" }
};

JsonSerializerOptions options = new JsonSerializerOptions
{
    WriteIndented = true
};

string json = JsonSerializer.Serialize(character, options);

Console.WriteLine("--- JSON персонажа ---");
Console.WriteLine(json);

GameCharacter? loadedCharacter = JsonSerializer.Deserialize<GameCharacter>(json);

if (loadedCharacter != null)
{
    Console.WriteLine("\n--- Восстановленный персонаж ---");
    Console.WriteLine($"Имя: {loadedCharacter.Name}");
    Console.WriteLine($"Количество предметов: {loadedCharacter.Inventory.Length}");
}

public class GameCharacter
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
}