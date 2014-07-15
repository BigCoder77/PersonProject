using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonProject.Model;

namespace PersonProject.Models
{
    public class PersonRepository : IDisposable
    {
        private readonly PeopleContext _context;

        public PersonRepository(PeopleContext context)
        {
            _context = context;
        }

        public PersonRepository()
        {
            _context = new PeopleContext();
        }

        public IEnumerable<Person> All()
        {
            return _context.People.ToList();
        }

        public Person GetById(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.Id == id);
            return person ?? new Person(id, string.Empty, string.Empty, DateTime.Now);
        }

        public void Update(Person person)
        {
            var currentPerson = _context.People.SingleOrDefault(p => p.Id == person.Id);

            if (currentPerson == null)
            {
                throw new InvalidOperationException(string.Format("Person with ID '{0}' was not found.", person.Id));
            }

            currentPerson.FirstName = person.FirstName;
            currentPerson.LastName = person.LastName;
            currentPerson.BirthDate = person.BirthDate;
            _context.SaveChanges();
        }

        public void Create(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.Id == id);

            if (person != null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}