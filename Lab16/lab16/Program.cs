using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab16
{
   public class Program
    {
        private static int _primesRange = 2000;
        public static void Main(string[] ars)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var stopwatch = new Stopwatch();

            IEnumerable<int> GeneratePrimeNumbers()
            {
                Console.WriteLine("    [Task output]: Задача выполняется");
                var result = new List<int>();
                foreach (var num in Enumerable.Range(0, _primesRange))
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("    [Task output]: Задача остановлена");
                        break;
                    }
                    if (IsPrime(num))
                    {
                        Thread.Sleep(10);
                        result.Add(num);
                    }
                }
                Console.WriteLine("    [Task output]: Задача завершена");
                return result;
            }

            var t1 = new Task<IEnumerable<int>>(GeneratePrimeNumbers);
            var t2 = new Task<IEnumerable<int>>(GeneratePrimeNumbers);
            var t3 = new Task<IEnumerable<int>>(GeneratePrimeNumbers);


            Console.WriteLine("Первый прогон:");
            RunTaskWithStats(t1, stopwatch, true);

            Console.WriteLine("=======================================");

            Console.WriteLine("Второй прогон:");
            RunTaskWithStats(t2, stopwatch, true);

            Console.WriteLine("=======================================");

            Console.WriteLine("Вариант с токеном отмены:");
            RunTaskWithStats(t3, null, false);

            Console.WriteLine("Поиск простых чисел. Нажмите 'Esc' что бы завершить задачу");
            var k = Console.ReadKey();
            if (k.Key == ConsoleKey.Escape)
            {
                Console.Write("\b"); // backspace
                source.Cancel();
            }
            t3.Wait();

            Console.WriteLine("=======================================");
            var calcX = new Task<int>(GetX);
            var calcY = new Task<int>(GetY);
            var calcZ = new Task<int>(GetZ);
            var calcFormula = new Task<double>(() => Calculate(calcX.Result, calcY.Result, calcZ.Result));

            calcX.GetAwaiter().OnCompleted(() => { Log("GetX Completed"); });
            calcFormula.ContinueWith((t) => { Log($"ID: {t.Id}, Name: Formula, Result: {t.Result}"); });

            calcX.Start();
            calcY.Start();
            calcZ.Start();

            Console.WriteLine($"X={calcX.Result}, Y={calcY.Result}, Z={calcZ.Result}\nВсе параметры готовы!");

            calcFormula.Start();
            Console.WriteLine($"Результат: {calcFormula.Result:F1}"); // 3 знака после запятой

            Console.WriteLine("=======================================");

            int[] arr1 = new int[1000_000];
            int[] arr2 = new int[1000_000];
            int[] arr3 = new int[1000_000];
            int[] arr4 = new int[1000_000];
            var arrs = new[] { arr1, arr2, arr3, arr4 };
            var orderedArrs = new List<int[]>();
            var rnd = new Random();

            Console.WriteLine("Parallel.For - генерация 4х массивов по 1000000 элементов");
            stopwatch.Reset();
            stopwatch.Start();
            Parallel.For(0, 1000000, i =>
            {
                arr1[i] = i;
                arr2[i] = rnd.Next(0, i);
                arr3[i] = rnd.Next(0, i) - arr2[i];
                arr4[i] = rnd.Next(0, i) - arr3[i];
            });
            stopwatch.Stop();
            Console.WriteLine("За время: {0}", stopwatch.Elapsed);

            stopwatch.Reset();
            stopwatch.Start();
            Console.WriteLine("\nFor - генерация 4х массивов по 1000000 элементов");
            for (int i = 0; i < 1000000; i++)
            {
                arr1[i] = i;
                arr2[i] = rnd.Next(0, i);
                arr3[i] = rnd.Next(0, i) - arr2[i];
                arr4[i] = rnd.Next(0, i) - arr3[i];
            }
            stopwatch.Stop();
            Console.WriteLine("За время: {0}", stopwatch.Elapsed);

            stopwatch.Reset();
            Console.WriteLine("\nParallel.ForEach - сортировка 4х массивов по 1000000 элементов");
            stopwatch.Start();
            Parallel.ForEach(arrs, (arr) =>
            {
                orderedArrs.Add(arr.OrderByDescending(e => e).ToArray());
            });
            stopwatch.Stop();
            Console.WriteLine("За время: {0}", stopwatch.Elapsed);

            stopwatch.Reset();
            Console.WriteLine("\nForEach - сортировка 4х массивов по 1000000 элементов");
            stopwatch.Start();
            orderedArrs.Clear();
            foreach (var arr in arrs)
            {
                orderedArrs.Add(arr.OrderByDescending(e => e).ToArray());
            }
            stopwatch.Stop();
            Console.WriteLine("За время: {0}", stopwatch.Elapsed);

            Console.WriteLine("=======================================");

            Console.WriteLine("Парралельное выполение задач:");
            Parallel.Invoke(() =>
                {
                    Factorial(10);
                },
                () =>
                {
                    Console.WriteLine($"ID {Task.CurrentId} - задача чтения из файла");
                    using (var reader = new StreamReader("log.txt"))
                    {
                        var text = reader.ReadToEnd();
                        Thread.Sleep(3000);
                        var lenght = Encoding.UTF8.GetByteCount(text);
                        Console.WriteLine($"ID {Task.CurrentId} - чтение {lenght} байт завершено");
                    }
                });

            Console.WriteLine("=======================================");

            var stock = new BlockingCollection<Product>();
            var producers = new List<Producer>
            {
                new Producer(2000, "Ivan", new Product {Name = "TV"}),
                new Producer(1000, "Alex", new Product { Name = "iPhone" }),
                new Producer(1500, "Kate", new Product { Name = "PlayStation" }),
                new Producer(3000, "Victor", new Product { Name = "Fridge" }),
                new Producer(4000, "Jess", new Product { Name = "Blender" })
            };

            var consumers = new List<Consumer>
            {
                new Consumer("Tom"),new Consumer("Boris"),new Consumer("Casey"),
                new Consumer("Cassandra"),new Consumer("Monica"),new Consumer("Sergey"),
                new Consumer("Maria"),new Consumer("Artour"),new Consumer("Roman"),
                new Consumer("Olga")
            };
            var tasks = new List<Task>();

            foreach (var producer in producers)
            {
                tasks.Add(new Task(() =>
                {
                    producer.Produce(stock);
                }));
            }
            tasks.ForEach((t)=> t.Start());

            foreach (var consumer in consumers)
            {
                Task.Factory.StartNew(() =>
                {
                    consumer.Consume(stock);
                });
            }

            Task.WaitAll(tasks.ToArray());
            stock.CompleteAdding();
            Thread.Sleep(1000);

            Console.WriteLine("=======================================");
            Console.WriteLine("Асинхронное получение простых чисел");

            Task<List<int>> primesTask = GetPrimeNumbersAsync();

            Console.WriteLine("Метод начал асинхронное выполнение");

            List<int> primes = primesTask.GetAwaiter().GetResult();
            Console.WriteLine($"Найдено {primes.Count} простых чисел в диапозоне 0 - {_primesRange}");
        }

        private static void RunTaskWithStats(Task task, Stopwatch stopwatch, bool isAwaitable)
        {
            Console.WriteLine($"Id задачи: {task.Id}");
            Console.WriteLine($"Статус задачи: {task.Status}");

            stopwatch?.Reset();
            stopwatch?.Start();
            task.Start();

            Console.WriteLine($"Статус задачи: {task.Status}");
            Thread.Sleep(100);
            Console.WriteLine($"Статус задачи: {task.Status}");

            if (isAwaitable)
            {
                task.Wait();
                if (stopwatch ==  null) return;
                stopwatch.Stop();
                Console.WriteLine("За время: {0}", stopwatch.Elapsed);
                stopwatch.Reset();
            }
        }

        private static int GetX()
        {
            Console.WriteLine("Вычисление X");
            Random rnd = new Random();
            Thread.Sleep(3000);
            return rnd.Next(50, 500);
        }
        private static int GetY()
        {
            Console.WriteLine("Вычисление Y");
            Random rnd = new Random();
            Thread.Sleep(4000);
            return rnd.Next(30, 300);
        }
        private static int GetZ()
        {
            Console.WriteLine("Вычисление Z");
            Random rnd = new Random();
            Thread.Sleep(2500);
            return rnd.Next(10, 100);
        }

        private static double Calculate(int x, int y, int z)
        {
            Console.WriteLine($"Расчет по формуле sqrt({x}) + ({y}^1.5 * sin({z}))");
            return (Math.Sqrt(x) + (Math.Pow(y, 1.5) * Math.Sin(z)));
        }

        private static void Log(string message)
        {
            var fileMode = FileMode.OpenOrCreate;
            if (File.Exists("log.txt"))
                fileMode = FileMode.Append;

            using (var fs = new FileStream("log.txt", fileMode, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine(message);
            }
        }

        private static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i * i <= number; i += 2)
                if (number % i == 0) return false;
            return true;
        }

        static void Factorial(int x)
        {
            int result = 1;
            Console.WriteLine($"ID {Task.CurrentId} - задача получения факториала {x}");
            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Thread.Sleep(3000);
            Console.WriteLine($"ID {Task.CurrentId} - результат {result}");
        }

        private static async Task<List<int>> GetPrimeNumbersAsync()
        {
            return await Task.Run(() =>
            {
                Console.WriteLine("    [Task output]: Задача выполняется");
                var result = new List<int>();
                foreach (var num in Enumerable.Range(0, _primesRange))
                {
                    if (IsPrime(num))
                    {
                        Thread.Sleep(10);
                        result.Add(num);
                    }
                }
                Console.WriteLine("    [Task output]: Задача завершена");
                return result;
            });
        }
    }
}
