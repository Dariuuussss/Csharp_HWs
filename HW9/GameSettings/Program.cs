using System;
using System.IO;
using System.Text.Json;

string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

if (File.Exists(path))
{
    string json = File.ReadAllText(path);
    GameSettings? settings = JsonSerializer.Deserialize<GameSettings>(json);

    if (settings != null)
    {
        Console.WriteLine("--- Текущие настройки ---");
        Console.WriteLine($"Имя игрока: {settings.PlayerName}");
        Console.WriteLine($"Сложность: {settings.Difficulty}");
        Console.WriteLine($"Громкость музыки: {settings.MusicVolume}");
    }
}
else
{
    Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");

    Console.Write("Имя игрока: ");
    string name = Console.ReadLine() ?? "";

    int difficulty;
    do
    {
        Console.Write("Сложность (1 - легко, 2 - нормально, 3 - сложно): ");
    }
    while (!int.TryParse(Console.ReadLine(), out difficulty)
           || difficulty < 1 || difficulty > 3);

    int volume;
    do
    {
        Console.Write("Громкость музыки (0-100): ");
    }
    while (!int.TryParse(Console.ReadLine(), out volume)
           || volume < 0 || volume > 100);

    GameSettings settings = new GameSettings
    {
        PlayerName = name,
        Difficulty = difficulty,
        MusicVolume = volume
    };

    JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    string json = JsonSerializer.Serialize(settings, options);
    File.WriteAllText(path, json);

    Console.WriteLine("Настройки сохранены в settings.json");
}

public class GameSettings
{
    public string PlayerName { get; set; } = "";
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }
}