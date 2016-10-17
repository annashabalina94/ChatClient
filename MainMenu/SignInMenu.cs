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
    public partial class SignInMenu : Form
    {
        static readonly string _connectionsString =
       @"Data Source=ANNAPC\SQLEXPRESS; Initial Catalog = ChatDB; Integrated Security = True";

        public SignInMenu()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
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
                 command.CommandText = "Select * From ChatClient Where (Name ='"+textBox1.Text+"' and Password ='"+textBox2.Text+"')";
                 command.CommandType = CommandType.Text;
                 using (SqlDataReader reader = command.ExecuteReader())
                 {     
            if(reader.Read())
            {
                ChatForm chatform = new ChatForm(textBox1.Text, textBox2.Text);
                chatform.Show();
                this.Close();
            }
             else
            {
                MessageBox.Show("Invalid name or password");
            }
                 }
             }
        }
    }
}
