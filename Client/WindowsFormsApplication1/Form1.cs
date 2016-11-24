using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainP;
using System.Threading;
using System.Net.Sockets;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    

    public partial class Form1 : Form
    {
        public static string TextChangedv;
        public static string InputText;
        public static string userName;
        // static string userName;
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        public static NetworkStream stream;
        //public static string message;


        public void main222()
        {

            client = new TcpClient();

            client.Connect(host, port); //подключение клиента
            stream = client.GetStream(); // получаем поток
            string userName;
            userName = WindowsFormsApplication1.Form1.userName;
            string message = userName;
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);

            // запускаем новый поток для получения данных
            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start(); //старт потока
                                   //Console.WriteLine("Добро пожаловать, {0}", userName);

            listView1.Items.Add(String.Format("Добро пожаловать, {0}", userName));
           // textBox3.Text = String.Format("Добро пожаловать, {0}", userName) ;
        }

        public static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }

        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    //Console.WriteLine(message);//вывод сообщения
                    // WindowsFormsApplication1.Form1.textBox3_TextChanged = message;
                    //textBox3.Text = message;
                    //  TextBox textBox3 = new TextBox();
                   
                    // textBox3.Text = String.Format(message);
                    //ListViewItem ListView1 = new ListViewItem();
                    //ListView1.Text = message;
                    listView1.Items.Add(message);
                }
                catch
                {
                    //TextBox textBox3 = new TextBox();
                    listView1.Items.Add(String.Format("Подключение прервано!"));
                   // textBox3.Text = String.Format("Подключение прервано!");
                    //Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    //Console.ReadLine();
                    Disconnect();
                }
            }
        }

        //public static void SendMessage()
        //{
        //    Console.WriteLine("Введите сообщение: ");
        //    while (true)
        //    {
               
        //    }
        //}


        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Введите свое имя:", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }
      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextChangedv = textBox1.Text;
            
        }

        public void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Enter)
            //{

            //    TextChangedv = textBox1.Text;

            //    // MainP.Program.SendMessage();
            //    wad();

            //}
            if (e.KeyCode == Keys.Enter)
            {

                //string message = Console.ReadLine();
                string message;
                //await 
                //while (WindowsFormsApplication1.Form1.ok == false)
                //{
                //}
                message = TextChangedv;
                //message = WindowsFormsApplication1.Form1.TextChangedv;
                //await WindowsFormsApplication1.Form1.
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
                TextChangedv = "";
                textBox1.Text = "";
                
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           // MainP.Program Start = new MainP.Program();
            main222();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                userName = textBox2.Text;
                textBox2.Enabled = false;
            }      
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        //public static void timer()
        //{
        //    System.Threading.Thread.Sleep(500);
        //    string Text = message;

        //}

        public void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textBox3.Text = Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
