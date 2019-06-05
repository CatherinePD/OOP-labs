using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.Exceptions;

namespace Lab7
{
    public abstract class Product //нельзя создать объект абстрактного класса
    {
        private int _price;

        public int Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new NegativeArgumentException("цена");
                _price = value;
            }
        }  // check for negative

        public string ProductType { get; protected set; }

        public ProductStatus Status { get; set; }

        protected Product()
        {
            ProductType = "Продукт";
            Status = ProductStatus.None;
        }

        public string StatusDescription
        {
            get
            {
                switch (Status)
                {
                    case ProductStatus.None:
                        return "Нет на складе";
                    case ProductStatus.Ordered:
                        return "Заказан";
                    case ProductStatus.Ready:
                        return "На складе";
                    case ProductStatus.Damaged:
                        return "Поврежден";
                    default:
                        return "Нет на складе";
                }
            }
        }

        public void Order()
        {
            if (Status == ProductStatus.Ready)
                Status = ProductStatus.Ordered;

            else if(Status == ProductStatus.None)
                Status = ProductStatus.Ready;
        }

        // абстрактный метод
        public abstract string DefineMarket(); // определить рынки сбыта

        public override string ToString()
        {
            return $"Информация о продукте: Тип продукта - {ProductType}; Цена - {Price}; Статус - {StatusDescription}.";
        }
    }

    public enum ProductStatus
    {
        None, // Нет на складе
        Ordered, // Заказан
        Ready, // На складе
        Damaged // Поврежден
    }

}
