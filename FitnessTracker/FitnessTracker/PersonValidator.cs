using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTracker
{
    public static class PersonValidator
    {
        public static string uresmezo = "Az összes mező kitöltése köelező!";
        public static string hibasidotartam = "Az időtartam csak pozitív egész szám lehet!";

        public static bool ValidatePerson(Person person)
        {

            if (string.IsNullOrWhiteSpace(person.Sportag) || string.IsNullOrWhiteSpace(person.Idotartam) || string.IsNullOrWhiteSpace(person.Helyszin))
            {
                MessageBox.Show(uresmezo, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(person.Idotartam, out int idotartamjo) || idotartamjo <= 0)
            {
                MessageBox.Show(hibasidotartam, "HIBA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
