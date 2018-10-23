using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Printer
    {
        public void IamPrinting(Product product)
        {
            Console.WriteLine($"Тип объекта: {product.GetType()}. Info: {product.ToString()}");
        }
    }
}
