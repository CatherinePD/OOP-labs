using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Lab7.Exceptions;

namespace Lab7
{
    public class NecklaceController
    {
        public Necklace Necklace { get; set; }

        public NecklaceController(Necklace necklace) // check for null
        {
            if (necklace == null)
                throw new ArgumentNullException(nameof(necklace), "объект типа necklace не может быть null");

            Necklace = necklace;
        }

        public int GetTotalWeight()
        {
            return Necklace.Stones.Sum(s => s.Weight);
        }

        public int GetTotalPrice()
        {
            return Necklace.Stones.Sum(s => s.Price);
        }

        public void Sort()
        {
            Necklace.Stones.Sort();  // Сортировка внутри себя вызывает метод CompareTo интерфейса IComparable у типа Stone (как элемента списка)
        }

        public IEnumerable<Stone> GetStonesByOpacity(int start, int end)  // check for range
        {
            if (start < 0)
                throw new NegativeArgumentException(nameof(start));
            if (end < 0)
                throw new NegativeArgumentException(nameof(end));
            if (end < start)
                throw new ArgumentRangeException(start, end);
            
            return Necklace.Stones.Where(s => s.OpticProperty.Opacity >= start && s.OpticProperty.Opacity <= end);
        }

    }
}
