using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projet_csharp
{
    public partial class menu_agent_social : Form
    {
        public menu_agent_social()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gérer_bulletin g = new gérer_bulletin();
            g.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rapport form = new rapport();
            form.Show();
            this.Hide();
        }

        private void menu_agent_social_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            acceuil form = new acceuil();
            form.Show();
            this.Hide();
        }
    }
}
