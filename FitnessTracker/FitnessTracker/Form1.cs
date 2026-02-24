using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using static System.Net.Mime.MediaTypeNames;

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
            if (!Directory.Exists(Datas.dictionaryPath))
            {
                MessageBox.Show("Hibás elérési útvonal van megadva kérlek javítsd ki a Datas.cs fileban!", "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sportag = textBox3.Text;
            DateTime datum = dateTimePicker1.Value;
            string idotartam = textBox4.Text;
            string helyszin = textBox1.Text;

            Person person = new Person
            {
                Sportag = sportag,
                Datum = datum,
                Idotartam = idotartam,
                Helyszin = helyszin
            };
            if (PersonValidator.ValidatePerson(person))
            {
                CsvService.Write(Datas.filePath, person);
                textBox1.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dateTimePicker1.Value = DateTime.Now;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DatabaseService.InsertPersons(Datas.connectionString);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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
