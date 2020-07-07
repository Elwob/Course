using Data.Models;
using Data.Models.JSONModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ContentController : MainController
    {
        /// <summary>
        /// returns all Contents from DB
        /// </summary>
        /// <returns></returns>
        public List<Content> GetAllContents()
        {
            var content = entities.Contents.ToList();
            // use following line if you want to return relations to courses (where a content is teached in) as well:
            // var content = entities.Contents.Include(c => c.CourseContents).ThenInclude(x => x.Course).ToList();
            return content;
        }

        /// <summary>
        /// inserts a Content in DB and returns created Content (including auto-generated id)
        /// </summary>
        /// <param name="recContent"></param>
        /// <returns></returns>
        public Content PostContent(JSONContent recContent)
        {
            entities.Contents.Add(new Content(recContent.Topic, recContent.Description, recContent.UnitEstimation));
            entities.SaveChanges();
            return entities.Contents.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        /// <summary>
        /// updates a content in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        public void PutContent(int id, JSONContent content)
        {
            var putContent = entities.Contents.Where(x => x.Id == id).FirstOrDefault();
            putContent.Topic = content.Topic;
            putContent.Description = content.Description;
            putContent.UnitEstimation = content.UnitEstimation;
            entities.SaveChanges();
        }

        /// <summary>
        /// deletes a content in DB
        /// </summary>
        /// <param name="id"></param>
        public void DeleteContent(int id)
        {
            entities.Contents.Remove(entities.Contents.Single(x => x.Id == id));
            entities.SaveChanges();
        }
    }
}
