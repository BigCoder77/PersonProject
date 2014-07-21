using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonProject.Model;
using Highway.Data.Contexts;

namespace PersonProject.Tests
{
    [TestClass]
    public class PersonRepositoryEffortTest
    {
        [TestMethod]
        public void RepositoryOpenNullDBEffortTest()
        {
            // arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();

            // act
            var context = new PeopleContext(null, connection, true);

            // assert
            Assert.AreNotEqual(null, context);
        }

        [TestMethod]
        public void RepositoryCreateRepositoryWithNullDBEffortTest()
        {
            // arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new PeopleContext(null, connection, true);

            // act
            var repository = new PersonRepository(context);

            // assert
            Assert.AreNotEqual(null, repository);
        }

        [TestMethod]
        public void RepositoryCreateAddsPersonEffortTest()
        {
            // arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new PeopleContext(null, connection, true);
            var repository = new PersonRepository(context);
            var person = new Person(0, "Hubert", "Mayer", DateTime.Now.Date);

            // act
            repository.Create(person);

            // assert
            Assert.AreEqual(1, context.People.ToList().Count);
        }
    }
}
