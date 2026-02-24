using FitnessTracker;

namespace FitnerTrackerTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void PersonValidator_Test()
        {
            var person = new Person
            {
                Sportag = "",
                Datum = DateTime.Now,
                Idotartam = "10",
                Helyszin = "Park"
            };
            var result = PersonValidator.ValidatePerson(person);
            Assert.IsFalse(result);


            var person2 = new Person
            {
                Sportag = "Foci",
                Datum = DateTime.Now,
                Idotartam = "-10",
                Helyszin = "Park"
            };
            var result2 = PersonValidator.ValidatePerson(person2);
            Assert.IsFalse(result2);


            var person3 = new Person
            {
                Sportag = "Foci",
                Datum = DateTime.Now,
                Idotartam = "10",
                Helyszin = "Park"
            };
            var result3 = PersonValidator.ValidatePerson(person3);
            Assert.IsTrue(result3);
        }
        [TestMethod]
        public void CsvService_Test()
        {
            bool result;
            bool result2;

            var person = new Person
            {
                Sportag = "Foci",
                Datum = DateTime.Now,
                Idotartam = "10",
                Helyszin = "Park"
            };
            if (CsvService.Write(Datas.filePath, person))
            {
                result = CsvService.Write(Datas.filePath, person);
                Assert.IsTrue(result);
            }

            var person2 = new Person
            {
                Sportag = "",
                Datum = DateTime.Now,
                Idotartam = "10",
                Helyszin = "Park"
            };
            if (!CsvService.Write(Datas.filePath, person2))
            {
                result2 = CsvService.Write(Datas.filePath, person2);
                Assert.IsFalse(result2);
            }
        }
    }
}
