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
    public partial class Calcul_Frais : Form
    {
        private int som;

        public Calcul_Frais()
        {
            InitializeComponent();
        }

        Boolean mat(string ch)
        {
            return Regex.IsMatch(ch, @"^[a-zA-Z0-9]+$") && ch.Length != 0;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            if (!mat(textBox1.Text))
            {
                MessageBox.Show("matricule invalide");
            }
            else
            {
                if (radioButton1.Checked)
                {
                    cmd.CommandText = "select sum(remboursement) from bulletin where matricule = @m and month(date_dépot) = @d ;";
                    cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@d", this.dateTimePicker1.Value.Month);
                }
                else
                {
                    cmd.CommandText = "select sum(remboursement) from bulletin where matricule = @m and year(date_dépot) = @d ;";
                    cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@d", this.dateTimePicker1.Value.Year);
                }
                cnx.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                        textBox2.Text = dr[0].ToString();
                }
                else
                    MessageBox.Show("employe n'existe pas");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            menu_administrateur form = new menu_administrateur();
            form.Show();
            this.Hide();
        }
    }
}
