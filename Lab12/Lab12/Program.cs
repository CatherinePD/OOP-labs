using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Lab5;

namespace Lab12
{
    public class Program
    {
        static void Main(string[] args)
        {
                Type productType = typeof(Product);
                Type preciousStoneType = typeof(PreciousStone);

                Reflector.Output("Lab5.Product", "product.txt"); //a

                var methods = Reflector.GetTypePublicMethods("Lab5.Stone"); //b
                Console.WriteLine("Методы Stone:");
                foreach (var m in methods.OrderBy(m => m.ToString()))
                    Console.WriteLine(m);

                var fieldsAndProps = Reflector.GetFieldsAndPropertiesInfo(productType); //c
                Console.WriteLine($"\nПоля и свойства {productType.Name}:");
                foreach (var info in fieldsAndProps)
                    Console.WriteLine($"{info.MemberType}: {info}");

                var interfaces = Reflector.GetAllInterfaces(preciousStoneType); //d
                Console.WriteLine($"\nРеализованные интерфейсы {preciousStoneType.Name}:");
                foreach (var i in interfaces)
                    Console.WriteLine(i.Name);
            try
            {
                Console.WriteLine("\nВведите тип параметра: "); //e
                string input = "System." + Console.ReadLine();
                Type t = Type.GetType(input, true); //Значение true, чтобы при невозможности найти тип создавалось исключение
                Console.WriteLine("\nМетоды класса String, содержащие параметры типа, введенного выше");
                var names = Reflector.GetMethodsContainsParamType("System.String", t);
                foreach (var name in names)
                    Console.WriteLine(name);
            }
            catch (TypeLoadException e)
            {

                Console.WriteLine("Вы ввели неверный тип параметра!");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Такого класса не существует!");
            }

                Console.WriteLine("\nВызов метода с параметрами, считанными из файла:"); //f
                Reflector.CallMethod("Lab5.Diamond", "DamageTest", "params.txt");

                var called = Reflector.CallMethod("System.Math", "Pow", "MathPow.txt");

                Console.WriteLine(called);
           
        }
    }
}
