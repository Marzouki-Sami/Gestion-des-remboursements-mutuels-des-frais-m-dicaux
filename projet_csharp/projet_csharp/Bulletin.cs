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
using System.Text.RegularExpressions;

namespace projet_csharp
{
    public partial class Bulletin : Form
    {
        public Bulletin()
        {
            InitializeComponent();
        }

        Boolean mat(string ch)
        {
            return Regex.IsMatch(ch, @"^[a-zA-Z0-9]+$") && ch.Length!=0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            dataGridView1.Rows.Clear();
            cnx.Open();
            cmd.CommandText = "select * from bulletin";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[4], dr[2], dr[3]);
                }
            }
            else
                MessageBox.Show("aucun bulletin");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            dataGridView1.Rows.Clear();
            cnx.Open();
            cmd.CommandText = "select * from bulletin where matricule=@m;";
            cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());

            if (!mat(textBox1.Text))
                MessageBox.Show("matricule invalide");
            else
            {

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0], dr[1], dr[4], dr[2], dr[3]);
                    }
                }
                else
                    MessageBox.Show("cet employé n'a pas de bulletin");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap objBmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(objBmp,new Rectangle( 0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

            e.Graphics.DrawImage(objBmp, 150, 90);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            menu_administrateur form=new menu_administrateur();
            form.Show();
            this.Hide();
        }
    }
}
