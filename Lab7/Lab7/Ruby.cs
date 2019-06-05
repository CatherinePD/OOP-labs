﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.Exceptions;

namespace Lab7
{
    public sealed class Ruby: PreciousStone
    {
        public Ruby()
        {
            Color = "Красный";
            ProductType = "Рубин";
        }

        public override string ToString()
        {
            return $"Информация о рубине: Вес - {Weight} грамм; Цвет - {Color}; Тип камня - {ProductType}; Цена - {Price}; Статус - {StatusDescription}; Прозрачность - {OpticProperty.Opacity}; Преломление - {OpticProperty.Refraction}.";
        }

        public override void DamageTest(int power)  //check for negative
        {
            if (power < 0)
                throw new NegativeArgumentException("давление");

            if (power > 9999)
            {
                Console.WriteLine("Рубин не прошел тест на прочность");
                Status = ProductStatus.Damaged;
            }
            else Console.WriteLine("Рубин прошел тест на прочность");
        }
    }
}
