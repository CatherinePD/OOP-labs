using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Abiturient
    {
        private readonly int id;
        private string firstName;
        private string secondName;
        private string address;
        private int phoneNumber;
        private int[] marks = new int[5];
        static string university;
        static int num = 0;

        public Abiturient()
        {
            num++;
            Console.WriteLine("Новый абитуриент номер "+num); //добавлен абитуриент №...
        }

        public Abiturient(string firstName, string secondName, string Address, int number = 0000000)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            address = Address;
            phoneNumber = number;
            num++;
        }
        static Abiturient()
        {
            university = "БГТУ";
            Console.WriteLine($"Прием абитуриентов ведется в {university}");
        }
      //private Abiturient()
      //  {
            
      //  }
        public void AddInfo(string FirstName, string SecondName, string Address, int Number)
        {
            firstName = FirstName;
            secondName = SecondName;
            address = Address;
            phoneNumber = Number;
        }
        public void GetInfo()
        {
            Console.WriteLine($"Сведения об абитуриенте: \n ФИ: {firstName} {secondName} \n Адрес: {address} \n Телефонный номер: {phoneNumber}");
        }
        public void AddMarks(int[] Marks)
        {
            marks = Marks;
        }
        public int srBall(int[] marks)
        {
            int sr;
            int sum = 0;
            for (int i = 0; i<marks.Length; i++)
                sum += marks[i];
            sr = sum / marks.Length;
            return sr;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Abiturient first = new Abiturient();
            first.GetInfo();
            first.AddInfo("Kate", "Petrovich", "Gromova 34-126", 3239480);
            first.GetInfo();
            
            
        }
    }
}
