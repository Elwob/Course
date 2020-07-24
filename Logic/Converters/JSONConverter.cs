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
        private RelCourseContentController relCourseContentController = new RelCourseContentController();
        private RelCourseTrainerController relCourseTrainerController = new RelCourseTrainerController();
        private RelCourseClassroomController relCourseClassroomController = new RelCourseClassroomController();
        private RelCourseSubventionController relCourseSubventionController = new RelCourseSubventionController();

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
            course.Start = DateTime.ParseExact(jsonCourse.Start.Replace('T', ' '), "yyyy-MM-dd HH:mm:ss", null);
            course.End = DateTime.ParseExact(jsonCourse.End.Replace('T', ' '), "yyyy-MM-dd HH:mm:ss", null);
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
            jC.ClassroomArr = relCourseClassroomController.CreateClassroomArr(course.Id);
            jC.participant_max = course.MaxParticipants;
            jC.participant_min = course.MinParticipants;
            jC.TrainerArr = relCourseTrainerController.CreateTrainerArr(course.Id);
            jC.SubventionArr = relCourseSubventionController.CreateSubventionArr(course.Id);
            jC.CreatedAt = course.CreatedAt;
            jC.ModifiedAt = course.ModifiedAt;
            return jC;
        }

        /// <summary>
        /// converts the name of a category to a CourseCategory
        /// </summary>
        /// <param name="catString"></param>
        /// <returns></returns>
        private CourseCategory FindCategory(string catString)
        {
            return entities.CourseCategories.FirstOrDefault(x => x.Name == catString);
        }
    }
}