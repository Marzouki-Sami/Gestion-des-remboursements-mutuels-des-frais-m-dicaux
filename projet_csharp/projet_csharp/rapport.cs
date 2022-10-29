using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projet_csharp
{
    public partial class rapport : Form
    {
        public rapport()
        {
            InitializeComponent();
        }

        Boolean num(string ch)
        {
            return Regex.IsMatch(ch, @"^[0-9]+$") && ch.Length != 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string res;
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cnx.Open();
            if (!num(textBox1.Text))
                MessageBox.Show("numero bulletin invalaide");
            else
            {
                cmd.CommandText = "select remboursement from bulletin where numero = @n;";
                cmd.Parameters.AddWithValue("@n", this.textBox1.Text.Trim());


                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if ((float)dr[0] == 0)
                        {
                            textBox2.Text = "refusé";
                            textBox3.Text = "0";
                        }
                        else
                        {
                            textBox2.Text = "accepté";
                            textBox3.Text = dr[0].ToString();

                        }
                    }
                }
                else
                    MessageBox.Show("aucun bulletin");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            menu_agent_social form = new menu_agent_social();
            form.Show();
            this.Hide();
        }
    }
}
