using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.Exceptions;

namespace Lab7
{
    public partial class Stone : Product, IComparable<Stone>
    {
        private int _weight;
        private string _color;

        public int Weight
        {
            get { return _weight; }
            set
            {
                if (value < 0)
                    throw new NegativeArgumentException("вес");
                _weight = value;
            }
        } // check for negative
        public string Color
        {
            get { return _color;}
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new StringNullOrEmptyException("цвет");
                _color = value;
            }
        } 

        public OpticProperty OpticProperty { get; set; }

        public Stone()
        {
            ProductType = "Неопределенный камень";
        }

        public virtual void DamageTest(int power) // виртуальный метод - тест на прочность // check for negative
        {
            if (power < 0)
                throw new NegativeArgumentException("давление");

            if (power > 1000)
            {
                Console.WriteLine("Камень не прошел тест на прочность");
                Status = ProductStatus.Damaged;
            }
            else Console.WriteLine("Камень прошел тест на прочность");
        }

        // реализация абстрактоного метода класса Product
        public override string DefineMarket() // определить рынки сбыта
        {
            return "Рынки сбыта - камни и минералы";
        }

        public int CompareTo(Stone other) // check for null
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "объект типа stone не может быть null");

            return this.Price.CompareTo(other.Price);
        }
    }

    public struct OpticProperty
    {
        private int _opacity;
        private int _refraction;

        public int Opacity
        {
            get { return _opacity; }
            set
            {
                if (value < 0)
                    throw new NegativeArgumentException("прозрачность");

                _opacity = value;
            }
        }  //отражение  // check for negative
        public int Refraction
        {
            get { return _refraction; }
            set
            {
                if (value < 0)
                    throw new NegativeArgumentException("отражение");

                _refraction = value;
            }
        }  //преломление // check for negative

        public OpticProperty(int opacity, int refraction) 
        {
            if (opacity < 0)
                throw new NegativeArgumentException("прозрачность");
            if (refraction < 0)
                throw new NegativeArgumentException("отражение");

            _opacity = opacity;
            _refraction = refraction;
        }
    }
}
