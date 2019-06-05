using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Threading;
using ThreadState = System.Threading.ThreadState;

namespace Lab15
{
    class Program
    {
        static void Main(string[] args)
        {
            DeleteData();
            // ---------------1-------------
            string format = "{0,-6}| {1,-38}| {2,-9}| {3,-10:HH:mm}| {4,-8}| {5:hh\\:mm\\:ss}";
            Console.WriteLine(format, "Id", "Name", "Priority", "Start Time", "State", "Processor Time");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            foreach (Process proc in Process.GetProcesses())
            {
                try
                {
                    var running = proc.Responding
                        ? "Running" : "Stopped";
                    Console.WriteLine(format, proc.Id, proc.ProcessName, proc.BasePriority, proc.StartTime, running, proc.TotalProcessorTime);
                }
                catch (Exception) { continue; }
            }
            // ---------------2-------------
            Console.WriteLine("\nДомен приложения: ");
            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine($"Name: {domain.FriendlyName}");
            Console.WriteLine($".NET version: {domain.SetupInformation.TargetFrameworkName}");
            Console.WriteLine($"Config file: {domain.SetupInformation.ConfigurationFile}");
            Console.WriteLine("Загруженные сборки: ");
            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
                Console.WriteLine(asm.GetName().Name);

            Console.WriteLine("Новый домен: ");
            AppDomain newD = AppDomain.CreateDomain("Brand New App Domain");
            newD.Load("System.IO");
            Console.WriteLine($"Name: {newD.FriendlyName}");
            assemblies = newD.GetAssemblies();
            foreach (Assembly asm in assemblies)
                Console.WriteLine(asm.GetName().Name);
            AppDomain.Unload(newD);

            // ---------------3-------------
            Console.Write("\nВведите N: ");
            var num = int.Parse(Console.ReadLine());

            Thread work = new Thread(PrintPrimeNumbers);
            work.Name = "Работяга";

            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nУправление потоком:");
                Console.WriteLine("1. Запуск потока");
                Console.WriteLine("2. Приостановка потока");
                Console.WriteLine("3. Возобновление потока");
                Console.WriteLine("4. Завершение потока");

                var option = int.Parse(Console.ReadLine());

                if (!flag || work.ThreadState == ThreadState.Aborted || work.ThreadState == ThreadState.Stopped) break;

                switch (option)
                {
                    case 1:
                        work.Start(num);
                        Console.WriteLine("Потока запущен");
                        break;
                    case 2:
                        work.Suspend();
                        Console.WriteLine("Потока Приостановлен");
                        break;
                    case 3:
                        work.Resume();
                        Console.WriteLine("Потока Возобновлен");
                        break;
                    case 4:
                        work.Abort();
                        Console.WriteLine("Потока Завершен");
                        break;
                }

                if (!work.IsAlive)
                    flag = false;
            }
            // ---------------4-------------
            Console.WriteLine("\nСоздание двух потоков:");
            Console.WriteLine("Введите N:");
            num = int.Parse(Console.ReadLine());

            Console.WriteLine("\nСначала четные, потом нечетные:");
            Thread odds = new Thread(Odd);
            Thread evens = new Thread(Even);

            evens.Start(num);
            odds.Start(num);
            evens.Join(); // текущий ждет evens

            odds.Join();

            Console.WriteLine("\nПоследовательно:"); //убрать lock с even
            Thread odds2 = new Thread(Odd);
            Thread evens2 = new Thread(Even);

            odds2.Priority = ThreadPriority.AboveNormal;

            odds2.Start(num);
            evens2.Start(num);

            odds2.Join();
            evens2.Join();

            //-------------------5-------------- -
            Console.WriteLine("\nТаймер:");
            var callback = new TimerCallback(PrintCurrentTime);

            var timer = new Timer(callback, null, 0, 4000);
            Console.ReadLine();
        }

        private static void PrintPrimeNumbers(object o)
        {
            PrintThreadInfo();

            int num = Convert.ToInt32(o);
            var numbers = Enumerable.Range(0, num).ToList();

            Console.WriteLine($"\nПростые числа от 0 до {num}: ");
            using (var writer = new StreamWriter("prime.txt"))
            {
                foreach (var n in numbers)
                {
                    Thread.Sleep(75);
                    if (IsPrime(n))
                    {
                        Output($"{n} ", new Action<string>[] {writer.Write, Console.Write});
                    }
                }
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

        private static void Output(string message, IEnumerable<Action<string>> outputHandlers)
        {
            foreach (var write in outputHandlers)
            {
                write(message);
            }
        }

        private static void PrintThreadInfo()
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"Имя потока: {thread.Name}");
            Console.WriteLine($"Статус потока: {thread.ThreadState}");
            Console.WriteLine($"Приоритет потока: {thread.Priority}");
            Console.WriteLine($"Id потока: {thread.ManagedThreadId}");
        }
        static string objlocker = "null";
        private static void Odd(object num)
        {
            lock (objlocker)
            {
                for (int i = 0; i <= (int)num; i++)
                {
                    if (i % 2 != 0)
                    {
                        Output($"{i} ", new Action<string>[] { WriteFile, Console.Write });
                    }
                    Thread.Sleep(200);
                }
            }
        }

        private static void Even(object num)
        {
            lock (objlocker)
            {
                for (int i = 0; i <= (int)num; i++)
                {
                    if (i % 2 == 0)
                    {
                        Output($"{i} ", new Action<string>[] { WriteFile, Console.Write });
                    }
                    Thread.Sleep(200);
                }
            }
        }

        private static void WriteFile(string text)
        {
            var fileMode = FileMode.OpenOrCreate;
            if (File.Exists("nums.txt"))
                fileMode = FileMode.Append;

            using (var fs = new FileStream("nums.txt", fileMode, FileAccess.Write, FileShare.Write))
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(text);
            }
        }

        private static void PrintCurrentTime(object state)
        {
            var date = DateTime.Now;
            Console.WriteLine($"Сегодня {date:dd.MM.yyyy HH:mm:ss} - {date.DayOfWeek}");
        }

        private static void DeleteData()
        {
            if (File.Exists("nums.txt"))
                File.Delete("nums.txt");

            if (File.Exists("prime.txt"))
                File.Delete("prime.txt");
        }
    }
}
