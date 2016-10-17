using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class Registration : Form
    {
        static readonly string _connectionsString =
     @"Data Source=ANNAPC\SQLEXPRESS; Initial Catalog = ChatDB; Integrated Security = True";

        public Registration()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(_connectionsString))
            using (SqlCommand command = conn.CreateCommand())
            {
                conn.Open();
                if (textBox4.Text == textBox2.Text)
                {
                    command.CommandText = "Insert into ChatClient values ('" + textBox1.Text + "', '" + textBox4.Text + "')";
                }
                else
                {
                    MessageBox.Show("Passwords do not match");
                }
                command.CommandType = CommandType.Text;
                MessageBox.Show("Registration successful!");
                ChatForm chatform = new ChatForm(textBox1.Text, textBox2.Text);
                chatform.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
