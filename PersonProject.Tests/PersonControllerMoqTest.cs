using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonProject.Model;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace PersonProject.Tests
{
    [TestClass]
    public class PersonControllerMoqTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ControllerDeleteNotExistingEntryMoqTest()
        {
            // arrange
            var controller = CreateMoqTestController();

            // act
            controller.Delete(2);

            // assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ControllerUpdateNotExistingEntryMoqTest()
        {
            // arrange
            var controller = CreateMoqTestController(); 
            var person = new Person(27, "Klaus", "Müller", DateTime.Now.Date);

            // act
            controller.Edit(person.Id, person);

            // assert
            Assert.Fail();
        }

        private static PersonTestController CreateMoqTestController()
        {
            var mockSet     = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PeopleContext>();

            return CreateMoqTestController(mockSet, mockContext);
        }

        private static PersonTestController CreateMoqTestController(Mock<DbSet<Person>> mockSet, Mock<PeopleContext> mockContext)
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

            return new PersonTestController(repository);
        }
    }
}
