using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrica;

namespace Lab4
{
    static class MathOperation //статический класс с 3 методами + методы расширения
    {
        public static void Zeroing(this MyMatrix obj)
        {
            for (int x = 0; x < obj.Rows; x++)
            {
                for (int y = 0; y < obj.Cols; y++)
                {
                    if (obj.Matrix[x, y] < 0)
                        obj.Matrix[x, y] = 0;
                }
            }
        }
        public static int FirstNumber(this MyMatrix obj, int k)
        {
            return obj.Matrix[k, 0];
        }
        public static void MinElement(this MyMatrix obj)
        {
            int k = 999999999;
            for (int x = 0; x < obj.Rows; x++)
            {
                for (int y = 0; y < obj.Cols; y++)
                {
                    if (obj.Matrix[x, y] < k)
                       k = obj.Matrix[x, y];
                }
            }
            Console.WriteLine("Минимальный элемент матрицы " + k);
        }
        public static void MaxElement(this MyMatrix obj)
        {
            int k = -999999999;
            for (int x = 0; x < obj.Rows; x++)
            {
                for (int y = 0; y < obj.Cols; y++)
                {
                    if (obj.Matrix[x, y] > k)
                        k = obj.Matrix[x, y];
                }
            }
            Console.WriteLine("Максимальный элемент матрицы " + k);
        }
        public static void SumElements(this MyMatrix obj)
        {
            int k = 0;
            for (int x = 0; x < obj.Rows; x++)
            {
                for (int y = 0; y < obj.Cols; y++)
                {
                        k += obj.Matrix[x, y];
                }
            }
            Console.WriteLine("Cумма всех элементов матрицы " + k);
        }

    }
    class Program
    {
        public class Owner//вложенный объект
        {
            public int ID;
            public string Name;
            public string CompanyName;

            public Owner(int id, string name, string comname)
            {
                ID = id;
                Name = name;
                CompanyName = comname;
            }
        }
        static void Main(string[] args)
        {
            MyMatrix m1 = new MyMatrix(3, 4);
            MyMatrix m2 = new MyMatrix(3, 4);
            m1.Matrix = new[,] { { 3, 7, -3, 0 }, { 5, 8, -8, -1 }, { -6, -2, 9, 3 } };
            m2.Matrix = new[,] { { 3, 7, -3, 0 }, { 5, 8, -8, -1 }, { -6, -2, 9, 3 } };
            Console.WriteLine("Первое число в третьей строке матрицы m2: " +m2.FirstNumber(2));
            Console.WriteLine("Равны ли матрицы m1 и m2 по нулевому столбцу? " + (m1 == m2));
            MyMatrix m3 = m1 + m2;
            m3.Print();
            m3.Zeroing(); 
            m3.Print();
            m1--;
            m1.Print();
            Console.WriteLine("А теперь равны ли матрицы m1 и m2 по нулевому столбцу? " + (m1 == m2));
            int col = (int)m2; //кол-во отрицательных эл-тов
            Console.WriteLine($"Количество отрицательных эл-тов матрицы m2 = {col}");
            Owner one = new Owner(561, "Catherine", "BSTU");
            Console.Write("Id=" + one.ID + " " + one.CompanyName + " " + one.Name + " ");
            MyMatrix.Data two = new MyMatrix.Data(04, 09, 1999);
            Console.WriteLine(two.day + "." + two.month + "." + two.years);
            MyMatrix.Data date = new MyMatrix.Data();
            m1.Matrix = new[,] { { 78, -17, -49, 2 }, { 4, 19, -55, -173 }, { 182, 31, -99, 114 } };
            m1.MaxElement();
            m1.MinElement();
            m1.SumElements();
        }
    }
}
