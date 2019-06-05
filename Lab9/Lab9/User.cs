using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public class User //класс-издатель
    {
        public string Name { get; set; }

        public delegate void Mydelegate(object sender, EventArgs e);
        public Mydelegate d1;

        public event Mydelegate Upgrade; //тип, позволяющий классу или объекту уведомлять другие классы или объекты о возникновении каких-то ситуаций
        public event EventHandler<MyEventArgs> Work;//нельзя вызывать вне класса
        
        public User(string name)
        {
            Name = name;
        }

        public void CheckForUpdates(bool upgrade) //метод, инициирующий событие
        {
            if (upgrade && Upgrade != null)
                Upgrade(this, EventArgs.Empty);
        }

        public void StartWorking()
        {
            if(Work != null)
                Work(this, new MyEventArgs("Hello"));
        }

        public class MyEventArgs : EventArgs 
        {
            public string Message { get; set; }

            public MyEventArgs(string message)
            {
                Message = message;
            }
        }
    }

}
