using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Exceptions
{
    public class ArgumentRangeException : ArgumentException
    {
        public ArgumentRangeException(int lower, int greater)
            : base($"Аргумент {greater} не может быть меньше чем {lower}")
        {
        }
    }
}
