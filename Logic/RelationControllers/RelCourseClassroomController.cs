using Data.Models;
using Data.Models.BaseClasses;
using Data.Models.Relations;
using iText.Signatures;
using Logic.RelationControllers;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Adds & Updates Classrooms in a Course
    /// </summary>
    internal class RelCourseClassroomController : MainRelController<RelCourseClassroom>
    {
        /// <summary>
        ///  creates relations between courses and classrooms
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="objId"></param>
        public void CreateRelation(int courseId, int objId)
        {
            CreateRel(courseId, objId, null, "CourseId", "ClassroomId", null);
        }

        /// <summary>
        /// Updates relations between courses and classrooms
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classroomIds"></param>
        public void UpdateRelations(int courseId, List<int> classroomIds)
        {
            UpdateRels(courseId, classroomIds, "CourseId", "ClassroomId");
        }
    }
}