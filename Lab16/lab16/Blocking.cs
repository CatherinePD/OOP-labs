using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab16
{
    public class Product
    {
        public string Name { get; set; }
    }

    public class Producer
    {
        private readonly int _sleep;
        public string Name { get; set; }

        public Product Product { get; set; }
        public Producer(int sleep, string name, Product product)
        {
            _sleep = sleep;
            Name = name;
            Product = product;
        }

        public void Produce(BlockingCollection<Product> stock)
        {
            Thread.Sleep(_sleep);
            stock.Add(Product);
            Console.WriteLine($"Поставщик {Name} доставил товар {Product.Name}");
        }
    }

    public class Consumer
    {
        public string Name { get; set; }

        public Consumer(string name)
        {
            Name = name;
        }
        public Product Consume(BlockingCollection<Product> stock)
        {
            Product p = null;
            while (!stock.IsAddingCompleted)
            {
                if (stock.TryTake(out p))
                {
                    Console.WriteLine($"Потребитель {Name} купил товар {p.Name}");
                    return p;
                }
            }
            return p;
        }
    }



}
