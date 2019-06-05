using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public class Software //класс-подписчик
    {
        public string Name { get; set; }
        public double Version { get; set; }
        public bool IsRunning { get; set; }

        public Software(string name, double version)
        {
            Name = name;
            Version = version;
        }
        
        public void Subscribe(User user)  // Подписка на события User'a
        {
            // Подписка на Upgrade
            user.d1 += (sender, args) => //анонимная функция (используем лямбда-выражение для упрощенной записи)
            {
                Version += 0.1;
                Console.WriteLine($"Программа {Name} была обновлена до версии {Version}");
            };
           // user.d1(user, EventArgs.Empty);
            // Подписка на Work
            user.Work += (sender, args) => //обработчик события
            {
                StartProgram();
                var usr = (User)sender;
                var m = args.Message;
                Console.WriteLine(m);
                Console.WriteLine($"Программа {Name} была запущена пользователем {usr.Name}");
            };

        }
        
        public void StartProgram()
        {
            IsRunning = true;
        }
        public void CloseProgram()
        {
            IsRunning = false;
        }

        public override string ToString()
        {
            return $"Информация о программе: \"{Name}\" версии {Version}, запущена {IsRunning}";
        }
    }
}
