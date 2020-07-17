using Data.Models;
using Data.Models.BaseClasses;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Org.BouncyCastle.Crypto.Tls;
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
            // TODO: change to enums
            return entities.Persons.Where(x => x.Function == "0" || x.Function == "1").ToList();
        }

        public List<Person> FindAll()
        {
            return entities.Persons.ToList();
        }
        /// <summary>
        /// Searches all Participants of one Course and calls Method FindPersonalizedMaterial
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Person> FindAllParticipantsOfOneCourse(int id)
        {

            List<RelCourseParticipant> relParticipantsList = entities.RelCourseParticipants
                                                            .Include(x => x.Person).ThenInclude(x => x.Comments)
                                                            .Include(x => x.Person).ThenInclude(x => x.Contacts)
                                                            .Include(x => x.Person).ThenInclude(x => x.Absences)
                                                            .Where(x => x.CourseId == id).ToList();

            List<Person> participants = relParticipantsList.Select(x => x.Person).ToList();

            if(participants.Count > 0)
            {
                participants = FindPersonalizedMaterial<Notebook>(participants);
                participants = FindPersonalizedMaterial<Equipment>(participants);
            }                   

            return participants;

        }
        /// <summary>
        /// searches for Material which is personalized and calls Method FillMaterialDictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="participants"></param>
        /// <returns></returns>
        public List<Person> FindPersonalizedMaterial<T>(List<Person> participants) where T : BaseClassMaterial
        {
            //from our entities we take all properties with the name of our class plus s, for example "Notebook" + "s",
            //because our entity property is called "Notebooks"
            var property = entities.GetType().GetProperty(typeof(T).Name + "s");

            if (property != null)
            {
                //from our property we take the Values, cast them into DbSet and return it as List()
                var materialList = (property.GetValue(entities, null) as DbSet<T>).ToList();

                foreach (Person person in participants)
                {
                   
                    //with x.GetType().GetProperty("PersonId")....we get access to the PersonId which otherwise would not be reachable
                    var materials = materialList.Where(x => (x.GetType().GetProperty("PersonId").GetValue(x, null) as int?) == person.Id).ToList();
                    if (materials != null)
                    {
                        foreach (var item in materials)
                        {                    
                            FillMaterialDictionary<T>(person, item);
                        }
                    }
                }
                return participants;
            }
            
            else return null;
        }
        /// <summary>
        /// fills right keys and values to each persons MaterialDictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="person"></param>
        /// <param name="item"></param>
        public void FillMaterialDictionary<T>(Person person, T item) where T : BaseClassMaterial
        {        
            if (!(typeof(T).Name == "Equipment"))
            {
                if (person.MaterialDict.ContainsKey(typeof(T).Name))
                {
                    person.MaterialDict[typeof(T).Name] = item;
                }
                else
                {
                    person.MaterialDict.Add(typeof(T).Name, item);

                }
            }
            else
            {
                if (person.MaterialDict.ContainsKey(item.GetType().GetProperty("Type").GetValue(item, null) as string))
                {
                    person.MaterialDict[item.GetType().GetProperty("Type").GetValue(item, null) as string] = item;
                }
                else
                {
                    person.MaterialDict.Add(item.GetType().GetProperty("Type").GetValue(item, null) as string, item);
                }
            }
        }
    }
}
