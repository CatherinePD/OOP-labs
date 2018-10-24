using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            // инициализация объектов
            var stone1 = new Stone {Color = "Белый", Price = 20, Status = ProductStatus.Ready, Weight = 100};
            var pStone1 = new PreciousStone { Color = "Желтый", Price = 40, Status = ProductStatus.Ready, Weight = 40};
            var ruby1 = new Ruby {Price = 57, Status = ProductStatus.None, Weight = 30};
            var diamond1 = new Diamond { Price = 89, Status = ProductStatus.Ready, Weight = 28 };
            var spStone1 = new SemiPreciousStone { Color = "Зелёный", Price = 30, Status = ProductStatus.None, Weight = 41 };
            var nefrit1 = new Nefrit { Price = 50, Status = ProductStatus.Ready, Weight = 44 };



            List<Stone> stones = new List<Stone> { stone1, spStone1, pStone1, nefrit1, ruby1, diamond1};
            
            var necklace = new Necklace(stones);

            necklace.Print();

            necklace.RemoveAt(0);

            necklace.Remove(necklace[0]);

            Console.WriteLine();

            necklace.Print();

            necklace.Add(new Ruby { Weight = 33, Price = 50, OpticProperty = new OpticProperty(5, 10)});
            necklace.AddRange(new List<Stone> {new Nefrit(), new Diamond() });

            Console.WriteLine();

            var controller = new NecklaceController(necklace);

            controller.Sort();

            necklace.Print();

            var stonesO = controller.GetStonesByOpacity(2, 11);

            Console.WriteLine();
            new Necklace(stonesO).Print();

            Console.WriteLine();
            Console.WriteLine($"Общая ценность ожерелья: {controller.GetTotalPrice()}");
            Console.WriteLine($"Общий вес ожерелья: {controller.GetTotalWeight()}");

        }
    }
}
