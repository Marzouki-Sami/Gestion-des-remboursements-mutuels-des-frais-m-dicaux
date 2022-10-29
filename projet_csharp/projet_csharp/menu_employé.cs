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

namespace projet_csharp
{
    public partial class menu_employé : Form
    {
        public menu_employé()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string res;
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cnx.Open();
            cmd.CommandText = "select numero,remboursement from bulletin where matricule = @m and day(date_dépot) = @d and month(date_dépot) = @mo and year(date_dépot) = @y;";
            cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@d", this.dateTimePicker1.Value.Day);
            cmd.Parameters.AddWithValue("@mo", this.dateTimePicker1.Value.Month);
            cmd.Parameters.AddWithValue("@y", this.dateTimePicker1.Value.Year);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if ((float)dr[1] == 0)
                        res = "refusé";
                    else
                        res = "accepté";
                    dataGridView1.Rows.Add(dr[0], res);
                }
            }
            else
                MessageBox.Show("aucun bulletin");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            acceuil form = new acceuil();
            form.Show();
            this.Hide();
        }
    }
}
