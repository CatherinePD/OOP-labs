using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
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
            return $"Информация о нефрите: Вес - {Weight} грамм; Цвет - {Color}; Тип камня - {ProductType}; Цена - {Price}; Статус - {StatusDescription};  Прозрачность - {OpticProperty.Opacity}; Преломление - {OpticProperty.Refraction}.";
        }
    }
}
