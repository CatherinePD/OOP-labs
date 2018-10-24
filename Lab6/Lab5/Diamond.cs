using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public sealed class Diamond : PreciousStone
    {
        public Diamond()
        {
            Color = "Прозрачный";
            ProductType = "Алмаз";
        }

        public override string ToString()
        {
            return $"Информация о алмазе: Вес - {Weight} грамм; Цвет - {Color}; Тип камня - {ProductType}; Цена - {Price}; Статус - {StatusDescription};  Прозрачность - {OpticProperty.Opacity}; Преломление - {OpticProperty.Refraction}.";
        }

        public override void DamageTest(int power)
        {
            if (power > 9_000_000)
            {
                Console.WriteLine("Алмаз не прошел тест на прочность");
                Status = ProductStatus.Damaged;
            }
            else Console.WriteLine("Алмаз прошел тест на прочность");
        }
    }
}
