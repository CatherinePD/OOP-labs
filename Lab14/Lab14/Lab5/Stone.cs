using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab5
{
    [Serializable]

    //[XmlInclude(typeof(Diamond))]
    //[XmlInclude(typeof(Ruby))]
    [XmlInclude(typeof(PreciousStone))]

    //[KnownType(typeof(Diamond))]
    //[KnownType(typeof(Ruby))]
    [KnownType(typeof(PreciousStone))]

    //[SoapInclude(typeof(Diamond))]
    //[SoapInclude(typeof(Ruby))]
    [SoapInclude(typeof(PreciousStone))]
    public partial class Stone : Product, IComparable<Stone>
    {
        [XmlAttribute]
        public int Weight { get; set; }// check for negative
        public string Color { get; set; }

        public OpticProperty OpticProperty { get; set; }

        public Stone()
        {
            ProductType = "Неопределенный камень";
        }

        public virtual void DamageTest(int power) // виртуальный метод - тест на прочность // check for negative
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

        public int CompareTo(Stone other) // check for null
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "объект типа stone не может быть null");

            return this.Price.CompareTo(other.Price);
        }
    }

    [Serializable]
    public struct OpticProperty
    {
        public int Opacity { get; set; }
        public int Refraction { get; set; }
    }
}
