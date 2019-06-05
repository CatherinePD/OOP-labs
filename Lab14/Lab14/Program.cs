using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Lab14.Serialization;
using Lab5;

namespace Lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            var diamond = new Diamond {Price = 113, Weight = 45};
            var stone1 = new Stone { Color = "Белый", Price = 20, Status = ProductStatus.Ready, Weight = 100 };
            var pStone1 = new PreciousStone { Color = "Желтый", Price = 40, Status = ProductStatus.Ready, Weight = 40 };
            var ruby1 = new Ruby { Price = 57, Status = ProductStatus.None, Weight = 30 };
            var diamond1 = new Diamond { Price = 89, Status = ProductStatus.Ready, Weight = 28 };

            List<Stone> stones = new List<Stone> {
                stone1,
                pStone1,
                ruby1,
                diamond1,
                diamond };
            var necklace = new Necklace(stones);

            var binSerializer = new BinarySerializer<Diamond>("doc/diamond.txt");
            binSerializer.Serialize(diamond);
            var bsDiamond = binSerializer.Deserialize();
            Console.WriteLine(bsDiamond);

            var soapSerializer = new SoapSerializer<Diamond>("doc/diamond.soap");
            soapSerializer.Serialize(diamond);
            var ssDiamond = soapSerializer.Deserialize();
            Console.WriteLine(ssDiamond);

            var xmlSerializer = new XmlSerializer<Diamond>("doc/diamond.xml");
            xmlSerializer.Serialize(diamond);
            var xsDiamond = xmlSerializer.Deserialize();
            Console.WriteLine(xsDiamond);

            var jsonSerializer = new JsonSerializer<Diamond>("doc/diamond.json");
            jsonSerializer.Serialize(diamond);
            var jsDiamond = jsonSerializer.Deserialize();
            Console.WriteLine($"{jsDiamond} \n");

            var xml = new XmlSerializer<Necklace>("doc/necklace.xml");
            xml.Serialize(necklace);
            var xsNecklace = xml.Deserialize();

            var soapStones = new SoapSerializer<Stone[]>("doc/stones.soap");
            soapStones.Serialize(stones.ToArray());
            var ssStones = soapStones.Deserialize();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("doc/necklace.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            //xpath - язык запросов к XML-документам
            Console.WriteLine("Вывод элементов, у которых вес больше 35");
            XmlNodeList childnodes = xRoot.SelectNodes("//Stones/Stone[@Weight>35]");
            foreach (XmlNode n in childnodes)
            {
                var prodType = n.SelectSingleNode("ProductType").InnerText;
                var color = n.SelectSingleNode("Color").InnerText;
                var price = n.SelectSingleNode("Price").InnerText;
                Console.WriteLine($"{color} {prodType} - ${price}");
            }

            Console.WriteLine("\nВывод элементов всех элементов с типом Алмаз:");
            childnodes = xRoot.SelectNodes("//Stones/Stone[ProductType='Алмаз']");
            foreach (XmlNode n in childnodes)
            {
                var prodType = n.SelectSingleNode("ProductType").InnerText;
                var weight = n.SelectSingleNode("@Weight").Value;
                var price = n.SelectSingleNode("Price").InnerText;
                Console.WriteLine($"{prodType} - ${price}, {weight} грамм");
            }

            Console.WriteLine("Создание документа books.xml:");
            XDocument qDoc = new XDocument();
            XElement books = new XElement("books");

            XElement book1 = CreateBookElement("Neil Gaiman", "American Gods", 23, 432);
            XElement book2 = CreateBookElement("J. K. Rowling", "Harry Potter", 17, 345);
            XElement book3 = CreateBookElement("William Shakespeare", "Hamlet", 27, 290);
            XElement book4 = CreateBookElement("George Martin", "The Song of Ice and Fire", 30, 550);

            books.Add(book1, book2, book3, book4);

            qDoc.Add(books);
            qDoc.Save("doc/books.xml");

            Console.WriteLine("\nЭлементы у которых pages больше 350:");
            XDocument xdoc = XDocument.Load("doc/books.xml");

            var items = xdoc.Element("books").Elements("book").Where(xe => int.Parse(xe.Element("pages").Value) > 350).ToList();

            foreach (var item in items)
                Console.WriteLine(item.ToString());


            Console.WriteLine("\nКниги с ценой меньше 25:");
            var booksQuery = from xe in xdoc.Element("books").Elements("book")
                        where int.Parse(xe.Attribute("price").Value) < 25
                        select new
                        {
                            Name = xe.Element("name").Value,
                            Author = xe.Element("author").Value
                        };

            foreach (var book in booksQuery)
                Console.WriteLine($"{book.Author} - {book.Name}");


        }

        private static XElement CreateBookElement(string author, string name, int price, int pages)
        {
            XElement book = new XElement("book");

            XAttribute attrPrice = new XAttribute("price", price);

            XElement childPages = new XElement("pages", pages);
            XElement childAuthor = new XElement("author", author);
            XElement childName = new XElement("name", name);

            book.Add(attrPrice);
            book.Add(childAuthor, childName, childPages);
            return book;
        }
    }
}
