using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public sealed class Nefrit: SemiPreciousStone
    {
        public Nefrit()
        {
            ProductType = "Нефрит";
            Color = "Зеленый";
        }

        public override void MakeBracelet()
        {
            Console.WriteLine($"Создан браслет из нефрита");
        }

        public override string ToString()
        {
            return $"Нефрит: Вес - {Weight} грамм; Цена - ${Price}";
        }
    }
}
