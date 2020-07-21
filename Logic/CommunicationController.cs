using Data.Models;
using Logic.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            else
            {
                throw new EntryCouldNotBeFoundException("Could not get any information about this Person from Database.");
            }         
        }

        /// <summary>
        /// calls Method to check Ids and Method to create Relations, then changes will be saved on database
        /// </summary>
        /// <param name="communication"></param>
        /// <returns>Communication</returns>
        public Communication CreateRelationAndAddToDatabase(Communication communication)
        {
            CheckIfIdToConnectWithExists(communication);
   
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
                throw new EntryCouldNotBeFoundException("The Communication you want to delete could not be found.");
            }
            else
            {
                entities.Communications.Remove(communicationToDelete);
                entities.SaveChanges();             
                return "Communication has been successfully deleted.";             
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
                throw new EntryCouldNotBeFoundException("The Communication you want to change could not be found.");
            }
        }

        /// <summary>
        /// prevents wrong entries, because of not existing CourseId or PersonId
        /// </summary>
        /// <param name="communication"></param>
        /// <returns>communication</returns>
        public void CheckIfIdToConnectWithExists(Communication communication)
        {
            Person trainer = null;
            Course course = null;
            var person = entities.Persons.FirstOrDefault(c => c.Id == communication.PersonId);
            if (person == null)
            {
                throw new EntryCouldNotBeFoundException("The Person whose communicaton you want to map could not be found.");
            }
            if (communication.TrainerId == null && communication.CourseId == null)
            {
                throw new MissingInputException("You have to enter either Trainer or Course or both.");
            }
            if (communication.TrainerId != null)
            {
                trainer = entities.Persons.FirstOrDefault(c => c.Id == communication.TrainerId);
                if (trainer == null)
                {                 
                    throw new EntryCouldNotBeFoundException("The Trainer you have entered could not be found.");
                }
            }
            if(communication.CourseId != null)
            {
                course = entities.Courses.FirstOrDefault(c => c.Id == communication.CourseId);
                if (course == null)
                {                   
                    throw new EntryCouldNotBeFoundException("The Course you have entered could not be found.");
                }
            }           
        }
    }
}