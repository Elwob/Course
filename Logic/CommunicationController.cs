using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class CommunicationController : MainController
    {
        /// <summary>
        /// Gets the Communication from one person concerning one course
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classId"></param>
        /// <param name="participantId"></param>
        /// <returns>List<Communication></Communication></returns>
        public List<Communication> GetCommunicationsNeeded<T>(int classId, int participantId)
        {
            var person = entities.Persons.FirstOrDefault(c => c.Id == participantId);
            if (person != null)
            {
                List<Communication> communicationsFromCourse = entities.RelCommunicationClasses
                                                                .Where(x => x.ClassId == classId && x.Class == typeof(T).Name)
                                                                .Include(c => c.Communication).ThenInclude(c => c.Document)
                                                                .Select(c => c.Communication).ToList();

                List<Communication> communicationsConcerningOneParticipant = communicationsFromCourse.Where(x => x.PersonId == participantId).ToList();
                return communicationsConcerningOneParticipant;
            }

            return null;
        }

        /// <summary>
        /// calls Method to check Ids and Method to create Relations, then changes will be saved on database
        /// </summary>
        /// <param name="communication"></param>
        /// <returns>Communication</returns>
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

        /// <summary>
        /// creates a communication, for example after sending mails to one or more Participants
        /// </summary>
        /// <param name="document"></param>
        /// <param name="template"></param>
        /// <param name="date"></param>
        /// <param name="reminderId"></param>
        /// <returns>Communication</returns>
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

        /// <summary>
        /// Deletes a Communication and its Relations to Classes, a Document which maybe is connected will not be deleted.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public string DeleteById(int id)
        {
            ///deletes the Relations from Communication to Classes
            List<RelCommunicationClass> relationList = entities.RelCommunicationClasses.Where(x => x.CommunicationId == id).ToList();
            foreach (var item in relationList)
            {
                entities.RelCommunicationClasses.Remove(item);
            }

            Communication communicationToDelete = entities.Communications.SingleOrDefault(x => x.Id == id);

            if (communicationToDelete == null)
            {
                return "The Communication you want to delete could not be found.";
            }
            else
            {
                entities.Communications.Remove(communicationToDelete);
                entities.SaveChanges();
                Communication communication = entities.Communications.SingleOrDefault(x => x.Id == id);
                if (communication == null)
                {
                    return "Communication has been successfully deleted.";
                }
                else
                {
                    return "Communication could not be deleted.";
                }
            }
        }

        /// <summary>
        /// changes the comment in a communication and saves changes on database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="communication"></param>
        /// <returns>Communication</returns>
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
            var trainer = entities.Persons.FirstOrDefault(c => c.Id == communication.TrainerId);
            var course = entities.Courses.FirstOrDefault(c => c.Id == communication.CourseId);
            if (person == null)
            {
                ///because a communication must always be assigned to a person, we return null
                return null;
            }
            else if (trainer == null && course == null)
            {
                ///in this case we return null, because otherwise we get no entry in RelCommunicationClass
                return null;
            }
            else if (course == null && trainer != null)
            {
                communication.CourseId = null;
                return communication;
            }
            else if (trainer == null && course != null)
            {
                communication.TrainerId = null;
                return communication;
            }
            else return communication;
        }
    }
}