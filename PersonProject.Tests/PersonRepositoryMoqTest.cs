using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonProject.Model;
using System.Data.Entity;
using Moq;

namespace PersonProject.Tests
{
    [TestClass]
    public class PersonRepositoryMoqTest
    {
        [TestMethod]
        public void RepositoryCreateMoqTest()
        {
            // arrange
            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PeopleContext>();

            mockContext.Setup(m => m.People).Returns(mockSet.Object);

            var repository = new PersonRepository(mockContext.Object);

            var person = new Person(1, "Hubert", "Mayer", DateTime.Now);

            // act
            repository.Create(person);

            // assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void RepositoryDeleteExistingEntryMoqTest()
        {
            // arrange
            var mockSet     = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PeopleContext>();

            var repository = CreateMoqTestRepository(mockSet, mockContext);

            // act
            repository.Delete(0);

            // assert
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RepositoryDeleteNotExistingEntryMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            repository.Delete(2);

            // assert
            Assert.Fail();
        }

        [TestMethod]
        public void RepositoryUpdateExistingEntryMoqTest()
        {
            // arrange
            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PeopleContext>();

            var repository = CreateMoqTestRepository(mockSet, mockContext);
            var person = new Person(0, "Klaus", "Müller", DateTime.Now.Date);

            // act
            repository.Update(person);

            // assert
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RepositoryUpdateNotExistingEntryMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();
            var person = new Person(27, "Klaus", "Müller", DateTime.Now.Date);

            // act
            repository.Update(person);

            // assert
            Assert.Fail();
        }

        [TestMethod]
        public void RepositoryAllReturnsAllEntriesMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPeople = repository.All().ToList();

            // assert
            Assert.AreEqual(2, testPeople.Count);
        }

        [TestMethod]
        public void RepositoryAllReturnsCorrectIdsMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPeople = repository.All().ToList();

            // assert
            Assert.AreEqual(0, testPeople[0].Id);
            Assert.AreEqual(1, testPeople[1].Id);
        }

        [TestMethod]
        public void RepositoryAllReturnsCorrectFirstNamesMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPeople = repository.All().ToList();

            // assert
            Assert.AreEqual("Hubert", testPeople[0].FirstName);
            Assert.AreEqual("Hans",   testPeople[1].FirstName);
        }

        [TestMethod]
        public void RepositoryAllReturnsCorrectLastNamesMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPeople = repository.All().ToList();

            // assert
            Assert.AreEqual("Mayer",  testPeople[0].LastName);
            Assert.AreEqual("Müller", testPeople[1].LastName);
        }

        [TestMethod]
        public void RepositoryAllReturnsCorrectBirthDatesMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPeople = repository.All().ToList();

            // assert
            Assert.IsTrue(testPeople[0].BirthDate.Equals(DateTime.Now.Date));
            Assert.IsTrue(testPeople[1].BirthDate.Equals(DateTime.Now.Date));
        }

        [TestMethod]
        public void RepositoryGetByIdReturnsCorrectFirstNameMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPerson = repository.GetById(0);

            // assert
            Assert.AreEqual("Hubert", testPerson.FirstName);
        }

        [TestMethod]
        public void RepositoryGetByIdReturnsCorrectLastNameMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPerson = repository.GetById(0);

            // assert
            Assert.AreEqual("Mayer", testPerson.LastName);
        }

        [TestMethod]
        public void RepositoryGetByIdReturnsCorrectBirthDateMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPerson = repository.GetById(0);

            // assert
            Assert.IsTrue(testPerson.BirthDate.Equals(DateTime.Now.Date));
        }

        [TestMethod]
        public void RepositoryGetByIdReturnsNegativeIdWhenRequestIsInvalidMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPerson = repository.GetById(2);

            // assert
            Assert.AreEqual(-1, testPerson.Id);
        }

        [TestMethod]
        public void RepositoryGetByIdReturnsEmptyFirstNameWhenRequestIsInvalidMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPerson = repository.GetById(2);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(testPerson.FirstName));
        }

        [TestMethod]
        public void RepositoryGetByIdReturnsEmptyLastNameWhenRequestIsInvalidMoqTest()
        {
            // arrange
            var repository = CreateMoqTestRepository();

            // act
            var testPerson = repository.GetById(2);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(testPerson.LastName));
        }


        private static PersonRepository CreateMoqTestRepository()
        {
            var mockSet     = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PeopleContext>();

            return CreateMoqTestRepository(mockSet, mockContext);
        }

        private static PersonRepository CreateMoqTestRepository(Mock<DbSet<Person>> mockSet, Mock<PeopleContext> mockContext)
        {
            if (mockSet == null)
            {
                throw new ArgumentNullException("mockSet");
            }

            if (mockContext == null)
            {
                throw new ArgumentNullException("mockContext");
            }

            var data = new List<Person>
            {
                new Person(0, "Hubert", "Mayer",  DateTime.Now.Date),
                new Person(1, "Hans",   "Müller", DateTime.Now.Date)
            }.AsQueryable();

            mockSet = new Mock<DbSet<Person>>();

            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(m => m.People).Returns(mockSet.Object);

            var repository = new PersonRepository(mockContext.Object);

            return repository;
        }
    }
}
