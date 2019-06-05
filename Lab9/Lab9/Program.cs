using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public class Program
    {
        static void Main(string[] args)
        { 
            
            var user = new User("Катя");

            var notepad = new Software("Блокнот", 1.3);
            var paint = new Software("Paint", 2.0);
            var browser = new Software("Google Chrome", 2.7);

            var userSoftware = new[] {notepad, paint, browser};

            Console.WriteLine("Программы пользователя:");
            foreach (var soft in userSoftware)
            {
                soft.Subscribe(user); // подписка программы на пользователя
                Console.WriteLine(soft);
            }

            Console.WriteLine("\nОбновление");
            user.CheckForUpdates(true); // Проверить обновления (внутри вызывает Событие Upgrade)

            Console.WriteLine("\nНачать работу");
            user.StartWorking(); // Начать работу (внутри вызывает Событие Work)

            Console.WriteLine("\nПрограммы пользователя");
            foreach (var soft in userSoftware)
            {
                Console.WriteLine(soft);
            }

            //=====================================================================================//

            Console.WriteLine("\nЗадание 2");

            //создаем лямбды для работы со строками
            //Func<T1,T2> - обобщенный делегат, T1 - тип входного параметра, T2 - тип возвращвемого
            Func<string, string> toUpper = str => str.ToUpper();  // все заглавными
            Func<string, string> removeSpaces = str => str.Replace(" ", ""); // удалить пробелы
            Func<string, string> addBrackets = str => "[" + str + "]"; // добавить скобки
            Func<string, string> reverse = str => // перевернуть строку
            {
                char[] charArray = str.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            };
            Func<string, string> removePunctuations = str => str.Replace(".", "").Replace(",", "") // удалить знаки препинания
                                                                .Replace("-", "").Replace(":", "")
                                                                .Replace(";", "").Replace("?", "")
                                                                .Replace("!", "");

            var text = "Изучать C# очень увлекательно! Язык удобен, относительно понятен. Предоставляет множество интересного - в нем есть классы; методы; объекты; интерфейсы; делегаты!!!";

            Console.WriteLine($"Исходный текст: {text}");

            var result = CustomStringHandler(text, new []{toUpper, removePunctuations, removeSpaces, reverse, addBrackets });

            Console.WriteLine($"\nПосле обработки: {result}");
        }

        public static string CustomStringHandler(string text, Func<string, string>[] functions)
        {
            foreach (var func in functions)
            {
                text = func(text);
            }

            return text;
        }
    }
}
