using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonProject.Model;

namespace PersonProject.Model
{
    public class PersonRepository : IDisposable
    {
        private readonly PeopleContext _context;

        public PersonRepository(PeopleContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

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
            return person ?? new Person(-1);
        }

        public void Update(Person person)
        {
            PrepareUpdate(person);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Person person)
        {
            PrepareUpdate(person);
            await _context.SaveChangesAsync();
        }

        private void PrepareUpdate(Person person)
        {
            var currentPerson = _context.People.SingleOrDefault(p => p.Id == person.Id);

            if (currentPerson == null)
            {
                throw new InvalidOperationException(string.Format("Person with ID '{0}' was not found.", person.Id));
            }

            currentPerson.FirstName = person.FirstName;
            currentPerson.LastName  = person.LastName;
            currentPerson.BirthDate = person.BirthDate;
        }


        public void Create(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.Id == id);

            if (person == null)
            {
                throw new InvalidOperationException(string.Format("Person with ID '{0}' was not found.", id));
            }

            _context.People.Remove(person);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.Id == id);

            if (person == null)
            {
                throw new InvalidOperationException(string.Format("Person with ID '{0}' was not found.", id));
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}