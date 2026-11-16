using System.IO;
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

string savePath = Path.Combine(AppContext.BaseDirectory, "save.json");

File.WriteAllText(savePath, json);
Console.WriteLine("\nПерсонаж сохранён в save.json");

string jsonFromFile = File.ReadAllText(savePath);
GameCharacter? savedCharacter = JsonSerializer.Deserialize<GameCharacter>(jsonFromFile);

if (savedCharacter != null)
{
    Console.WriteLine("\n--- Загруженный персонаж ---");
    Console.WriteLine($"Имя: {savedCharacter.Name}");
    Console.WriteLine($"Уровень: {savedCharacter.Level}");
    Console.WriteLine($"Здоровье: {savedCharacter.Health}");
    Console.WriteLine($"Инвентарь: {string.Join(", ", savedCharacter.Inventory)}");
}

public class GameCharacter
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
}