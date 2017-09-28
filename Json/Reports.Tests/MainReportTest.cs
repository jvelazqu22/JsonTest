using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reports.Tests
{
    [TestClass]
    public class MainReportTest
    {
        [TestMethod]
        public void GetRawData_FileExists_ReadData()
        {
            // Arrange 
            var inputPath = @"c:\temp\Json\ConsoleJob\Input\";
            List<Person> results = new List<Person>();

            // Act
            results = new MainReport().GetRawData(inputPath);

            // Assert
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void Sort_3Records_SortByAgeLNameAndFname()
        {
            // Arrange 
            List<Person> list = new List<Person>()
            {
                new Person() { age = 10, lastName = "Jones", firstName = "Joseph"},
                new Person() { age = 25, lastName = "Fox", firstName = "Michael" },
                new Person() { age = 50, lastName = "Jordan", firstName = "Michael"},
            };
            List<Person> results = new List<Person>();


            // Act
            results = new MainReport().Sort(list);

            // Assert
            Assert.AreEqual(results[0].age, 50);
            Assert.AreEqual(results[1].age, 25);
            Assert.AreEqual(results[2].age, 10);
        }

        [TestMethod]
        public void FormatData_3Records_FormatByAgeLNameFNameEyeColorAndGender()
        {
            // Arrange 
            List<Person> list = new List<Person>()
            {
                new Person() { age = 10, lastName = "Jones", firstName = "Joseph", eyeColor = "brown", gender = "male"},
                new Person() { age = 25, lastName = "Fox", firstName = "Michael", eyeColor = "blue", gender = "male"},
                new Person() { age = 50, lastName = "Jordan", firstName = "Michael", eyeColor = "green", gender = "male"},
            };
            List<string> results = new List<string>();
            List<string> firstRecordResults = new List<string>();

                // Act
            results = new MainReport().FormatData(list);
            firstRecordResults = results.First().Split('|').ToList();

            // Assert
            Assert.AreEqual(firstRecordResults[0].Trim(), "10");
            Assert.AreEqual(firstRecordResults[1].Trim(), "Jones");
            Assert.AreEqual(firstRecordResults[2].Trim(), "Joseph");
            Assert.AreEqual(firstRecordResults[3].Trim(), "brown");
            Assert.AreEqual(firstRecordResults[4].Trim(), "male");
        }

    }
}
