using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelDeVeiculos
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void inícioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            this.Hide();
            form1.Show();
        }
    }
}
