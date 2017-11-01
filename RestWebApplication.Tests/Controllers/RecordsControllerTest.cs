using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestWebApplication;
using RestWebApplication.Controllers;
using CodingAssignment;

namespace RestWebApplication.Tests.Controllers
{
    [TestClass]
    public class RecordsControllerTest
    {
        private RecordDatabase db = RecordDatabase.Instance;
        private RecordGenerator gen = new RecordGenerator();
        RecordsController controller = new RecordsController();

        [TestMethod]
        public void Post()
        {
            // Arrange
            var recordListBeforeAddition = db.Get();

            // Act
            Record nextRecord = gen.GenerateRandomRecord();
            controller.Post(nextRecord.ToString());
            var recordListAfterAddition = db.Get();

            // Assert
            Assert.IsTrue(recordListAfterAddition.Last() == nextRecord);
        }

        [TestMethod]
        public void SortByGender()
        {
            // Arrange
            var originalList = db.Get();
            var sortedList = originalList.OrderBy(r => r.Gender).ThenBy(r => r.LastName).ToList();

            // Act
            var testList = controller.SortByGender().ToList();
            var passTest = true;
            for (int i = 0; i < testList.Count() - 1 && passTest; i++)
            {
                passTest &= (testList[i].Gender.CompareTo( testList[i + 1].Gender)) <= 0;
            }

            // Assert
            Assert.IsTrue(passTest);
        }

        [TestMethod]
        public void SortByBirthdate()
        {
            // Arrange
            var originalList = db.Get();
            var sortedList = originalList.OrderBy(r => r.DateOfBirth).ToList();

            // Act
            var testList = controller.SortByBirthdate().ToList();
            var passTest = true;
            for (int i = 0; i < testList.Count() - 1 && passTest; i++)
            {
                passTest &= (testList[i].DateOfBirth.CompareTo(testList[i + 1].DateOfBirth)) <= 0;
            }

            // Assert
            Assert.IsTrue(passTest);
        }

        [TestMethod]
        public void SortByName()
        {
            // Arrange
            var originalList = db.Get();
            var sortedList = originalList.OrderByDescending(r => r.LastName).ToList();

            // Act
            var testList = controller.SortByName().ToList();
            var passTest = true;
            for (int i = 0; i < testList.Count() - 1 && passTest; i++)
            {
                passTest &= (testList[i].LastName.CompareTo(testList[i + 1].LastName)) >= 0;
            }

            // Assert
            Assert.IsTrue(passTest);
        }
    }
}
