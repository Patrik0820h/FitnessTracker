using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker
{
    public class Datas
    {
        public static string dictionaryPath = "C:\\Users\\Patrik\\Documents\\GitHub\\FitnessTracker\\FitnessTracker\\FitnessTracker"; //Ezt változtatni kell folyton a megfelelőre gépenként!!!
        public static string filename = "Adatok.csv";
        public static string filePath = Path.Combine(dictionaryPath, filename);
        public static string connectionString = "Server=localhost;Database=adatok;Uid=root;Pwd=;"; // Ezt is változtatni kell a megfelelő adatbázis eléréséhez gépenként!!!
    }
}
