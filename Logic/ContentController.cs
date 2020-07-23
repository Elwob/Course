using Data.Models;
using Logic.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// contains logic for handling Contents
    /// </summary>
    public class ContentController : MainController
    {
        /// <summary>
        /// returns all Contents from DB
        /// </summary>
        /// <returns></returns>
        public List<Content> GetAllContents()
        {
            var content = entities.Contents.ToList();
            return content;
        }

        /// <summary>
        /// inserts a Content in DB and returns created Content (including auto-generated id)
        /// </summary>
        /// <param name="recContent"></param>
        /// <returns></returns>
        public Content PostContent(Content recContent)
        {
            entities.Contents.Add(recContent);
            entities.SaveChanges();
            return recContent;
        }

        /// <summary>
        /// updates a Content in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        public Content PutContent(int id, Content content)
        {
            var putContent = entities.Contents.Where(x => x.Id == id).FirstOrDefault();
            if (putContent != null)
            {
                putContent.Topic = content.Topic;
                putContent.Description = content.Description;
                putContent.UnitEstimation = content.UnitEstimation;
                entities.SaveChanges();
                return entities.Contents.Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                throw new EntryCouldNotBeFoundException("Could not find content in database");
            }
        }

        /// <summary>
        /// deletes a Content in DB
        /// </summary>
        /// <param name="id"></param>
        public void DeleteContent(int id)
        {
            if (entities.Contents.FirstOrDefault(x => x.Id == id) != null)
            {
                entities.Contents.Remove(entities.Contents.Single(x => x.Id == id));
                entities.SaveChanges();
            }
            else
            {
                throw new EntryCouldNotBeFoundException("Could not find content in database");
            }
        }
    }
}