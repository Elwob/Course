using Data.Models;
using System.Linq;

namespace Logic
{
    internal class PersonController : MainController
    {
        public Person FindOne(int id)
        {
            return entities.Persons.FirstOrDefault(x => x.Id == id);
        }
    }
}