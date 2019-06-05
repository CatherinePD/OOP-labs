using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Lab14.Serialization;

namespace Server
{
    class SocketServer
    {
        static int port = 8005; // порт для приема входящих запросов
        static void Main(string[] args)
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                Console.ResetColor();

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    int bytesLenght = 0; // количество полученных байтов
                    byte[] data = new byte[512]; // буфер для получаемых данных
                    List<byte> byteStream = new List<byte>();
                    var binarySerializer = new BinarySerializer<object>();

                    do
                    {
                        bytesLenght = handler.Receive(data);

                        byteStream.AddRange(data);
                    }
                    while (handler.Available > 0);

                    var result = binarySerializer.DeserializeBytes(byteStream.ToArray());

                    Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]: {result.ToString()}");

                    // отправляем ответ
                    string message = "ваше сообщение доставлено";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}