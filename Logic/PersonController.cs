using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    class PersonController: MainController
    {
   
        public Person FindOne(int id)
        {
            
            return entities.Persons.FirstOrDefault(x => x.Id == id);
        }

    }
}
