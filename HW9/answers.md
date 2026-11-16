\# Домашнее задание 9

\## Сериализация. Потоки. Чтение-запись



\## Задача 1



\### а) При стандартной сериализации в JSON попадут только Name и Score, потому что это публичные свойства с get и set.

Поле secretCode не попадет, потому что оно приватное. Поле IsAlive тоже не попадет, потому что System.Textx.Json по умолчанию не сериализует поля, даже публичные



Результат:



{

&#x09;"Name": "Alex",

&#x09;"Score": 100

}



\### б) Можно создать публичное свойство для чтения приватного поля:



public class Player

{

&#x09;public string Name { get; set; }

&#x09;public int Score { get; set; }

&#x09;private int secretCode = 42;

&#x09;public int SecretCode => secretCode;

&#x09;public bool IsAlive;

}



Поле secretCode останется приватным, но его значение будет попадать в JSON через публичное свойство SecretCode



\### в) 1 способ. Заменить публичное поле на публичное свойство:



public bool IsAlive { get; set; }



2 способ. Включить сериализацию полей через JsonSerializerOptions:



var options = new JsonSerializerOptions

{

&#x09;IncludeFields = true

};

string json = JsonSerializer.Serialize(p, options);



В первом случае IsAlive будет сериализоваться как обычное свойство. Во втором случае JsonSerializer начнет включать в JSON публичные поля объекта, в том числе IsAlive



\## Задача 2



\### а) Класс для сериализации:



public class Game

{

&#x09;public string Title { get; set; }

&#x09;public int Year { get; set; }

&#x09;public double Rating { get; set; }

&#x09;public string\[] Genres { get; set; }

&#x09;public bool IsMultiplayer { get; set; }

}



Здесь типы соответствуют данным JSON:

string - название игры

int - год выпуска

double - рейтинг с дробной частью

string\[] - массив жанров

bool - наличие



Десериализация:



Game game = JsonSerializer.Deserialize<Game>(json);



\### б) По умолчанию System.Text.Json учитывает регистр букв в названиях свойств. Поэтому title и Title считаются разными именами, и свойство Title получает значение null



Нужно установить PropertyNameCaseInsensitive = true:



var options = new JsonSerializerOptions

{

&#x09;PropertyNameCaseInsensitive = true

};

Game game = JsonSerializer.Deserialize<Game>(json, options);



Теперь десериализатор будет игнорировать регистр букв, и значение title корректно попадёт в свойство Title



\### в) Да, это валидный JSON. Массив JSON может содержать элементы разных типов: число, строку, логическое значение и null



Его можно десериализовать в массив JsonElement\[]:



string json = "\[1, \\"hello\\", true, null]";

JsonElement\[] data = JsonSerializer.Deserialize<JsonElement\[]>(json);



Тип JsonElement позволяет представлять значения разных типов JSON в одном массиве



\## Задача 3



\### 1) Ответ: File



Для небольшого файла удобнее использовать File.ReadAllText(), так как он позволяет прочитать всё содержимое одной командой. Использовать поток здесь нет необходимости



\### 2) Ответ: File



Данные о прогрессе игрока можно сериализовать в JSON и сохранить с помощью File.WriteAllText(). Этот способ проще, если при каждом автосохранении файл полностью перезаписывается



\### 3) Ответ: FileStream



Для большого лог-файла лучше использовать FileStream, поскольку он позволяет записывать данные постепенно, не загружая весь файл в память. Для записи текстовых строк его можно использовать вместе со StreamWriter в режиме добавления в конец файла



\### 4) Ответ: File



Можно сначала проверить наличие файла с помощью File.Exists(), а затем прочитать содержимое через File.ReadAllText(). Для этих действий не нужно вручную управлять потоком



\### 5) Ответ: FileStream



FileStream позволяет переместиться в нужное место файла с помощью Seek() и перезаписать только необходимые байты. Благодаря этому не придётся читать и перезаписывать весь файл

