using Data.Extensions;
using Data.Models;
using Data.Models.BaseClasses;
using Data.Models.Enums;
using Logic.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Logic
{
    public class PersonController : MainController
    {
        /// <summary>
        /// returns a specific person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person FindOne(int id)
        {
            var x = entities.Persons.Include(x => x.Contacts).FirstOrDefault(x => x.Id == id);
            if (x != null)
            {
                return x;
            }
            else throw new EntryCouldNotBeFoundException("The Person could not be found.");
            
        }

        /// <summary>
        /// returns all trainers
        /// </summary>
        /// <returns></returns>
        public List<Person> FindAllTrainers()
        {        
            List<Person> trainers = entities.Persons.ToList().Where(x => x.Function.Equals(EFunction.Trainer_Intern) || x.Function.Equals(EFunction.Trainer_Extern)).ToList();
            if (trainers.Count > 0)
            {
                return trainers;
            }
            else throw new EntryCouldNotBeFoundException("No Trainers found.");
             
        }
        /// <summary>
        /// returns a list of all persons
        /// </summary>
        /// <returns></returns>
        public List<Person> FindAll()
        {
            List<Person> allPersons = entities.Persons.ToList();
            if (allPersons.Count > 0)
            {
                return allPersons;
            }
            else throw new EntryCouldNotBeFoundException("No Persons could be found.");
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

            if (participants.Count > 0)
            {
                participants = FindPersonalizedMaterial<Notebook>(participants);
                participants = FindPersonalizedMaterial<Equipment>(participants);
                return participants;
            }
            else throw new EntryCouldNotBeFoundException("Could not find any Participants.");
            
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