using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab3;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] months = {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            var abiturients = new List<Abiturient>
            {
                new Abiturient("Екатерина", "Петрович", "Дмитриевна", new[] { 9, 8, 10, 9, 8 }, "Минск"),
                new Abiturient("Петр", "Белов", "Андреевич", new[] { 2, 4, 6, 3, 8 }, "Брест"),
                new Abiturient("Валерий", "Федотов", "Дмитриевич", new[] { 10, 5, 4, 7, 9 }, "Мозырь"),
                new Abiturient("Екатерина", "Хомченко", "Сергеевна", new[] { 6, 7, 9, 3, 8 }, "Орша"),
                new Abiturient("Артур", "Романов", "Аркадьевич", new[] { 7, 7, 8, 9, 8 }, "Витебск"),
                new Abiturient("Мария", "Иванова", "Федоровна", new[] { 5, 8, 5, 6, 5 }, "Минск"),
                new Abiturient("Алексей", "Романов", "Иванович", new[] { 6, 2, 4, 4, 4 }, "Ошмяны"),
                new Abiturient("Иван", "Кузьмин", "Максимович", new[] { 1, 6, 3, 6, 5 }, "Брест")
            };

            //1
            int n = 4;
            var result1 = from m in months
                          where m.Length == n //where - фильтрует
                          select m; //синтаксис выражения запроса
            var result2 = months.Where(m => m == "December" || m == "January" || m == "February"
                                         || m == "June" || m == "July" || m == "August"); //точечная нотация (метод расширения)
            var result3 = months.OrderBy(m => m);
            var result4 = months.Count(m => m.Contains("u") && m.Length >= 4);

            PrintResult($"Месяцы с длиной строки {n}: ", result1);
            PrintResult("Только зимние и летние месяцы: ", result2);
            PrintResult("Месяцы в алфавитном порядке: ", result3);
            PrintResult("Количество месяцев, содержащие букву u и длиной не менее 4х символов: ", new[]{result4});

            //2-3
            int markSum = 31;
            
            var abs1 = abiturients.Where(a => a.Marks.Any(m => m < 4));
            var abs2 = abiturients.Where(a => a.Marks.Sum() > markSum);
            int absWith10 = abiturients.Count(a => a.Marks.Any(m => m == 10));
            var abs3 = abiturients.OrderBy(a => a.Surname).ThenBy(a => a.Name).ThenBy(a => a.MiddleName);
            var abs4 = abiturients.OrderBy(a => a.GetAverageMark()).Take(4).Reverse(); //сортировка от меньшего к большему, берем 4 первых, а потом переворачиваем порядок(якобы взяли 4 последних)

            PrintResult("Абитуриенты с неудовлетворительными оценками:", abs1, "\n");
            PrintResult($"Абитуриенты с суммой баллов выше {markSum}:", abs2, "\n");
            PrintResult("Количество абитуриентов с десятками:", new []{absWith10}, "\n");
            PrintResult("Абитуриенты по алфавиту:", abs3, "\n");
            PrintResult("4 последних абитуриента с самой низкой успеваемостью :", abs4, "\n");

            //4
            var result = abiturients.OrderBy(a => a.Name) //сортировка
                .Take(6) //разбиение
                .Where(a => a.Name.Length <= 7) // условие
                .Where(a => a.Marks.Average() >= 6) //условие + агрегирование
                .Select(a => a.Name); //проекция

            PrintResult("Имена:", result, "\n");

            // 5
            var cities = new List<City>
            {
                new City{Name = "Минск", Region = "Минская"},
                new City{Name = "Брест", Region = "Брестская"},
                new City{Name = "Мозырь", Region = "Гомельская"},
                new City{Name = "Орша", Region = "Витебская"},
                new City{Name = "Витебск", Region = "Витебская"},
                new City{Name = "Ошмяны", Region = "Гродненская"}
            };

            var absNotFromMainCities =
            abiturients.Join(cities, a => a.City, c => c.Name, (a, c) => new
            {
                Abiturient = a, City = c
            })
            .Where(t => !City.MainCities.Contains(t.City.Name));

            WriteLineColor("Абитуриенты не из областных центров :", ConsoleColor.DarkMagenta);
            foreach (var ab in absNotFromMainCities)
            {
                Console.WriteLine($"{ab.Abiturient} - {ab.City.Name}, {ab.City.Region} область");
            }


        }

        class City
        {
            public string Name { get; set; }
            public string Region { get; set; }

            public static string[] MainCities = { "Минск", "Могилёв", "Гомель", "Брест", "Гродно", "Витебск" };
        }

        private static void PrintResult<T>(string message, IEnumerable<T> source, string separator = " ")
        {
            WriteLineColor(message, ConsoleColor.DarkMagenta);
            foreach (var item in source)
            {
                Console.Write($"{item.ToString()}{separator}");
            }
            Console.WriteLine();
        }

        private static void WriteLineColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

 

   
}
