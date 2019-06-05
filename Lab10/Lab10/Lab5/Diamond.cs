﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
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
            return $"Алмаз: Вес - {Weight} грамм; Цена - ${Price}";
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
