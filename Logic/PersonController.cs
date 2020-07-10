using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class PersonController : MainController
    {
        public Person FindOne(int id)
        {
            return entities.Persons.FirstOrDefault(x => x.Id == id);
        }

        public List<Person> FindAllTrainers()
        {
            return entities.Persons.Where(x => x.Function == "0" || x.Function == "1").ToList();
        }

        public List<Person> FindAll()
        {
            return entities.Persons.ToList();
        }
    }
}