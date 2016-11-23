using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        public string InputText;
        public bool ok=false;
        public Form1()
        {
            
            InitializeComponent();

            

        }




        void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
            //if (textBox1.Text == String.Empty)
            //{

                //}

                //if (e.KeyCode == '/r')
                //{
                //    //необходимые действия
                //}

        }

        private void textBox1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InputText = textBox1.Text;
                ok = true;
                textBox3.Text = InputText;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client1();
            //textBox2.Text = textBox1.Text;
            //InputText = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }





      public  void Client1()
        {
            //try
            //{

                SendMessageFromSocket(11000);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //finally
            //{
            //   // Console.ReadLine();
            //}
        }

        public void SendMessageFromSocket2()
        {

            Console.Write("Введите сообщение: ");
            //string message = Console.ReadLine();
            ok = false;
            textBox2.Text = "Введите сообщение:";


        }
        public void SendMessageFromSocket(int port)
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

           


            Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());




            //// Получаем ответ от сервера
            //int bytesRec = sender.Receive(bytes);
            //string OtServera = Encoding.UTF8.GetString(bytes, 0, bytesRec);

            //textBox2.Text = OtServera;
            //Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            SendMessageFromSocket2();


            string message = InputText;
            
            byte[] msg = Encoding.UTF8.GetBytes(message);
            int bytesSent = sender.Send(msg);
            // Используем рекурсию для неоднократного вызова SendMessageFromSocket()
            if (message.IndexOf("<TheEnd>") == -1)
                SendMessageFromSocket(port);

            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //Client();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }



}

