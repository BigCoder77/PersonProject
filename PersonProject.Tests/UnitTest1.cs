using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonProject.Model;

namespace PersonProject.Tests
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void SetFirstName()
        {
            // arrange
            var person = new Person();

            // act
            person.FirstName = "Hubert";

            // assert
            Assert.AreEqual(person.FirstName, "Hubert");
        }

        [TestMethod]
        public void SetLastName()
        {
            // arrange
            var person = new Person();

            // act
            person.LastName = "Mayer";

            // assert
            Assert.AreEqual(person.LastName, "Mayer");
        }

        [TestMethod]
        public void SetBirthDate()
        {
            // arrange
            var person = new Person();
            var birthDate = new DateTime();

            birthDate = DateTime.Now.Date;
            birthDate.AddYears(-19);

            // act
            person.BirthDate = birthDate;

            // assert
            Assert.IsTrue(person.BirthDate.Equals(birthDate));
        }
    }
}
