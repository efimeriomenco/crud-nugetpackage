using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.Repository.Model;

namespace CRUD.Repository.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> AllPersons();
        IEnumerable<Person> FindPerson(string searchValue);
        Person GetPersonById(int id);
        Person Edit(Person updatedPerson);
        Person Add(Person newPerson);
        bool Delete(int id);
    }
}
