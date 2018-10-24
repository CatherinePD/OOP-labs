using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Necklace
    {
        public List<Stone> Stones { get; set; }

        public Necklace(IEnumerable<Stone> stones)  // передаём любую коллекцию камней
        {
            Stones = stones.ToList();  // преобразовываем коллекцию в список
        }

        public Stone this[int i]
        {
            get { return Stones[i]; }
            set { Stones[i] = value; }
        }

        public void Add(Stone stone)
        {
            Stones.Add(stone);
        }

        public void AddRange(IEnumerable<Stone> stones)
        {
            Stones.AddRange(stones);
        }

        public void Remove(Stone stone)
        {
            Stones.Remove(stone);
        }

        public void RemoveAt(int index)
        {
            Stones.RemoveAt(index);
        }

        public override string ToString()
        {
            string result = "";
            foreach (var stone in Stones)
            {
                result += $"{stone}\n";
            }

            return result;
        }

        public void Print()
        {
            Console.WriteLine(this);
        }
    }
}
