using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTracker
{
    public static class DatabaseService
    {
        public static bool InsertPersons(string connectionString) 
        {
            string message = "Nincs megfelelő adat a exportáláshoz!";
            string sikeres = "Sikeresen feltöltötted az adatokat az adatbázisba!";

            if (!File.Exists(Datas.filePath))
            {
                MessageBox.Show(message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

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
                        using (var sr = new StreamReader(Datas.filePath))
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
                            File.Delete(Datas.filePath);
                            MessageBox.Show(sikeres, "SIKER!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Az adatok sikeresen feltöltődtek, de a CSV fájl törlése sikertelen: {ex.Message}", "FIGYELMEZTETÉS!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Hiba történt a CSV olvasásakor: " + ex.Message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt az adatbázis műveletek során: " + ex.Message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
