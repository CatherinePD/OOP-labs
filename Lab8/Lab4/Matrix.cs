using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab8;

namespace Matrica
{
    [Serializable]
    public class MyMatrix<T> : IGen<T> where T : IComparable
    {
        private T[,] _matrix;
        private int _rows; //строки
        private int _cols; //столбцы
        public T[,] Matrix
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
            _matrix = new T[x, y];
            _rows = x;
            _cols = y;
        }
        public static MyMatrix<T> operator +(MyMatrix<T> obj1, MyMatrix<T> obj2) //cложение матриц
        {
            MyMatrix<T> obj3 = new MyMatrix<T>(obj1._rows, obj1._cols);
            for (int x = 0; x < obj1._rows; x++)
            {
                for (int y = 0; y < obj1._cols; y++)
                {
                    obj3._matrix[x, y] = Sum(obj1._matrix[x, y], obj2._matrix[x, y]);
                }
            }
            return obj3;
        }
        private static T Sum(T left, T right)
        {
            return (dynamic)left + (dynamic)right;
        }
        public static MyMatrix<T> operator --(MyMatrix<T> obj1) //обнуление матрицы
        {
            for (int x = 0; x < obj1._rows; x++)
            {
                for (int y = 0; y < obj1._cols; y++)
                {
                    obj1._matrix[x, y] = default(T);
                }
            }
            return obj1;
        }
        public static bool operator ==(MyMatrix<T> obj1, MyMatrix<T> obj2) //cравнение матриц по нулевому столбцу
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(MyMatrix<T> obj1, MyMatrix<T> obj2) //cравнение матриц по нулевому столбцу
        {
            return !obj1.Equals(obj2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            MyMatrix<T> result = obj as MyMatrix<T>;
            for (int x = 0; x < result._rows; x++)
            {
                if(result._matrix[x,0].CompareTo(_matrix[x, 0]) != 0)
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

        public void Add(T item, int x, int y)
        {
            this._matrix[x, y] = item;

        }

        public void Delete(T item)
        {
            for (int x = 0; x < this.Rows; x++)
            {
                for (int y = 0; y < this.Cols; y++)
                {
                    if (this._matrix[x, y].CompareTo(item) == 0)
                        this._matrix[x, y] = default(T);
                }
            }
        }
        public void Delete(int x, int y)
        {
            this._matrix[x, y] = default(T);
        }

        public T Look(int x, int y)
        {
            return this._matrix[x, y];
        }
    }
}
