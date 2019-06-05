using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Matrica;
using Microsoft.CSharp.RuntimeBinder;
using System.Xml.Serialization;


namespace Lab8
{
    static class MathOperation //статический класс с 3 методами + методы расширения
    {
        public static void Zeroing<T>(this MyMatrix<T> obj) where T: IComparable
        {
            for (int x = 0; x < obj.Rows; x++)
            {
                for (int y = 0; y < obj.Cols; y++)
                {
                    if ((dynamic)obj.Matrix[x, y] < 0) //Во время компиляции предполагается что элементы с типом dynamic поддерживают любые операции
                        obj.Matrix[x, y] = default(T);
                }
            }
        }
        public static T FirstNumber<T>(this MyMatrix<T> obj, int k) where T : IComparable
        {
            return obj.Matrix[k, 0];
        }
        public static void MinElement<T>(this MyMatrix<T> obj) where T : IComparable
        {
            dynamic k = 99999999999999999;
            for (int x = 0; x < obj.Rows; x++)
            {
                for (int y = 0; y < obj.Cols; y++)
                {
                    if ((dynamic)obj.Matrix[x, y] < k)
                       k = obj.Matrix[x, y];
                }
            }
            Console.WriteLine("Минимальный элемент матрицы " + k);
        }
        public static void MaxElement<T>(this MyMatrix<T> obj) where T : IComparable
        {
            dynamic k = -99999999999999999;
            for (int x = 0; x < obj.Rows; x++)
            {
                for (int y = 0; y < obj.Cols; y++)
                {
                    if ((dynamic)obj.Matrix[x, y] > k)
                        k = obj.Matrix[x, y];
                }
            }
            Console.WriteLine("Максимальный элемент матрицы " + k);
        }
        public static void SumElements<T>(this MyMatrix<T> obj) where T : IComparable
        {
            dynamic k = 0;
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

    public static class FileOperations
    {
        public static void WriteToFile<T>(this MyMatrix<T> obj, string fileName) where T : IComparable
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fs, obj);
            }
            
        }

        public static MyMatrix<T> ReadFromFile<T>(string fileName) where T : IComparable
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new BinaryFormatter();
                return (MyMatrix<T>)binaryFormatter.Deserialize(fs);
            }
        }


    }
    
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MyMatrix<int> m1 = new MyMatrix<int>(3, 4);
                MyMatrix<int> m2 = new MyMatrix<int>(3, 4);

                m1.Matrix = new[,] {{3, 7, -3, 0}, {5, 8, -8, -1}, {-6, -2, 9, 3}};
                m2.Matrix = new[,] {{3, 7, -3, 0}, {5, 8, -8, -1}, {-6, -2, 9, 3}};

                Console.WriteLine("Первое число в третьей строке матрицы m2: " + m2.FirstNumber(2));

                Console.WriteLine("Равны ли матрицы m1 и m2 по нулевому столбцу? " + (m1 == m2));

                MyMatrix<int> m3 = m1 + m2;

                m3.Print();
                m3.Zeroing();
                m3.Print();

                m1--;

                m1.Print();

                Console.WriteLine("А теперь равны ли матрицы m1 и m2 по нулевому столбцу? " + (m1 == m2));

                m1.Matrix = new[,] {{78, -17, -49, 2}, {4, 19, -55, -173}, {182, 31, -99, 114}};

                m1.MaxElement();
                m1.MinElement();
                m1.SumElements();

                //Матрицы различных типов

                MyMatrix<string> ms = new MyMatrix<string>(2, 2);
                ms.Matrix = new[,] {{"kate", "dasha"}, {"lexa", "roma"}};
                ms.Print();
                //исключение
                //ms.MaxElement(); 
                //ms.Zeroing();

                var mb = new MyMatrix<bool>(3, 2);
                mb.Matrix = new[,] {{false, true}, {true, true}, {true, false}};
                mb.Print();

                var md = new MyMatrix<double>(2, 3);
                md.Matrix = new[,] {{4.765, -12.61222222, 0.69}, {666.999, 82.28, 04.091999}};
                md.MinElement();
                md.Zeroing();
                md.Print();
                md.SumElements();

                var stone1 = new Stone { Color = "Белый", Price = 20, Weight = 100 };
                var stone2 = new Stone { Color = "Желтый", Price = 15, Weight = 98 };
                var stone3 = new Stone { Color = "Зеленый", Price = 24, Weight = 109 };
                var stone4 = new Stone { Color = "Черный", Price = 31, Weight = 124 };

                MyMatrix<Stone> mStones = new MyMatrix<Stone>(2,2);
                mStones.Matrix = new[,]{{stone1, stone2},{stone3, stone4}};
                mStones.Print();
                //исключение
                //MyMatrix<Stone> mStones1 = new MyMatrix<Stone>(2, 2);
                //mStones1.Matrix = new[,] { { stone2, stone1 }, { stone4, stone3 } };
                // var mNew = mStones1 + mStones; 

                mStones.WriteToFile("storage.txt");

                MyMatrix<Stone> mF = FileOperations.ReadFromFile<Stone>("storage.txt");
                mF.Print();
            }
            catch (RuntimeBinderException e)
            {
                Console.WriteLine($"Этот тип матрицы не поддерживает некоторые операции:{e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Непредвиденное исключение:{e.Message}");
            }
            finally
            {
                Console.WriteLine("Завершение работы");
            }
        }
    }
}
