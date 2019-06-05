using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Exceptions
{
    public class NegativeArgumentException : ArgumentException
    {
        public NegativeArgumentException()
            :base("Аргумент не может быть меньше нуля")
        {
        }

        public NegativeArgumentException(string argumentName)
            :base($"Аргумент {argumentName} не может быть меньше нуля")
        {
        }
    }
}
