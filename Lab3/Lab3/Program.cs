using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassDesign;

namespace Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            var a = new Abiturient("Екатерина", "Петрович", "Дмитриевна", "Громова", "1234567", new[] { 9, 8, 10, 9, 8 });
            var b = new Abiturient("Валерий", "Коклюшкин", "Аристархович");
            var c = new Abiturient("Виктор", "Петучевский", "Геннадьевич", "ул. Пушкина, дом Колотушкина");

            var x1 = new Abiturient("Мария", "Петрова", "Витальевна");
            var x2 = new Abiturient("Мария", "Петрова", "Витальевна");

            b.Marks = new[] { 7, 6, 4, 9, 6 };
            c.Phone = "88005553535";

            a.PrintFullname(out var fullname_a);
            b.PrintFullname(out var fullname_b);
            Console.WriteLine($"Максимальная оценка {fullname_a}: {a.GetMaxMark()}");
            Console.WriteLine($"Средний балл {fullname_b}: {b.GetAverageMark()}");

            Console.WriteLine($"x1 и x2 равны: {x1.Equals(x2)}");

            x1.Phone = "9999";

            Console.WriteLine($"а теперь x1 и x2 равны: {x1.Equals(x2)}");

            Console.WriteLine($"Тип x1 {x1.GetType()}");

            x1.Marks = new[] { 5, 4, 9, 5, 6 };


            Abiturient[] abs = { a, b, c, x1, x2 };

            Console.WriteLine("Неудовлетворительные оценки у:");
            foreach (var abiturient in abs)
            {
                if (abiturient.Marks.Any(m => m < 4))
                    Console.WriteLine($"{abiturient.Name} {abiturient.Surname}");
            }

            Console.WriteLine("Абитуриенты с суммой баллов выше 40:");
            foreach (var abiturient in abs)
            {
                if (abiturient.Marks.Sum() > 40)
                    Console.WriteLine($"{abiturient.Name} {abiturient.Surname}");
            }

            Console.WriteLine(Abiturient.GetMetaInfo());

            var aType = new { Name = "Анастасия", Surname = "Шевцова", Middlename = "Дмитриевна", Address = "Волгоградская 65-8", Phone = "01010101", Marks = new int[] { 9, 8, 10, 7, 9 } };

            Console.WriteLine($"Экземпляр анонимного типа: {aType.Name} {aType.Surname}; адрес: {aType.Address}, телефон: {aType.Phone}");
        }
    }
}
