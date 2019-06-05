using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Exceptions
{
    public class StringNullOrEmptyException : ArgumentException
    {
        public StringNullOrEmptyException()
            :base("Строка не может быть пустой либо null")
        {
        }

        public StringNullOrEmptyException(string argumentName)
            :base($"Строковый аргумет {argumentName} не может быть пустой либо null")
        {
        }
    }
}
