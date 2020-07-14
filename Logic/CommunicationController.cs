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
            return null;
        }
    }
}