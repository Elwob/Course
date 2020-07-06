using Data.Models;
using Data.Models.ReceiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ContentController : MainController
    {
        public List<Content> GetAllContents()
        {
            var content = entities.Contents.ToList();
            // use following line if you want to return relations to courses (where a content is teached in) as well:
            // var content = entities.Contents.Include(c => c.CourseContents).ThenInclude(x => x.Course).ToList();
            return content;
        }

        public Content PostContent(ReceivedContent recContent)
        {
            entities.Contents.Add(new Content(recContent.Topic, recContent.Description, recContent.UnitEstimation));
            entities.SaveChanges();
            return entities.Contents.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
