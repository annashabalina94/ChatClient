using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class ChatForm : Form
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        private const string host = "192.168.0.103";
        private const int port = 27000;
        static TcpClient client;
        static NetworkStream stream;

        public ChatForm(string userName, string password)
        {
            InitializeComponent();
            this.UserName = userName;
            this.Password = password;
            label1.Text = userName;
            Connect();
        }

        public void Connect()
        {
            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = UserName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = this.UserName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // отправка сообщений
            string message = textBox1.Text;
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
            listBox1.Items.Add("You:" + message);
            textBox1.Clear();
        }

        void ReceiveMessage()
        {
            // получение сообщений
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
                    BeginInvoke(new Action<ChatForm>((sender) =>
                    {
                        listBox1.Items.Add(message);
                    }), this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    BeginInvoke(new Action<ChatForm>((sender) =>
                    {
                        listBox1.Items.Add("Connection lost!"); //соединение было прервано
                    }), this);
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
        }
    }
}
