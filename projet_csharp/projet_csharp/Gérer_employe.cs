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
    public partial class Gérer_employe : Form
    {
        
        public Gérer_employe()
        {
            InitializeComponent();
        }

        Boolean mat(string ch)
        {
            return Regex.IsMatch(ch, @"^[a-zA-Z0-9]+$") && ch.Length != 0;
        }

        Boolean alphab(string ch)
        {
            return Regex.IsMatch(ch, @"^[a-zA-Z]+$") && ch.Length != 0;
        }

        Boolean alphanum(string ch)
        {
            return Regex.IsMatch(ch, @"^[a-zA-Z0-9]+$") && ch.Length != 0;
        }

        Boolean num(string ch)
        {
            return Regex.IsMatch(ch, @"^[0-9]+$") && ch.Length != 0;
        }


        Boolean reel(string ch)
        {
            return Regex.IsMatch(ch, "\\d+((,|\\.)\\d+)?$") && ch.Length != 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            cmd.Connection = cnx;
            cmd1.Connection = cnx;
            cnx.Open();
            bool existe = false;
            cmd1.CommandText = "select * from employe where matricule = @m ;";
            cmd1.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
            SqlDataReader dr0 = cmd1.ExecuteReader();
            if (dr0.HasRows)
                existe = true;
            dr0.Close();
            if (existe)
                MessageBox.Show("matricule employé déja existe");
            else
            {
                if ((mat(textBox1.Text)) && (alphab(textBox2.Text)) && (alphab(textBox3.Text)) && (num(textBox4.Text)) && (alphanum(textBox5.Text)) && (num(textBox6.Text)) && (dateTimePicker1.Value != null) && (alphanum(textBox7.Text)) && (alphab(textBox8.Text)) && (alphab(textBox9.Text)) && (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true || radioButton4.Checked == true))
                {
                    cmd.CommandText = "insert into employe(matricule,nom,prenom,cin,datenaissance,adresse,grade,numtel,codecnam,etatcivil,nomconjoint,prenomconjoint,nbrenfants)" +
                    " values (@m,@nom,@prenom,@c,@datenais,@adresse,@grade,@num,@codecnam,@e,@nomconj,@prenomconj,@enf) ;";
                    cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@nom", this.textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@prenom", this.textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@c", int.Parse(this.textBox4.Text));
                    cmd.Parameters.AddWithValue("@datenais", this.dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@adresse", this.textBox5.Text.Trim());
                    if (radioButton1.Checked == true)
                        cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton1.Text));
                    if (radioButton2.Checked == true)
                        cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton2.Text));
                    if (radioButton3.Checked == true)
                        cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton3.Text));
                    if (radioButton4.Checked == true)
                        cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton4.Text));
                    cmd.Parameters.AddWithValue("@num", this.textBox6.Text);
                    cmd.Parameters.AddWithValue("@codecnam", int.Parse(this.textBox7.Text.Trim()));
                    if (comboBox1.SelectedIndex == 0)
                        cmd.Parameters.AddWithValue("@e", "marié");
                    if (comboBox1.SelectedIndex == 1)
                        cmd.Parameters.AddWithValue("@e", "célibataire");
                    if (comboBox1.SelectedIndex == 2)
                        cmd.Parameters.AddWithValue("@e", "divorcé");
                    cmd.Parameters.AddWithValue("@nomconj", this.textBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@prenomconj", this.textBox9.Text.Trim());
                    if (comboBox2.SelectedIndex == 0)
                        cmd.Parameters.AddWithValue("@enf", "0");
                    if (comboBox2.SelectedIndex == 1)
                        cmd.Parameters.AddWithValue("@enf", "1");
                    if (comboBox2.SelectedIndex == 2)
                        cmd.Parameters.AddWithValue("@enf", "2");
                    if (comboBox2.SelectedIndex == 3)
                        cmd.Parameters.AddWithValue("@enf", "3");
                    if (comboBox2.SelectedIndex == 4)
                        cmd.Parameters.AddWithValue("@enf", "4");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("employé ajouté avec succés");
                }
                else
                {
                    if (!mat(textBox1.Text))
                        MessageBox.Show(" matricule invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!alphab(textBox2.Text))
                        MessageBox.Show(" nom invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!alphab(textBox3.Text))
                        MessageBox.Show(" prénom invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!num(textBox4.Text))
                        MessageBox.Show(" CIN invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!alphanum(textBox5.Text))
                        MessageBox.Show(" adresse invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!num(textBox6.Text))
                        MessageBox.Show(" numero de telephone invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!alphanum(textBox7.Text))
                        MessageBox.Show(" code cnam invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!alphab(textBox8.Text))
                        MessageBox.Show(" Prenom Conjoint code cnam invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!alphab(textBox9.Text))
                        MessageBox.Show(" nom Conjoint : invalide", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            radioButton1.Checked= false;
            radioButton2.Checked= false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
        }

        private void  textBox1_TextChanged(object sender, EventArgs  e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cnx.Open();
            cmd.CommandText = "select * from employe ;";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12]);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "select matricule from employe where matricule=@m;";
            cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
            cnx.Open();
            if (!mat(textBox1.Text))
                MessageBox.Show(" matricule invalide", "ERROR",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    SqlCommand del = new SqlCommand();
                    del.Connection = cnx;
                    dr.Close();
                    del.CommandText = "delete from employe where matricule=@a;";
                    del.Parameters.AddWithValue("@a", this.textBox1.Text.Trim());
                    del.ExecuteNonQuery();
                    MessageBox.Show("employé supprimé avec succés ");
                    dataGridView1.Rows.Clear();
                }
                else
                    MessageBox.Show("employé innexistant dans la base de donnée ");
            }
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cnx.Open();
            if (!mat(textBox1.Text))
                MessageBox.Show(" matricule invalide", "ERROR",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                cmd.CommandText = "select * from employe where matricule=@m;";
                cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());

                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12]);
                    }

                }
                else
                    MessageBox.Show("employé innexistant dans la base de donnée ");
            }
            textBox1.Text = "";
        }


        private void rempform()
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "select * from employe where matricule=@m;";
            cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (!mat(textBox1.Text))
                MessageBox.Show(" matricule invalide", "ERROR",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        this.textBox2.Text = dr[1].ToString();
                        this.textBox3.Text = dr[2].ToString();
                        this.textBox4.Text = dr[3].ToString();
                        this.dateTimePicker1.Value = (DateTime)dr[4];
                        this.textBox5.Text = dr[5].ToString();
                        if (dr[6].ToString() == "1")
                        {
                            radioButton1.Checked = true;
                        }

                        if (dr[6].ToString() == "2")
                        {
                            radioButton2.Checked = true;
                        }

                        if (dr[6].ToString() == "3")
                        {
                            radioButton3.Checked = true;
                        }

                        if (dr[6].ToString() == "4")
                        {
                            radioButton4.Checked = true;
                        }

                        this.textBox6.Text = dr[7].ToString();
                        this.textBox7.Text = dr[8].ToString();
                        if (dr[9].ToString() == "marié")
                            comboBox1.SelectedIndex = 0;
                        if (dr[9].ToString() == "célibataire")
                            comboBox1.SelectedIndex = 1;
                        if (dr[9].ToString() == "divorcé")
                            comboBox1.SelectedIndex = 2;

                        this.textBox8.Text = dr[10].ToString();
                        this.textBox9.Text = dr[11].ToString();

                        if (dr[12].ToString() == "1")
                            comboBox2.SelectedIndex = 0;
                        if (dr[12].ToString() == "2")
                            comboBox2.SelectedIndex = 1;
                        if (dr[12].ToString() == "3")
                            comboBox2.SelectedIndex = 2;
                        if (dr[12].ToString() == "4")
                            comboBox2.SelectedIndex = 3;
                        if (dr[12].ToString() == "5")
                            comboBox2.SelectedIndex = 4;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = @"Data Source=WAL3A\SQLEXPRESS;Initial Catalog=csharp;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cnx.Open();
            if ((mat(textBox1.Text)) && (alphab(textBox2.Text)) && (alphab(textBox3.Text)) && (num(textBox4.Text)) && (alphanum(textBox5.Text)) && (num(textBox6.Text)) && (dateTimePicker1.Value != null) && (alphanum(textBox7.Text)) && (alphab(textBox8.Text)) && (alphab(textBox9.Text)) && (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true || radioButton4.Checked == true))
            {
                cmd.CommandText = "update  employe" +
                " set  nom=@nom,prenom=@prenom,cin=@c,datenaissance=@datenais,adresse=@adresse,grade=@grade,numtel=@num,codecnam=@codecnam,etatcivil=@e,nomconjoint=@nomconj,prenomconjoint=@prenomconj,nbrenfants=@enf where matricule=@m ;";
                cmd.Parameters.AddWithValue("@m", this.textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@nom", this.textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@prenom", this.textBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@c", int.Parse(this.textBox4.Text));
                cmd.Parameters.AddWithValue("@datenais", this.dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@adresse", this.textBox5.Text.Trim());
                if (radioButton1.Checked == true)
                    cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton1.Text));
                if (radioButton2.Checked == true)
                    cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton2.Text));
                if (radioButton3.Checked == true)
                    cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton3.Text));
                if (radioButton4.Checked == true)
                    cmd.Parameters.AddWithValue("@grade", int.Parse(this.radioButton4.Text));
                cmd.Parameters.AddWithValue("@num", this.textBox6.Text);
                cmd.Parameters.AddWithValue("@codecnam", int.Parse(this.textBox7.Text.Trim()));
                if (comboBox1.SelectedIndex == 0)
                    cmd.Parameters.AddWithValue("@e", "marié");
                if (comboBox1.SelectedIndex == 1)
                    cmd.Parameters.AddWithValue("@e", "célibataire");
                if (comboBox1.SelectedIndex == 2)
                    cmd.Parameters.AddWithValue("@e", "divorcé");
                cmd.Parameters.AddWithValue("@nomconj", this.textBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@prenomconj", this.textBox9.Text.Trim());
                if (comboBox2.SelectedIndex == 0)
                    cmd.Parameters.AddWithValue("@enf", "1");
                if (comboBox2.SelectedIndex == 1)
                    cmd.Parameters.AddWithValue("@enf", "2");
                if (comboBox2.SelectedIndex == 2)
                    cmd.Parameters.AddWithValue("@enf", "3");
                if (comboBox2.SelectedIndex == 3)
                    cmd.Parameters.AddWithValue("@enf", "4");
                if (comboBox2.SelectedIndex == 4)
                    cmd.Parameters.AddWithValue("@enf", "5");
                cmd.ExecuteNonQuery();
                MessageBox.Show("employé modifié avec succés");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                comboBox1.SelectedText = "";
                comboBox2.SelectedText = "";
            }
            else
            {
                if (!mat(textBox1.Text))
                    MessageBox.Show(" matricule invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!alphab(textBox2.Text))
                    MessageBox.Show(" nom invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!alphab(textBox3.Text))
                    MessageBox.Show(" prénom invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!num(textBox4.Text))
                    MessageBox.Show(" CIN invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!alphanum(textBox5.Text))
                    MessageBox.Show(" adresse invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!num(textBox6.Text))
                    MessageBox.Show(" numero de telephone invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!alphanum(textBox7.Text))
                    MessageBox.Show(" code cnam invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!alphab(textBox8.Text))
                    MessageBox.Show(" Prenom Conjoint code cnam invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!alphab(textBox9.Text))
                    MessageBox.Show(" nom Conjoint : invalide", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Gérer_employe_Load(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rempform();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            menu_administrateur form = new menu_administrateur();
            form.Show();
            this.Hide();
        }
    }
}
