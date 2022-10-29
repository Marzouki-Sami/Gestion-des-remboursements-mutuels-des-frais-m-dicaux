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
    public partial class connex_agentsocial : Form
    {

        public connex_agentsocial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "select loginn,motdepasse_as from agentsociale where loginn=@m and motdepasse_as=@c;";
            cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@c", this.textBox2.Text.Trim());
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                menu_agent_social form1 = new menu_agent_social() ;
                form1.Show();
                this.Hide();
            }
            else
                MessageBox.Show(" login ou mot de passe incorrecte");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            acceuil form = new acceuil();
            form.Show();
            this.Hide();
        }

        private void connex_agentsocial_Load(object sender, EventArgs e)
        {

        }
    }
}
