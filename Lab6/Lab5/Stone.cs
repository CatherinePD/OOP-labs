using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public partial class Stone : Product, IComparable<Stone>
    {
        public int Weight { get; set; }
        public string Color { get; set; }

        public OpticProperty OpticProperty { get; set; }

        public Stone()
        {
            ProductType = "Неопределенный камень";
        }

        public virtual void DamageTest(int power) // виртуальный метод - тест на прочность
        {
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

        public int CompareTo(Stone other)
        {
            return this.Price.CompareTo(other.Price);
        }
    }

    public struct OpticProperty
    {
        public int Opacity { get; set; }  //отражение
        public int Refraction { get; set; }  //преломление

        public OpticProperty(int opacity, int refraction)
        {
            Opacity = opacity;
            Refraction = refraction;
        }
    }
}
