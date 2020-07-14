using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class PersonController : MainController
    {
        public Person FindOne(int id)
        {
            var x = entities.Persons.Include(x => x.Contacts).FirstOrDefault(x => x.Id == id);
            return x;
        }

        public List<Person> FindAllTrainers()
        {
            return entities.Persons.Where(x => x.Function == "0" || x.Function == "1").ToList();
        }

        public List<Person> FindAll()
        {
            return entities.Persons.ToList();
        }

        //public void GetPerson()
        //{
        //    entities.Persons.Include(x => x.Contacts);
        //}
    }
}