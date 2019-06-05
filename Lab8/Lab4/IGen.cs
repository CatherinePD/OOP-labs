using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    interface IGen <T>
    {
        void Add(T item, int x, int y);
        void Delete(T item);
        void Delete(int x, int y);
        T Look(int x, int y);
    }
}
