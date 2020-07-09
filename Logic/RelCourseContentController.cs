using Data.Models;
using Data.Models.JSONModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class RelCourseContentController : MainController
    {
        public void CreateRelation(int courseId, JSONContent content)
        {
            entities.RelCourseContents.Add(new RelCourseContent() { CourseId = courseId, ContentId = content.Id, UnitEstimation = content.Units });
            entities.SaveChanges();
        }
    }
}
