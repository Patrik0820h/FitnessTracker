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
        public static string dictionaryPath = "C:\\Users\\Patrik\\Documents\\GitHub\\FitnessTracker\\FitnessTracker\\FitnessTracker"; //Ezt változtatni kell folyton a megfelelőre gépenként!!!
        public static string filename = "Adatok.csv";
        public static string filePath = Path.Combine(dictionaryPath, filename);

        public class Person
        {
            public string Sportag { get; set; }
            public DateTime Datum { get; set; }
            public int Idotartam { get; set; }
            public string Helyszin { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uresmezo = "Az összes mező kitöltése köelező!";
            string hibasidotartam = "Az időtartam csak pozitív egész szám lehet!";
            string hibaspath = $"A beállított mappa nem létezik: {dictionaryPath}\nKérlek, állítsd be a helyes elérési utat a programban.";
            string sikeres = "Sikeres adatbeírás a csv fileba!";
            string sportag = textBox3.Text;
            DateTime datum = dateTimePicker1.Value;
            string idotartam = textBox4.Text;
            string helyszin = textBox1.Text;

            if (!Directory.Exists(dictionaryPath))
            {
                MessageBox.Show(hibaspath,"HIBA!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            if (string.IsNullOrWhiteSpace(sportag) || string.IsNullOrWhiteSpace(idotartam) || string.IsNullOrWhiteSpace(helyszin))
            {
                MessageBox.Show(uresmezo,"HIBA!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(idotartam, out int idotartamjo) || idotartamjo <= 0)
            {
                MessageBox.Show(hibasidotartam,"HIBA!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            List<Person> textboxok = new List<Person>
            {
                new Person { Sportag = sportag, Datum = datum, Idotartam = idotartamjo, Helyszin = helyszin }
            };
            try
            {
                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath)) { }
                    var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true
                    };
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    using (CsvWriter csvWriter = new CsvWriter(sw, configPersons))
                    {
                        csvWriter.WriteRecords(textboxok);
                    }
                    MessageBox.Show(sikeres, "Siker!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    dateTimePicker1.Value= DateTime.Now;
                    textBox3.Clear();
                    textBox4.Clear();
                    return;
                }
                else
                {
                    var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false
                    };
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    using (CsvWriter csvWriter = new CsvWriter(sw, configPersons))
                    {
                        csvWriter.WriteRecords(textboxok);
                    }
                    MessageBox.Show(sikeres, "Siker!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    dateTimePicker1.Value = DateTime.Now;
                    textBox3.Clear();
                    textBox4.Clear();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a fájl írása közben: " + ex.Message);
                MessageBox.Show("Hiba történt a fájl írása közben: " + ex.Message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Nincs megfelelő adat a exportáláshoz!";
            string sikeres = "Sikeresen feltöltötted az adatokat az adatbázisba!";

            if (!File.Exists(filePath))
            {
                MessageBox.Show(message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=localhost;Database=adatok;Uid=root";
            // A connection stringet is helyesen bekell allitani!!!

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Sikeresen kapcsolódtál az adatbázishoz!");

                    string createtable = "CREATE TABLE IF NOT EXISTS Adatok (Sportag VARCHAR(50), Datum DATE, Idotartam INT, Helyszin VARCHAR(50))";
                    MySqlCommand cmd = new MySqlCommand(createtable, conn);
                    cmd.ExecuteNonQuery();


                    string insert = "INSERT INTO Adatok (Sportag, Datum, Idotartam, Helyszin) VALUES (@Sportag, @Datum, @Idotartam, @Helyszin)";

                    try
                    {
                        using (var sr = new StreamReader(filePath))
                        {
                            string elsosor = sr.ReadLine();
                            while (!sr.EndOfStream)
                            {
                                string sor = sr.ReadLine();
                                string[] adatok = sor.Split(',');
                                if (adatok.Length != 4)
                                    continue;
                                if (!DateTime.TryParse(adatok[1], out DateTime datum))
                                    continue;

                                if (!int.TryParse(adatok[2], out int idotartamjo))
                                    continue;
                                using (MySqlCommand cmd2 = new MySqlCommand(insert, conn))
                                {
                                    cmd2.Parameters.AddWithValue("@Sportag", adatok[0]);
                                    cmd2.Parameters.AddWithValue("@Datum", datum);
                                    cmd2.Parameters.AddWithValue("@Idotartam", idotartamjo);
                                    cmd2.Parameters.AddWithValue("@Helyszin", adatok[3]);

                                    cmd2.ExecuteNonQuery();
                                }

                            }
                        }
                        try
                        {
                            File.Delete(filePath);
                            MessageBox.Show(sikeres, "SIKER!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Az adatok sikeresen feltöltődtek, de a CSV fájl törlése sikertelen: {ex.Message}", "FIGYELMEZTETÉS!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Hiba történt a CSV olvasásakor: " + ex.Message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt az adatbázis műveletek során: " + ex.Message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
