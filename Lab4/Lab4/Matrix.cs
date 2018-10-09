using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrica
{
    class MyMatrix
    {
        private int[,] _matrix;
        private int _rows; //строки
        private int _cols; //столбцы
        public int[,] Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }
        public int Rows
        {
            get { return _rows; }
            set { _rows = _matrix.GetLength(0); }

        }
        public int Cols
        {
            get { return _cols; }
            set { _rows = _matrix.GetLength(1); }

        }
        public MyMatrix(int x, int y)
        {
            _matrix = new int[x, y];
            _rows = x;
            _cols = y;
        }
        public static MyMatrix operator +(MyMatrix obj1, MyMatrix obj2) //cложение матриц
        {
            MyMatrix obj3 = new MyMatrix(obj1._rows, obj1._cols);
            for (int x = 0; x < obj1._rows; x++)
            {
                for (int y = 0; y < obj1._cols; y++)
                {
                    obj3._matrix[x, y] = obj1._matrix[x, y] + obj2._matrix[x, y];
                }
            }
            return obj3;
        }
        public static MyMatrix operator --(MyMatrix obj1) //обнуление матрицы
        {
            for (int x = 0; x < obj1._rows; x++)
            {
                for (int y = 0; y < obj1._cols; y++)
                {
                    obj1._matrix[x, y] = 0;
                }
            }
            return obj1;
        }
        public static bool operator ==(MyMatrix obj1, MyMatrix obj2) //cравнение матриц по нулевому столбцу
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(MyMatrix obj1, MyMatrix obj2) //cравнение матриц по нулевому столбцу
        {
            return !obj1.Equals(obj2);
        }
        public static explicit operator int(MyMatrix obj)//явное преобразование
        {
            int k = 0;

            for (int x = 0; x < obj._rows; x++)
            {
                for (int y = 0; y < obj._cols; y++)
                {
                    if (obj._matrix[x, y] < 0)
                        k++;
                }
            }
            return k;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            MyMatrix result = obj as MyMatrix;
            for (int x = 0; x < result._rows; x++)
            {
                if (result._matrix[x, 0] != _matrix[x, 0])
                    return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int hash = _matrix.IsFixedSize ? 0 : _matrix.GetHashCode();
            hash = (hash * 47) + _rows.GetHashCode();
            return hash;
        }
        public void Print()
        {
            for (int x = 0; x < this._rows; x++)
            {
                for (int y = 0; y < this._cols; y++)
                {
                    Console.Write(this._matrix[x, y] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public class Data//вложенный класс
        {
            public int month;
            public int day;
            public int years;
            public Data(int d, int m, int y)
            {
                month = m;
                day = d;
                years = y;
            }
            public Data ()
            {
                Console.WriteLine("TODAY IS " + DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss"));
            }
        }
    }
}
