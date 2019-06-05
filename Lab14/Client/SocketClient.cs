using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Lab14.Serialization;
using Lab5;

namespace Client
{
    class SocketClient
    {
        // порт и адрес сервера, к которому будем подключаться
        static int port = 8005;
        static string address = "127.0.0.1";
        static void Main(string[] args)
        {
            try
            {
                //сетевая конечная точка
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                //сокет - программный интерфейс для обеспечения обмена данными между процессами(абстрактный объект, представляющий конечную точку соединения)
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Клиент запущен");
                Console.ResetColor();

                var diamond = new Diamond{Price = 123, Weight = 48};
                var diamond2 = new Diamond { Price = 123, Weight = 48 };
                var ruby = new Ruby { Price = 100, Weight = 12 };
                var stones = new Stone[] {diamond, ruby, diamond2};

                var serializer = new BinarySerializer<Necklace>();
                var data = serializer.SerializeBytes(new Necklace(stones));

                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("ответ сервера: " + builder.ToString());

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}