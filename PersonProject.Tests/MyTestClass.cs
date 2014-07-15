using System;
using NUnit.Framework;
using PersonProject.Model;

namespace PersonProject.Tests
{
    [TestFixture]
    class MyTestClass
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MustThrowExceptionWhenFirstNameIsNullOnObjectInitialization()
        {
            // arrange
            Step1();
            Step2();
            Step3();

            // act
            var person = new Person(0, null, "Hubert", DateTime.Now);

            // assert
            Assert.Fail();
        }
        [Test]
        public void MustSetFirstName()
        {
            // arrange

            // act
            var person = new Person(0, "Hubert", "Mayer", DateTime.Now);

            // assert
            Assert.AreEqual("Hubert", person.FirstName);
        }
        [Test]
        public void MustSetLastName()
        {
            // arrange

            // act
            var person = new Person(0, "Hubert", "Mayer", DateTime.Now);

            // assert
            Assert.AreEqual("Mayer", person.LastName);
        }

        private void Step1()
        {
        }

        private void Step2()
        {
        }

        private void Step3()
        {
        }
    }
}