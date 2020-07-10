using Data.Models;
using Data.Models.JSONModels;

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