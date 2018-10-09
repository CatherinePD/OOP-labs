using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDesign
{
    public partial class Abiturient
    {
        static Abiturient()
        {
            Console.WriteLine($"Прием абитуриентов ведется в {UniversityName}");
        }

        private Abiturient()
        {
            _num++;
            Console.WriteLine("Новый абитуриент номер " + _num);
        }

        public Abiturient(string name, string surname, string middleName, string address, string phone, int[] marks)
            :this()
        {
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            Address = address;
            Phone = phone;
            Marks = marks;

            _id = GetHashCode();
        }

        public Abiturient(string name, string surname, string middleName, string address = "Sverdlova", string phone = "7788")
            : this(name, surname, middleName, address, phone, new[] {10, 10, 3, 7, 10})
        {
        }

        public static string GetMetaInfo()
        {
            return $"\nAbiturient.cs info: \n\n" +
                   $"Университет: {UniversityName} \n" +
                   $"Количество абитуриентов: {_num} \n" +
                   $"\n2018 (c) All Rights Reserved\n";
        }
        public int GetAverageMark()
        {
            return _marks.Sum() / _marks.Length;
        }

        public int GetMaxMark()
        {
            return _marks.Max();
        }

        public int GetMinMark()
        {
            return _marks.Min();
        }

        public void PrintFullname(out string fullname)
        {
            fullname = $"{_surname} {_name} {_middleName}";

            Console.WriteLine($"Полное имя абитуриента: {fullname}");
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            
            Abiturient result = obj as Abiturient;

            if (_name == result._name &&
                _surname == result._surname &&
                //_id == result._id &&
                _middleName == result._middleName &&
                _phone == result._phone &&
                _address == result._address
                )
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = string.IsNullOrEmpty(_name) ? 0 : _name.GetHashCode();
            hash = (hash * 47) + _num.GetHashCode();
            return hash;
        }

        private bool CheckForNullOrEmpty(string value, string fieldName)
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine($"Ошибка: {fieldName} не может быть пустым");
                return false;
            }
            return true;
        }

        private bool ValidateMarks(int[] value, string fieldName)
        {
            foreach (var mark in value)
            {
                if (mark > 10 || mark < 0)
                {
                    Console.WriteLine($"Ошибка: {fieldName} не может содержать значения меньше 0 или больше 10");
                    return false;
                }
            }
            return true;
        }
    }

}
