using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CRUD.Repository.Interfaces;
using CRUD.Repository.Model;
using CRUD.Repository.Repositories;

namespace CRUD.Repositories
{
    public class EfPersonRepository : IPersonRepository
    {
        private readonly CrudDbContext db;

        public EfPersonRepository(CrudDbContext db)
        {
            this.db = db;
        }
        public List<Person> AllPersons()
        {
            var persons = db.Persons.ToList();
            return persons;
        }

        public IEnumerable<Person> FindPerson(string searchValue)
        {
            searchValue ??= String.Empty;

            var persons = db.Persons.Where(x => x.Firstname.Contains(searchValue)).ToList();
            return persons;
        }

        public Person GetPersonById(int id)
        {
            return db.Persons.SingleOrDefault(x => x.Id == id);
        }

        public Person Edit(Person updatedPerson)
        {
            db.Persons.Update(updatedPerson);
            db.SaveChanges();
            return updatedPerson;
        }

        public Person Add(Person newPerson)
        {
            db.Persons.Add(newPerson);
            db.SaveChanges();
            return newPerson;
        }

        public bool Delete(int id)
        {
            var person = GetPersonById(id);
            if (person == null)
            {
                return false;
            }
            db.Persons.Remove(person);
            db.SaveChanges();
            return true;
        }
    }

}
