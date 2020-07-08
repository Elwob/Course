using Data.Entities;
using Data.Models;
using Data.Models.JSONModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class CourseController
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        /// <summary>
        /// singleton instance
        /// </summary>
        /// 

        RelCourseContentController relCourseContentController = new RelCourseContentController();
        RelCourseTrainerController relCourseTrainerController = new RelCourseTrainerController();

        public static CourseController instance = null;

        /// <summary>
        /// returns existing singleton instance or new instance if none exists
        /// </summary>
        /// <returns></returns>
        public static CourseController GetInstance()
        {
            if (instance == null)
            {
                instance = new CourseController();
            }
            return instance;
        }

        /// <summary>
        /// returns a list of all courses in DB
        /// </summary>
        /// <returns></returns>
        public List<Course> GetAll()
        {
            var courses = entities.Courses
                .Include(c => c.CourseContents).ThenInclude(x => x.Content)
                .Include(x => x.CourseSubventions).ThenInclude(x => x.Subvention)
                .ToList();
            return courses;
        }

        /// <summary>
        /// filters for four filter options and returns courses as list
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> GetFilteredCourses(CourseFilter filter)
        {
            var courses = GetAll();
            courses = FilterStatus(courses, filter);
            courses = FilterTrainer(courses, filter);
            courses = FilterCategory(courses, filter);
            courses = FilterSearch(courses, filter);
            return courses;
        }

        /// <summary>
        /// filters courses for a certain status (planned, active, completed)
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> FilterStatus(List<Course> courses, CourseFilter filter)
        {
            if (filter.status != null && filter.status.Length > 0)
            {
                if (filter.status == "active")
                {
                    courses = courses.Where(x => x.Start < DateTime.Now && x.End > DateTime.Now).ToList();
                }
                else if (filter.status == "planned")
                {
                    courses = courses.Where(x => x.Start > DateTime.Now).ToList();
                }
                else if (filter.status == "completed")
                {
                    courses = courses.Where(x => x.End > DateTime.Now).ToList();
                }
            }
            return courses;
        }

        /// <summary>
        /// filters courses assigned to a certain trainer
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> FilterTrainer(List<Course> courses, CourseFilter filter)
        {
            if (filter.trainer_id != 0)
            {
                // get all course-trainer relations where a certain trainer exists
                var relations = entities.RelCourseTrainers.Where(x => x.TrainerID == filter.trainer_id).ToList();
                // filter courses for existing course-trainer relations
                courses = courses.Where(x => relations.Any(z => x.Id == z.CourseId)).ToList();
            }
            return courses;
        }

        /// <summary>
        /// filters for a certain course category (e.g. CodingCampus)
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> FilterCategory(List<Course> courses, CourseFilter filter)
        {
            if (filter.category != null && filter.category.Length > 0)
            {
                Enum.TryParse(filter.category, out ECourseCategory category);
                courses = courses.Where(x => x.Category == category).ToList();
            }
            return courses;
        }

        /// <summary>
        /// filters course titles for an entry in the search field
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> FilterSearch(List<Course> courses, CourseFilter filter)
        {
            if (filter.search != null && filter.search.Length > 0)
            {
                courses = courses.Where(x => x.Title.Contains(filter.search.ToLower())).ToList();
            }
            return courses;
        }

        /// <summary>
        /// adds a course to DB (+ relations to trainers and contents)
        /// </summary>
        /// <param name="jsonCourse"></param>
        /// <returns></returns>
        public Course PostCourse(JSONCourse jsonCourse)
        {
            Course course = ConvertToCourse(jsonCourse);
            entities.Courses.Add(course);
            entities.SaveChanges();
            // create trainer relations
            foreach (var trainerid in jsonCourse.TrainerArr)
            {
                relCourseTrainerController.CreateRelation(course.Id, trainerid);
            }
            // create content relations
            foreach (var content in jsonCourse.ContentArr)
            {
                relCourseContentController.CreateRelation(course.Id, content);
            }
            return course;
        }

        /// <summary>
        /// converts a JSONCourse to Course
        /// </summary>
        /// <returns></returns>
        private Course ConvertToCourse(JSONCourse jC)
        {
            var course = new Course();
            course.Title = jC.Title;
            course.CourseNumber = jC.CourseNumber;
            course.Description = jC.Description;
            Enum.TryParse(jC.Category, out ECourseCategory courseCategory);
            course.Category = courseCategory;
            course.Start = DateTime.ParseExact(jC.Start.Replace('T', ' '), "yyyy-MM-dd HH:mm:ss", null);
            course.End = DateTime.ParseExact(jC.End.Replace('T', ' '), "yyyy-MM-dd HH:mm:ss", null);
            course.Unit = jC.Unit;
            course.Price = jC.Price;
            course.ClassroomId = jC.ClassroomId;
            course.MaxParticipants = jC.MaxParticipants;
            course.MinParticipants = jC.MinParticipants;
            course.CreatedAt = DateTime.Now;
            course.ModifiedAt = DateTime.Now;
            course.TrainerArr = entities.Persons.Where(x => jC.TrainerArr.Any(y => x.Id == y)).ToList();
            course.ContentArr = jC.ContentArr;
            return course;
        }
    }    
}
