using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTracker
{
    public static class CsvService
    {
        public static bool Write(string path, Person person)
        {
            List<Person> Persons = new List<Person> { person };
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = !File.Exists(path)
                };

                using (var sw = new StreamWriter(path, true))
                using (var writer = new CsvWriter(sw, config))
                {
                    writer.WriteRecords(Persons);
                    MessageBox.Show("Sikeres adatbeírás a fileba!", "SIKER!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a file írása közben: " + ex.Message, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
