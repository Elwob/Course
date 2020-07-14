using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class CommunicationController : MainController
    {
        public List<Communication> GetCommunicationsNeeded(int id, EClass className)
        {
            List<Communication> communications = entities.RelCommunicationClasses.Where(x => x.ClassId == id && x.Class == className.ToString()).
                                                    Select(c => c.Communication).ToList();

            return communications;
        }

        public Communication CreateRelationAndAddToDatabase(Communication communication)
        {
            communication = CheckIfIdToConnectWithExists(communication);
            if (communication == null)
            {
                return null;
            }
            communication.CreatedAt = DateTime.Now;
            communication.ModifiedAt = DateTime.Now;

            entities.Communications.Add(communication);
            communication.CreateRelation();
            entities.SaveChanges();
            return communication;
        }

        public Communication CreateCommunication(Document document, EmailTemplate template, DateTime date, int? reminderId)

        {
            Communication communication = new Communication();
            communication.Channel = EChannel.Email;
            communication.PersonId = (int)document.PersonId;
            communication.Date = date;
            communication.Comment = template.Comment;
            communication.CourseId = template.CourseId;
            communication.TrainerId = template.TrainerId;
            communication.DocumentId = document.Id;
            communication.ReminderId = reminderId;
            communication = CreateRelationAndAddToDatabase(communication);
            return communication;
        }

        public string DeleteById(int id)
        {
            ///deletes the Relations from Communication to Classes
            List<RelCommunicationClass> relationList = entities.RelCommunicationClasses.Where(x => x.CommunicationId == id).ToList();
            foreach (var item in relationList)
            {
                entities.RelCommunicationClasses.Remove(item);
            }

            Communication communicationToDelete = entities.Communications.SingleOrDefault(x => x.Id == id);
            
            if(communicationToDelete == null)
            {
                return "The Communication you want to delete could not be found.";
            }
            else
            {
                entities.Communications.Remove(communicationToDelete);
                entities.SaveChanges();
                Communication communication = entities.Communications.SingleOrDefault(x => x.Id == id);
                if(communication == null)
                {
                    return "Communication has been successfully deleted.";
                }
                else
                {
                    return "Communication could not be deleted.";
                }
                
            }
        }

        public Communication ChangeCommunication(int id, Communication communication)
        {
            var communicationToChange = entities.Communications.Where(x => x.Id == id).FirstOrDefault();
            if (communicationToChange != null)
            {
                communicationToChange.Comment = communication.Comment;
                communicationToChange.ModifiedAt = DateTime.Now;
                entities.SaveChanges();
                return entities.Communications.Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return null;
            }
           
        }
        /// <summary>
        /// prevents wrong entries, because of not existing CourseId or PersonId
        /// </summary>
        /// <param name="communication"></param>
        /// <returns>communication</returns>
        public Communication CheckIfIdToConnectWithExists(Communication communication)
        {
            var person = entities.Persons.FirstOrDefault(c => c.Id == communication.PersonId);
            var course = entities.Courses.FirstOrDefault(c => c.Id == communication.CourseId);
            if (person == null && course == null)
            {
                return null;
            }
            else if (person == null && course != null)
            {
                ///because a communication must always be assigned to a person, we return null although course is not null
                return null;
                
            }
            else if (course == null && person != null)
            {
                communication.CourseId = null;
                return communication;
            }
            else return communication;
        }
    }
}