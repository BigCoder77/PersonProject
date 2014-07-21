using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonProject.Model;

namespace PersonProject.Tests
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void PersonConstructorSetsIdTest()
        {
            // arrange + act
            var person = new Person(27, "Hubert", "Mayer", DateTime.Now.Date);

            // assert
            Assert.AreEqual(27, person.Id);
        }

        [TestMethod]
        public void PersonConstructorSetsFirstNameTest()
        {
            // arrange + act
            var person = new Person(0, "Hubert", "Mayer", DateTime.Now.Date);

            // assert
            Assert.AreEqual("Hubert", person.FirstName);
        }

        [TestMethod]
        public void PersonConstructorSetsLastNameTest()
        {
            // arrange + act
            var person = new Person(0, "Hubert", "Mayer", DateTime.Now.Date);

            // assert
            Assert.AreEqual("Mayer", person.LastName);
        }

        [TestMethod]
        public void PersonConstructorSetsBirthDateTest()
        {
            // arrange
            var birthDate = DateTime.Now.Date;
            birthDate.AddYears(-19);

            // act
            var person = new Person(0, "Hubert", "Mayer", birthDate);

            // assert
            Assert.IsTrue(person.BirthDate.Equals(birthDate));
        }

        [TestMethod]
        public void PersonSetIdTest()
        {
            // arrange
            var person = new Person();

            // act
            person.Id = 27;

            // assert
            Assert.AreEqual(27, person.Id);
        }


        [TestMethod]
        public void PersonSetFirstNameTest()
        {
            // arrange
            var person = new Person();

            // act
            person.FirstName = "Hubert";

            // assert
            Assert.AreEqual("Hubert", person.FirstName);
        }

        [TestMethod]
        public void PersonSetLastNameTest()
        {
            // arrange
            var person = new Person();

            // act
            person.LastName = "Mayer";

            // assert
            Assert.AreEqual("Mayer", person.LastName);
        }

        [TestMethod]
        public void PersonSetBirthDateTest()
        {
            // arrange
            var person = new Person();

            var birthDate = DateTime.Now.Date;

            birthDate.AddYears(-19);

            // act
            person.BirthDate = birthDate;

            // assert
            Assert.IsTrue(person.BirthDate.Equals(birthDate));
        }
    }
}
