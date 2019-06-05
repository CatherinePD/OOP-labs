using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Lab5;

namespace Lab10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //====================================//  1
            var student = new Student {Age = 19, Group = 4, Name = "Катя"};
            ArrayList arrayList = new ArrayList {6, 2, 11, 1, 9};
            arrayList.Add("строка");
            arrayList.Add(student);
            arrayList.PrintCollection();
            arrayList.RemoveAt(0);
            arrayList.PrintCollection();
            int founded = arrayList.Find(student);
            Console.WriteLine($"Индекс искомого элемента: {founded}\n");

            //====================================//  2
            SortedDictionary<string, char> symbolDict = new SortedDictionary<string, char>();   //клавиатура
            symbolDict.Add("key_S_eng", 'S');
            symbolDict.Add("key_S_rus", 'Ы');
            symbolDict.Add("key_D_eng", 'D');
            symbolDict.Add("key_D_rus", 'В');
            symbolDict.Add("key_F_eng", 'F');
            symbolDict.Add("key_F_rus", 'А');
            symbolDict.Add("key_G_eng", 'G');
            symbolDict.Add("key_G_rus", 'П');

            symbolDict.PrintDictionary();
            symbolDict.DeleteElementSequence(2, 1);
            symbolDict.PrintDictionary();

            symbolDict.Add("key_W_eng", 'W');
            symbolDict.Add("key_W_rus", 'Ц');
            symbolDict.Add("key_Q_eng", 'Q');
            symbolDict.Add("key_Q_rus", 'Й');

            symbolDict.PrintDictionary();

            LinkedList<char> symbList = new LinkedList<char>();
            symbList.AddFirst(symbolDict.First().Value);

            foreach (var s in symbolDict.Skip(1))
            {
                symbList.AddAfter(symbList.Last, s.Value);
            }

            symbList.PrintCollection();

            LinkedListNode<char> foundedNode = symbList.Find('W');
            if (foundedNode!= null)
                Console.WriteLine($"Искомый элемент: {foundedNode.Value}, Next :{foundedNode.Next?.Value}, Prev: {foundedNode.Previous?.Value}\n");

            //====================================//  3
            SortedDictionary<string, Stone> stoneDict = new SortedDictionary<string, Stone>();
            stoneDict.Add("rare_Ruby", new Ruby {Price = 130, Weight = 16});
            stoneDict.Add("common_Ruby", new Ruby { Price = 79, Weight = 11 });
            stoneDict.Add("rare_Diamond", new Diamond { Price = 240, Weight = 15 });
            stoneDict.Add("common_Diamond", new Diamond { Price = 360, Weight = 19 });
            stoneDict.Add("rare_Nefrit", new Nefrit { Price = 122, Weight = 27 });
            stoneDict.Add("common_Nefrit", new Nefrit { Price = 100, Weight = 20 });

            stoneDict.PrintDictionary();
            stoneDict.DeleteElementSequence(2, 1);
            stoneDict.PrintDictionary();

            stoneDict.Add("exclusive_Diamond", new Diamond {Price = 450, Weight = 25}); 
            stoneDict.Add("exclusive_Ruby", new Diamond { Price = 190, Weight = 22 });

            stoneDict.PrintDictionary();

            LinkedList<Stone> stoneList = new LinkedList<Stone>();
            stoneList.AddFirst(stoneDict.First().Value);

            foreach (var s in stoneDict.Skip(1))
                stoneList.AddAfter(stoneList.Last, s.Value);

            stoneList.PrintCollection();

            LinkedListNode<Stone> foundedStoneNode = stoneList.Find(new Diamond { Price = 450, Weight = 25 });
            if (foundedStoneNode != null)
                Console.WriteLine($"Искомый элемент: {foundedStoneNode.Value}, \n\tNext :{foundedStoneNode.Next?.Value}, \n\tPrev: {foundedStoneNode.Previous?.Value}\n");

            //====================================//  4
            ObservableCollection<Stone> observableStones = new ObservableCollection<Stone>(stoneList);
            observableStones.CollectionChanged += OnCollectionChanged; 

            observableStones.Add(new Nefrit {Price = 90, Weight = 34});
            observableStones.RemoveAt(0);

            observableStones.PrintCollection();
        }
        private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyArgs)
        {
            if (notifyArgs.Action == NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine($"Был добавлен элемент: {notifyArgs.NewItems[0]}");
            }
            else if (notifyArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine($"Был удален элемент: {notifyArgs.OldItems[0]}");
            }
        }
    }
}
