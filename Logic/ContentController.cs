using Data.Models;
using System.Collections.Generic;
using System.Linq;

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
            //var content = entities.Contents.Include(c => c.ContentCourse).ThenInclude(x => x.Course).ToList();
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
            putContent.Topic = content.Topic;
            putContent.Description = content.Description;
            putContent.UnitEstimation = content.UnitEstimation;
            entities.SaveChanges();
            return entities.Contents.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// deletes a Content in DB
        /// </summary>
        /// <param name="id"></param>
        public void DeleteContent(int id)
        {
            entities.Contents.Remove(entities.Contents.Single(x => x.Id == id));
            entities.SaveChanges();
        }
    }
}