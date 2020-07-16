using Data.Models.Relations;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Adds & Updates Classrooms in a Course
    /// </summary>
    internal class RelCourseClassroomController : MainController
    {
        public void CreateRelation(int courseId, int classroomId)
        {
            entities.RelCourseClassrooms.Add(new RelCourseClassroom() { CourseId = courseId, ClassroomId = classroomId });
            entities.SaveChanges();
        }

        public void UpdateRelations(int courseId, List<int> classroomIds)
        {
            // add not already existing relations
            var courseRels = entities.RelCourseClassrooms.Where(x => x.CourseId == courseId).ToList();
            foreach (var classroomId in classroomIds)
            {
                if (!courseRels.Any(x => x.ClassroomId == classroomId))
                {
                    entities.RelCourseClassrooms.Add(new RelCourseClassroom() { CourseId = courseId, ClassroomId = classroomId });
                    entities.SaveChanges();
                }
            }
            // delete relations
            foreach (var courseRel in courseRels)
            {
                if (!classroomIds.Contains(courseRel.ClassroomId))
                {
                    entities.RelCourseClassrooms.Remove(courseRel);
                    entities.SaveChanges();
                }
            }
        }
    }
}