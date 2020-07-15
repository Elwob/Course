using Data.Models.JSONModels;
using Data.Models.Relations;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class RelCourseClassroomController : MainController
    {
        public void CreateRelation(int courseId, int classroomId)
        {
            entities.RelCourseClassrooms.Add(new RelCourseClassroom() { CourseId = courseId, ClassroomId = classroomId });
            entities.SaveChanges();
        }
    }
}
