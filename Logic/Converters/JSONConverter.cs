using Data.Entities;
using Data.Models;
using Data.Models.JSONModels;
using System;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// handles all Model to JSONModel Converstions
    /// </summary>
    public class JSONConverter
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        RelCourseContentController relCourseContentController = new RelCourseContentController();
        RelCourseTrainerController relCourseTrainerController = new RelCourseTrainerController();
        RelCourseClassroomController relCourseClassroomController = new RelCourseClassroomController();
        RelCourseSubventionController relCourseSubventionController = new RelCourseSubventionController();
        ClassroomController classroomController = new ClassroomController();

        /// <summary>
        /// converts a JSONCourseReceive to Course
        /// </summary>
        /// <returns></returns>
        public Course ConvertJSONToCourse(JSONCourseReceive jsonCourse)
        {
            var course = new Course();
            course.Title = jsonCourse.Title;
            course.CourseNumber = jsonCourse.CourseNumber;
            course.Description = jsonCourse.Description;
            course.Category = jsonCourse.Category;
            course.Start = DateTime.ParseExact(jsonCourse.Start, "yyyy-MM-ddTHH:mm", null);
            course.End = DateTime.ParseExact(jsonCourse.End, "yyyy-MM-ddTHH:mm", null);
            course.Unit = jsonCourse.Unit;
            course.Price = jsonCourse.Price;
            course.MaxParticipants = jsonCourse.MaxParticipants;
            course.MinParticipants = jsonCourse.MinParticipants;
            course.CreatedAt = DateTime.Now;
            course.ModifiedAt = DateTime.Now;
            return course;
        }

        /// <summary>
        /// converts a Course to a JSONCourseSend
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public JSONCourseSend ConvertCourseToJSON(Course course)
        {
            var jC = new JSONCourseSend();
            jC.Id = course.Id;
            jC.Title = course.Title;
            jC.CourseNumber = course.CourseNumber;
            jC.Description = course.Description;
            jC.Category = FindCategory(course.Category);
            jC.Start = course.Start;
            jC.End = course.End;
            jC.Content = relCourseContentController.CreateContentArr(course.Id);
            jC.Units = course.Unit;
            jC.Price = course.Price;
            jC.ClassroomArr = classroomController.CreateClassroomArr(course.Id);
            jC.participant_max = course.MaxParticipants;
            jC.participant_min = course.MinParticipants;
            jC.TrainerArr = relCourseTrainerController.CreateTrainerArr(course.Id);
            jC.SubventionArr = relCourseSubventionController.CreateSubventionArr(course.Id);
            jC.CreatedAt = course.CreatedAt;
            jC.ModifiedAt = course.ModifiedAt;
            return jC;
        }
        private CourseCategory FindCategory(string catString)
        {
            return entities.CourseCategories.FirstOrDefault(x => x.Name == catString);
        }
    }
}
