using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "Az összes mező kitöltése köelező!";

            string sportag = textBox3.Text;
            string datum = textBox2.Text;
            string idotartam = textBox4.Text;
            string helyszin = textBox1.Text;
            string[] textboxok = new string[] { sportag, datum, idotartam, helyszin };
            for (int i = 0; i < textboxok.Length; i++)
            {
                if (textboxok[i] == "")
                {
                    MessageBox.Show(message,"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
