using Data.Entities;
using Data.Models;
using Data.Models.JSONModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class CourseController
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        /// <summary>
        /// singleton instance
        /// </summary>
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

        public List<Course> GetAll()
        {
            var courses = entities.Courses.Include(c => c.CourseContents).ThenInclude(x => x.Content).Include(x => x.CourseSubventions).ThenInclude(x => x.Subvention).ToList();
            return courses;
        }

        public List<Course> GetFilteredCourses(CourseFilter filter)
        {
            var courses = GetAll();
            courses = FilterStatus(courses, filter);


            return courses;
        }

        public List<Course> FilterStatus(List<Course> courses, CourseFilter filter)
        {
            //var filteredCourses = courses.Where(x => )


            return courses;
        }
    }
}
