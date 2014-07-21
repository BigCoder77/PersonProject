using PersonProject.Controllers;
using PersonProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonProject.Tests
{
    class PersonTestController : PersonController
    {
        PersonRepository Repository { get; set; }

        public PersonTestController(PersonRepository repository)
        {
            Repository = repository;
        }

        protected virtual PersonRepository NewRepository()
        {
            return Repository;
        }
    }
}
