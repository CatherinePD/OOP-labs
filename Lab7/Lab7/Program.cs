using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.Exceptions;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            int exeptionCounter = 0;

            try
            {
                // инициализация объектов
                var stone1 = new Stone {Color = "Белый", Price = 20, Status = ProductStatus.Ready, Weight = 100};
                var pStone1 = new PreciousStone { Color = "Желтый", Price = 40, Status = ProductStatus.Ready, Weight = 40};
                var ruby1 = new Ruby {Price = 57, Status = ProductStatus.None, Weight = 30};
                var diamond1 = new Diamond { Price = 89, Status = ProductStatus.Ready, Weight = 28 };
                var spStone1 = new SemiPreciousStone { Color = "Зелёный", Price = 30, Status = ProductStatus.None, Weight = 41 };
                var nefrit1 = new Nefrit { Price = 50, Status = ProductStatus.Ready, Weight = 44 };

                List<Stone> stones = new List<Stone> { stone1, spStone1, pStone1, nefrit1, ruby1, diamond1};
                var necklace = new Necklace(stones);
                var controller = new NecklaceController(necklace);


                Console.WriteLine("Тест диапазона (ArgumentRangeException)");
                try { controller.GetStonesByOpacity(3, 1); }
                catch (ArgumentRangeException e)
                {
                    exeptionCounter++;
                    Console.WriteLine($"Обработано исключение: {e}");
                }

                Console.WriteLine("\nТест отрицательного аргумента (ArgumentRangeException)");
                try { diamond1.DamageTest(-10); }
                catch (NegativeArgumentException e)
                {
                    exeptionCounter++;
                    Console.WriteLine($"Обработано исключение: {e}");
                }

                Console.WriteLine("\nТест на пустую или null-строку (StringNullOrEmptyException)");
                try
                {
                    ruby1.Color = null;
                    ruby1.Color = "";
                }
                catch (StringNullOrEmptyException e)
                {
                    exeptionCounter++;
                    Console.WriteLine($"Обработано исключение: {e}");
                }

                Console.WriteLine("\nТест на вхождение индекса в диапазон (IndexOutOfRangeException  - стандартное исключение)");
                try { necklace[10] = new Stone(); }
                catch (IndexOutOfRangeException e)
                {
                    exeptionCounter++;
                    Console.WriteLine($"Обработано исключение: {e}");
                }

                Console.WriteLine("\nТест на null-аргумент (ArgumentNullException - стандартное исключение)");
                try { necklace.Add(null); }
                catch (ArgumentNullException e)
                {
                    exeptionCounter++;
                    Console.WriteLine($"Обработано исключение: {e}");
                }

                Console.WriteLine("Тест Debug.Assert");
                necklace.AddRange(new List<Stone>());

            }
            catch (Exception e)
            {
                exeptionCounter++;
                Console.WriteLine($"Произошло непредвиденное исключение: {e}");
            }
            finally
            {
                Console.WriteLine($"\nПроизошло {exeptionCounter} исключительных ситуаций");
                Console.WriteLine("Программа завершает работу...");
            }
        }
    }
}
