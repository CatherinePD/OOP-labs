using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public partial class Stone
    {
        public override string ToString()
        {
            return $"Информация о камне: Вес - {Weight} грамм; Цвет - {Color}; Тип камня - {ProductType}; Цена - {Price}; Статус - {StatusDescription}; Прозрачность - {OpticProperty.Opacity}; Преломление - {OpticProperty.Refraction}.";
        }

        public override bool Equals(object obj)
        {
            var stone = obj as Stone;

            if (stone == null)
                return false;

            return this.Weight == stone.Weight &&
                   this.Color == stone.Color &&
                   this.Status == stone.Status &&
                   this.ProductType == stone.ProductType &&
                   this.Price == stone.Price;
        }

        public override int GetHashCode()
        {
            return ProductType.GetHashCode() * 17 + Color.GetHashCode() * 19 + Price.GetHashCode() * 17;
        }
    }
}
