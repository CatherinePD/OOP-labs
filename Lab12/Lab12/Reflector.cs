using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public static class Reflector
    {
        public static string GetTypeInfo(Type type)     //(a) получить содержимое класса
        {
            string result = $"Тип: {type.Name}\n";

            var fields = GetFields(type);
            if (fields.Any())
            {
                result += "Поля: \n";
                foreach (var item in fields)
                    result += $"    {item}\n";
            }

            var props = GetProps(type);
            if (props.Any())
            {
                result += "Свойства: \n";
                foreach (var item in props)
                    result += $"    {item}\n";
            }

            var methods = GetMethods(type);
            if (methods.Any())
            {
                result += "Методы: \n";
                foreach (var item in methods)
                    result += $"    {item}\n";
            }

            var constructors = GetConstructors(type);
            if (constructors.Any())
            {
                result += "Конструкторы: \n";
                foreach (var item in constructors)
                    result += $"    {item}\n";
            }

            return result;
        }
        public static void Output(string typeName, string fileName)     //(а) вывести содержимое в текстовый файл
        {
            Type type = Type.GetType(typeName);

            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (var line in GetTypeInfo(type).Split('\n')) //убирает \n и разбивает на строки
                    writetext.WriteLine(line);
            }
        }

        public static List<MethodInfo> GetTypePublicMethods(string typeName)    //(b)
        {
            Type type = Type.GetType(typeName);
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public static IEnumerable<MemberInfo> GetFieldsAndPropertiesInfo(Type type)     //(c)
        {
            var fields = GetFields(type).Cast<MemberInfo>().ToList(); //приводим их к общему базовому типу, чтобы совместить в список
            var props = GetProps(type).Cast<MemberInfo>().ToList();

            return fields.Union(props);
        }
        public static List<Type> GetAllInterfaces(Type type)      //(d)
        {
            return type.GetInterfaces().ToList();
        }

        public static IEnumerable<string> GetMethodsContainsParamType(string typeName, Type paramType)    //(e)
        {
            Type type = Type.GetType(typeName);

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            foreach (var method in methods.Where(p => p.GetParameters()
                                                       .Any(x => x.ParameterType == paramType)))
            {
                yield return method.ToString(); //возвращает по одному
            }
        }

        public static object CallMethod(string typeName, string methodName, string paramsFileName)  //(f)
        {
            Type type = Type.GetType(typeName);
            MethodInfo method = type.GetMethod(methodName);

            var paramTypes = method.GetParameters().Select(p => p.ParameterType).ToArray(); //создаем массив типов параметров
            var pars = GetParamsFromFile(paramsFileName); //считываем параметры из файла и передаем как object

            for (var i = 0; i < paramTypes.Length; i++)
            {
                pars[i] = Convert.ChangeType(pars[i], paramTypes[i]); //меняем типы считанных параметров на нужные
            }

            if (method.IsStatic) 
            {
                return method.Invoke(null, pars); //если метод статический, экземляр создавать не нужно, вызываем
            }

            var instance = Activator.CreateInstance(type); //создаем экземпляр заданного типа
            return method.Invoke(instance, pars); //вызываем метод у него
        }


        // private methods
        private static List<FieldInfo> GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .ToList();
        }
        private static List<PropertyInfo> GetProps(Type type)
        {
            return type.GetProperties().ToList();
        }
        private static List<MethodInfo> GetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .ToList();
        }
        private static List<ConstructorInfo> GetConstructors(Type type)
        {
            return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .ToList();
        }

        private static object[] GetParamsFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines.Cast<object>().ToArray();
        }
    }
}
