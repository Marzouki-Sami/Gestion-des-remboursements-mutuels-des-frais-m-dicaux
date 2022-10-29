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

namespace projet_csharp
{
    public partial class connex_employe : Form
    {
        public connex_employe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            
            cmd.Connection = cnx;
            cmd.CommandText = "select matricule,cin from employe where matricule=@m and cin=@c;";
            cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@c", int.Parse(this.textBox2.Text.Trim()));
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
          
                if (dr.HasRows)
                {
                    menu_employé form1 = new menu_employé();
                    form1.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show(" login ou mot de passe incorrect"); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            acceuil form = new acceuil();
            form.Show();
            this.Hide();
        }
    }
}
