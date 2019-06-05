using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab10
{
    public static class CollectionExtensions
    {
        public static void PrintCollection(this ICollection source)
        {
            Console.WriteLine($"Элементов в коллекции: {source.Count}");
            foreach (var item in source)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }

        public static int Find(this IList source, object value)
        {
            foreach (var item in source)
            {
                if (item.Equals(value))
                {
                    return source.IndexOf(item);
                }
            }
            return -1;
        }

        public static void PrintDictionary<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            Console.WriteLine($"Элементов в словаре: {source.Count}");
            foreach (var item in source)
            {
                Console.WriteLine($"Ключ: [{item.Key}], Значение: [{item.Value}]");
            }
            Console.WriteLine();
        }

        public static void DeleteElementSequence<TKey, TValue>(this IDictionary<TKey, TValue> source, int numberOfElements, int startPosition = 0)
        {
            var itemsToRemove = source.Skip(startPosition).Take(numberOfElements).ToList();

            foreach (var item in itemsToRemove)
            {
                source.Remove(item.Key);
            }
        }
    }
}