using System;
using NUnit.Framework;
using PersonProject.Model;
using TechTalk.SpecFlow;

namespace PersonProject.Tests
{
    [Binding]
    public class PersonStepDefinitions
    {
        private Person _person;
        private bool _isArgumentNullExceptionForFirstNameThrown;


        [Given(@"I have created a person with FirstName (.*) and LastName (.*)")]
        public void GivenIHaveCreatedAPersonWithFirstNameAndLastName(string firstName, string lastName)
        {
            _person = new Person(0,firstName, lastName, DateTime.Now);
        }

        [Given(@"I have created a person without firstName and LastName (.*)")]
        public void GivenIHaveCreatedAPersonWithoutFirstNameAndLastName(string lastName)
        {
            _isArgumentNullExceptionForFirstNameThrown = false;
            try
            {
                _person = new Person(0, null, lastName, DateTime.Now);
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName == "firstName")
                _isArgumentNullExceptionForFirstNameThrown = true;
            }
        }

        [Given(@"I have created a person with empty firstName and LastName (.*)")]
        public void GivenIHaveCreatedAPersonWithEmptyFirstNameAndLastName(string lastName)
        {
            _isArgumentNullExceptionForFirstNameThrown = false;
            try
            {
                _person = new Person(0, string.Empty, lastName, DateTime.Now);
            }
            catch (ArgumentNullException e)
            {
                if (e.ParamName == "firstName")
                    _isArgumentNullExceptionForFirstNameThrown = true;
            }
        }

        [Then(@"the FirstName must be (.*)")]
        public void ThenTheFirstNameMustBe(string firstName)
        {
            Assert.AreEqual(firstName, _person.FirstName);
        }

        [Then(@"the LastName must be (.*)")]
        public void ThenTheLastNameMustBe(string lastName)
        {
            Assert.AreEqual(lastName, _person.LastName);
        }

        [Then(@"an ArgumentNullException must be thrown")]
        public void ThenAnArgumentNullExceptionMustBeThrown()
        {
            Assert.AreEqual(true, _isArgumentNullExceptionForFirstNameThrown);
        }
    }
}
