using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            //task 1.a
            bool a = true;
            byte b = 240;
            sbyte c = -127;
            short d = -300;
            ushort e = 648;
            int f = -4003;
            uint g = 810273;
            long h = -388483929;
            ulong i = 938729283883;
            float j = 58.29f;
            double k = 1378.1738d;
            decimal l = 6273;
            char m = 'k';
            string n = "Catherine";
            object o = "hello"; //значение любого типа

            //1.b,c
            //неявное
            k = j; //float к double
            h = f; //int к long
            o = c; //sbyte к object (boxing)
            l = d; //short к decimal
            i = b; //byte к ulong
            //явное
            f = (int)e;
            l = (decimal)(sbyte)o; //(unboxing)
            g = Convert.ToUInt32(b);
            h = (long)g;
            a = Convert.ToBoolean(l);
            //1.d
            var vStr = "I am string";
            var vI = 328;
            var vCh = 'y';
            vI += 1220;
            Console.WriteLine(vStr + ", " + vCh);
            Console.WriteLine(Convert.ToString(vI) + " = " + vI.GetType());
            //1.e
            Nullable<int> p = null; //полная форма записи
            int? q = null; //упрощенная форма записи
            bool ifEqual = p == q;
            Console.WriteLine("Имеет ли значение р? " + p.HasValue);
            Console.WriteLine("Равны ли переменные p и q? " + ifEqual);

            //2.a
            string str1 = "I am learning C#";
            string str2 = "Something interesting";
            int result = String.Compare(str1, str2); //сравнение в алфавитном порядке
            if (result < 0)
                Console.WriteLine("Строка str1 перед строкой str2");
            else if (result > 0)
                Console.WriteLine("Строка str1 стоит после строки str2");
            else
                Console.WriteLine("Строки str1 и str2 идентичны");

            //2.b
            string str3 = "Hey guys";
            //сцепление
            Console.WriteLine(str1 + str2 + str3);
            Console.WriteLine(String.Concat(str1, str2, str3));
            string[] values = { str1, str2, str3 };
            Console.WriteLine(String.Join(" ", values)); //объединили и добавили между строками пробел
            //копирование
            string strCopy = String.Copy(str3);
            Console.WriteLine(strCopy);
            //выделение подстроки
            Console.WriteLine(str2.Substring(10));
            Console.WriteLine(str1.Substring(5, 8)); //8 символов начиная с 5
            //разделение строки на слова
            string[] words = str1.Split(new char[] { ' ' });
            foreach (string s in words)
            {
                Console.WriteLine(s);
            }
            //вставка подстроки в заданную позицию
            Console.WriteLine(str2.Insert(str2.Length, str1.Substring(13)));
            //удаление заданной подстроки
            Console.WriteLine(str3.Remove(0, 4));//4 символа начиная с 0

            //2.c
            string sEmpty = "";
            string sNull = null;
            Console.WriteLine(sEmpty.Length);
            Console.WriteLine(sEmpty == sNull);
            //2.d
            StringBuilder sb = new StringBuilder("New String", 50); //строка и выделяемая ей память
            sb.Remove(7, 3);
            sb.Append("oke");
            sb.Insert(0, "Hi ");
            sb.Remove(3, 4);
            Console.WriteLine(sb);

            //3.a
            int[,] arr = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                    Console.Write(arr[x, y] + " ");
                Console.WriteLine();
            }
            //3.b
            string[] ns = { "C++", "C#", "Python", "Java", "Swift", "JavaScript" };
            foreach (string ind in ns)
            {
                if (ind == ns[ns.Length - 1])
                {
                    Console.WriteLine(ind + ".");
                    break;
                }
                Console.Write(ind + ", ");
            }
            Console.WriteLine($"Длина массива: {ns.Length}");
            Console.WriteLine("Введите позицию нового элемента массива, а затем его значение");
            ns[Convert.ToInt32(Console.ReadLine()) - 1] = Console.ReadLine();
            foreach (string ind in ns)
            {
                if (ind == ns[ns.Length - 1])
                {
                    Console.WriteLine(ind + ".");
                    break;
                }
                Console.Write(ind + ", ");
            }
            //3.c
            int[][] jaggedArr = new int[][]
            {
                new int[2],
                new int[3],
                new int[4]
            };
            Console.WriteLine("Введите элементы ступенчатого массива");
            for (int x = 0; x < jaggedArr.Length; x++)
                for (int y = 0; y < jaggedArr[x].Length; y++)
                    jaggedArr[x][y] = Convert.ToInt32(Console.ReadLine());
            for (int x = 0; x < jaggedArr.Length; x++)
            {
                for (int y = 0; y < jaggedArr[x].Length; y++)
                {
                    Console.Write("{0} ", jaggedArr[x][y]);
                }
                Console.WriteLine();
            }
            //3.d
            var Array = new[] { "Kate", "Alex", "Nastya", "Darya", "Misha", "Sam" };
            Console.WriteLine(Array.GetType());
            //or
            var List = new List<string>();
            List.Add("Kate");
            List.Add("Nastya");
            List.Insert(1, "Alex");
            foreach(var x in List)
                Console.Write($"{x} ");
            Console.WriteLine();
            Console.WriteLine(List.GetType());

            //4
            (int first, string second, char third, string fourth, ulong fifth) VarTuple = (28, "Cat", 'A', "Dog", 1827293);
            Console.WriteLine("Кортеж целиком: " + VarTuple);
            Console.WriteLine($"1, 3 и 4 элементы кортежа: {VarTuple.first}, {VarTuple.third}, {VarTuple.fourth}");
            //распаковка в переменные
            int firstT = VarTuple.first;
            string secondT = VarTuple.second;
            char thirdT = VarTuple.third;
            string fourthT = VarTuple.fourth;
            ulong fifthT = VarTuple.fifth;
            //сравнение
            var NewTuple = (28, "Cat", 'A', "Dog", (ulong)1827293);
            if (VarTuple.CompareTo(NewTuple) == 0)
                Console.WriteLine("Кортежи равны");
            else
                Console.WriteLine("Кортежи не равны");

            //5
            (int, int, int, char) TFunction(int[] mas, string str)
            {
                int max = mas.Max();
                int min = mas.Min();
                int sum = mas.Sum();
                char FL = str[0];
                return (max, min, sum, FL);

            }
            var Array1 = new[] { 1, 2, 3, 4, 12, 20, 82, 18, 100, 18, 9, 122, 14, 5, 13 };
            string Str1 = "Wonderful day";
            Console.WriteLine(TFunction(Array1, Str1));
        }
    }
}
