// SocketClient.cs
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{

    class Program
    {


        static void Main(string[] args)
        {
            try
            {
                SendMessageFromSocket(11000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private string RemoveSpaces(string inputString)
        {
            inputString = inputString.Replace(" ", string.Empty);
            inputString = inputString.Trim().Replace(" ", string.Empty);
            while (inputString.Contains("(-"))
            {
                int pos = inputString.IndexOf("(-");
                String s1 = inputString.Substring(pos + 1);
                int pos2 = s1.IndexOf(")");
                //число 
                String s2 = s1.Substring(1, pos2 - 1);
                String x1 = inputString.Substring(0, pos);
                String x2 = s1.Substring(pos2 + 1);
                inputString = x1 + "(" + s2 + " - 2 * " + s2 + ")" + x2;


            }
        }
            static void SendMessageFromSocket(int port)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);

            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();

            Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);

            Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            // Используем рекурсию для неоднократного вызова SendMessageFromSocket()
            if (message.IndexOf("<TheEnd>") == -1)
                SendMessageFromSocket(port);

            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}