using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class SemiPreciousStone : Stone, IJeweleryWorkshop
    {

        public SemiPreciousStone()
        {
            ProductType = "Неопределенный полудрагоценный камень";
        }

        public virtual void MakeBracelet()
        {
            Console.WriteLine($"Создан браслет из полудгагоценного камня");
        }

        // реализация абстрактоного метода класса Product
        public override string DefineMarket() // определить рынки сбыта
        {
            return "Рынки сбыта - Полудрагоценные камни";
        }

        public override string ToString()
        {
            return $"Информация о полудрагоценном камне: Вес - {Weight} грамм; Цвет - {Color}; Тип камня - {ProductType}; Цена - {Price}; Статус - {StatusDescription}; Прозрачность - {OpticProperty.Opacity}; Преломление - {OpticProperty.Refraction}.";
        }


        //  реализация IJeweleryWorkshop
        public void MakeRing()
        {
            Console.WriteLine($"Создано полудрагоценное кольцо из: {ProductType}");
        }

        public void MakeEarrings()
        {
            Console.WriteLine($"Созданы полудрагоценные серьги из: {ProductType}");
        }

        public void ProcessStone()
        {
            Console.WriteLine($"Обработан полудрагоценный камень: {ProductType}");
        }
    }
}
