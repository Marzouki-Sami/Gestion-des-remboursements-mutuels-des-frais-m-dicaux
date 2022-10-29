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
using System.Globalization;

namespace projet_csharp
{
    public partial class gérer_bulletin : Form
    {
        string grade;
        float x;

        public gérer_bulletin()
        {
            InitializeComponent();
        }

        Boolean IsNull(object source)
        {
            return source == null;
        }

        Boolean mat(string ch)
        {
            return Regex.IsMatch(ch, @"^[a-zA-Z0-9]+$") && ch.Length != 0;
        }

        Boolean num(string ch)
        {
            return Regex.IsMatch(ch, @"^[0-9]+$") && ch.Length != 0;
        }

        Boolean reel(string ch)
        {
            return Regex.IsMatch(ch, @"^([0-9]*[.])?[0-9]+") && ch.Length != 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            if (!num(textBox1.Text))
                MessageBox.Show(" numero invalide", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                cmd.CommandText = "select * from bulletin where numero=@n;";
                cmd.Parameters.AddWithValue("@n", this.textBox1.Text.Trim());
                cnx.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    SqlCommand del = new SqlCommand();
                    del.Connection = cnx;
                    dr.Close();
                    del.CommandText = "delete from bulletin where numero=@a;";
                    del.Parameters.AddWithValue("@a", this.textBox1.Text.Trim());
                    del.ExecuteNonQuery();
                    MessageBox.Show("employé supprimé avec succés ");
                    dataGridView1.Rows.Clear();
                }
                else
                    MessageBox.Show("employé innexistant dans la base de donnée ");
            }
        }

        private void Rempformbul()
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            if (!num(textBox1.Text))
                MessageBox.Show(" numero invalide", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                cmd.CommandText = "select * from bulletin where numero = @k;";
                cmd.Parameters.AddWithValue("@k", this.textBox1.Text.Trim());
                cnx.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (!num(textBox1.Text))
                    MessageBox.Show("le numero de bulletinest invalide");
                else
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            this.dateTimePicker1.Value = (DateTime)dr[1];
                            if (dr[2].ToString() == "1")
                                radioButton1.Checked = true;
                            else
                                radioButton2.Checked = true;

                            this.textBox3.Text = dr[3].ToString();
                            this.textBox2.Text = dr[4].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bulletin introuvable");
                    }
                }
            }
        }
        static Double Eval(String expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            return Convert.ToDouble(table.Compute(expression, String.Empty));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rempformbul();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            SqlCommand cmd3 = new SqlCommand();

            cmd.Connection = cnx;
            cmd1.Connection = cnx;
            cmd2.Connection = cnx;
            cmd3.Connection = cnx;
            cnx.Open();
            bool existe = false;
            cmd1.CommandText = "select * from bulletin where numero = @n ;";
            cmd1.Parameters.AddWithValue("@n", this.textBox1.Text.Trim());
            SqlDataReader dr0 = cmd1.ExecuteReader();
            if (dr0.HasRows)
                existe = true;
            dr0.Close();
            if (existe)
                MessageBox.Show("numero bulletin déja existe");
            else
            {
                if ((num(textBox1.Text)) && (reel(textBox3.Text)) && (mat(textBox2.Text)) && (radioButton1.Checked || radioButton2.Checked))
                {
                    cmd.CommandText = "insert into bulletin(numero,date_dépot,acte,frais_acte,matricule,remboursement)" +
                    " values (@num,@date,@acte,@frais,@matricule,@remboursement) ;";
                    cmd.Parameters.AddWithValue("@num", this.textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@date", this.dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@matricule", this.textBox2.Text.Trim());
                    if (radioButton1.Checked == true)
                        cmd.Parameters.AddWithValue("@acte", this.radioButton1.Text);
                    if (radioButton2.Checked == true)
                        cmd.Parameters.AddWithValue("@acte", this.radioButton2.Text);
                    if (radioButton3.Checked == true)
                        cmd.Parameters.AddWithValue("@acte", this.radioButton3.Text);
                    cmd.Parameters.AddWithValue("@frais", this.textBox3.Text.Trim());

                    cmd2.CommandText = "select grade from employe where matricule = @m;";
                    cmd2.Parameters.AddWithValue("@m", this.textBox2.Text.Trim());
                    SqlDataReader dr1 = cmd2.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            grade = dr1[0].ToString();
                        }
                        dr1.Close();

                        cmd3.CommandText = "select sum(remboursement) from bulletin where matricule = @m and year(date_dépot) = @d;";
                        cmd3.Parameters.AddWithValue("@m", this.textBox2.Text.Trim());
                        cmd3.Parameters.AddWithValue("@d", this.dateTimePicker1.Value.Year);
                        SqlDataReader dr = cmd3.ExecuteReader();
                        string ch;
                        ch = textBox3.Text + "*0.3";
                        double f = Eval((string)ch);
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                if (dr[0].ToString()=="")
                                {           
                                    x = (float)f;
                                }
                                else
                                {
                                    x = (float)(float.Parse(dr[0].ToString()) + f);
                                }
                                float oldplafond;
                                    switch (grade)
                                    {
                                        case "1":
                                            {

                                            if (dr[0].ToString() == "")
                                            {
                                                 oldplafond = 0;
                                            }
                                            else
                                            {
                                                oldplafond = float.Parse(dr[0].ToString());
                                            }

                                            if (x >= 1800)
                                                MessageBox.Show("votre plafond est égal à 1800");
                                            else
                                                MessageBox.Show("votre plafond est égal à "+x.ToString());

                                            if (oldplafond >= 1800)
                                                    cmd.Parameters.AddWithValue("@remboursement", 0);
                                                else
                                                {

                                                    if (x > 1800)
                                                        cmd.Parameters.AddWithValue("@remboursement", 1800 - oldplafond);
                                                    else
                                                        cmd.Parameters.AddWithValue("@remboursement", f);

                                                }
                                                break;
                                            }
                                        case "2":
                                            {
                                            if (dr[0].ToString() == "")
                                            {
                                                oldplafond = 0;
                                            }
                                            else
                                            {
                                                oldplafond = float.Parse(dr[0].ToString());
                                            }
                                            if (x >= 1400)
                                                MessageBox.Show("votre plafond est égal à 1400");
                                            else
                                                MessageBox.Show("votre plafond est égal à " + x.ToString());
                                            if (oldplafond >= 1400)
                                                    cmd.Parameters.AddWithValue("@remboursement", 0);
                                                else
                                                {
                                                    if (x > 1400)
                                                        cmd.Parameters.AddWithValue("@remboursement", 1400 - oldplafond);
                                                    else
                                                        cmd.Parameters.AddWithValue("@remboursement", f);

                                                }
                                                break;
                                            }
                                        case "3":
                                            {
                                            if (dr[0].ToString() == "")
                                            {
                                                oldplafond = 0;
                                            }
                                            else
                                            {
                                                oldplafond = float.Parse(dr[0].ToString());
                                            }
                                            if (x >= 1000)
                                                MessageBox.Show("votre plafond est égal à 1000");
                                            else
                                                MessageBox.Show("votre plafond est égal à " + x.ToString());
                                            if (oldplafond >= 1000)
                                                    cmd.Parameters.AddWithValue("@remboursement", 0);
                                                else
                                                {
                                                    if (x > 1000)
                                                        cmd.Parameters.AddWithValue("@remboursement", 1000 - oldplafond);
                                                    else
                                                        cmd.Parameters.AddWithValue("@remboursement", f);

                                                }
                                                break;
                                            }
                                        default:
                                            {
                                            if (dr[0].ToString() == "")
                                            {
                                                oldplafond = 0;
                                            }
                                            else
                                            {
                                                oldplafond = float.Parse(dr[0].ToString());
                                            }
                                            if (x >= 600)
                                                MessageBox.Show("votre plafond est égal à 600");
                                            else
                                                MessageBox.Show("votre plafond est égal à " + x.ToString());
                                            if (oldplafond >= 600)
                                                    cmd.Parameters.AddWithValue("@remboursement", 0);
                                                else
                                                {
                                                    if (x > 600)
                                                        cmd.Parameters.AddWithValue("@remboursement", 600 - oldplafond);
                                                    else
                                                        cmd.Parameters.AddWithValue("@remboursement", f);
                                                }
                                                break;
                                            }
                                    }
                            }
                        }
                        dr.Close();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Bulletin ajouté avec succés");
                    textBox1.Text = "";
                    textBox3.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    if (!mat(textBox2.Text))
                        MessageBox.Show("matricule invalide");
                    if (!num(textBox1.Text))
                        MessageBox.Show("numero invalide");
                    if (!reel(textBox3.Text))
                        MessageBox.Show("frais invalide");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            dataGridView1.Rows.Clear();
            cnx.Open();
            cmd.CommandText = "select * from bulletin where matricule=@m;";
            cmd.Parameters.AddWithValue("@m", this.textBox2.Text.Trim());
            if (textBox2.Text == "")
                MessageBox.Show("veuillez saisir la matricule de l'employé");
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[4], dr[2], dr[3]);
                }
                textBox2.Text = "";
            }
            else
                MessageBox.Show("cet employé n'a pas de bulletin");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cnx.Open();
            if (!num(textBox1.Text))
                MessageBox.Show(" numero invalide", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                cmd.CommandText = "select * from bulletin where numero=@m;";
                cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0], dr[1], dr[4], dr[2], dr[3]);
                    }

                }
                else
                    MessageBox.Show("bulletin innexistant dans la base de donnée ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cnx.Open();
            if ((num(textBox1.Text)) && (reel(textBox3.Text)) && (mat(textBox2.Text)) && (radioButton1.Checked || radioButton2.Checked))
            {
                cmd.CommandText = "update  bulletin set date_dépot=@date,acte=@acte,frais_acte=@frais where numero=@num;";
                cmd.Parameters.AddWithValue("@num", this.textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@date", this.dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@matricule", this.textBox2.Text.Trim());
                if (radioButton1.Checked == true)
                    cmd.Parameters.AddWithValue("@acte", this.radioButton1.Text);
                if (radioButton2.Checked == true)
                    cmd.Parameters.AddWithValue("@acte", this.radioButton2.Text);
                cmd.Parameters.AddWithValue("@frais", this.textBox3.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bulletin modifieé avec succés");
                textBox1.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
            else
            {
                if (!num(textBox1.Text))
                    MessageBox.Show(" numero invalide", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!reel(textBox3.Text))
                    MessageBox.Show(" frais invalide", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!num(textBox2.Text))
                    MessageBox.Show(" matricule invalide", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            menu_agent_social form=new menu_agent_social();
            form.Show();
            this.Hide();
        }
    }
}
