﻿using Data.Models;
using Microsoft.EntityFrameworkCore;
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
        public List<Person> FindAllParticipantsOfOneCourse(int id)
        {

            List<RelCourseParticipant> relParticipantsList = entities.RelCourseParticipants.Include(x => x.Person).ThenInclude(x => x.Comments).
                Include(x => x.Person).ThenInclude(x => x.Contacts).
                Include(x => x.Person).ThenInclude(x => x.Absences).Where(x => x.CourseId == id).ToList();

            List<Person> participants = relParticipantsList.Select(x => x.Person).ToList();

            return participants;
               
        }
    }
}

