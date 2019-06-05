using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class Necklace
    {
        public List<Stone> Stones { get; set; }

        public Necklace(IEnumerable<Stone> stones)  // передаём любую коллекцию камней
        {
            if (stones == null)
                throw new ArgumentNullException(nameof(stones), "коллекция типа stone не может быть null");

            Stones = stones.ToList();  // преобразовываем коллекцию в список
        }

        public Stone this[int i]  // indexOutOfRange
        {
            get
            {
                if (i < 0 || i > Stones.Count)
                    throw new IndexOutOfRangeException("индекс вышел за пределы допустимого диапозона");

                return Stones[i];
            }
            set
            {
                if (i < 0 || i > Stones.Count)
                    throw new IndexOutOfRangeException("индекс вышел за пределы допустимого диапозона");

                Stones[i] = value;
            }
        }

        public void Add(Stone stone)  // stone - check for null
        {
            if (stone == null)
                throw new ArgumentNullException(nameof(stone), "объект типа stone не может быть null");

            Stones.Add(stone);
        }

        public void AddRange(IEnumerable<Stone> stones) // stones - check for null
        {
            if (stones == null)
                throw new ArgumentNullException(nameof(stones), "коллекция типа stone не может быть null");

            Debug.Assert(stones.Any(), "Вы пытайтесь добавить пустой массив");//Any - проверка, есть ли элементы в массиве
            //Assert - если первый параметр true, то ничего не происходит, если false, то выводится окно с сообщение(второй параметр). 
            //В окне можно прервать выполнение программы или продолжить его

            Stones.AddRange(stones);
        }

        public void Remove(Stone stone)  // stones - check for null
        {
            if (stone == null)
                throw new ArgumentNullException(nameof(stone), "объект типа stone не может быть null");

            Stones.Remove(stone);
        }

        public void RemoveAt(int index)  // indexOutOfRange
        {
            if (index < 0 || index > Stones.Count)
                throw new IndexOutOfRangeException("индекс вышел за пределы допустимого диапозона");

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
