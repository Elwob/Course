using Data.Entities;
using Data.Models;
using Data.Models.JSONModels;
using Logic.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class CourseController
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        RelCourseContentController relCourseContentController = new RelCourseContentController();
        RelCourseTrainerController relCourseTrainerController = new RelCourseTrainerController();
        RelCourseClassroomController relCourseClassroomController = new RelCourseClassroomController();
        RelCourseSubventionController relCourseSubventionController = new RelCourseSubventionController();
        JSONConverter jsonConverter = new JSONConverter();

        /// <summary>
        /// finds all courses as List<JSONCourseSend>
        /// </summary>
        /// <returns></returns>
        public List<JSONCourseSend> GetAllCourses()
        {
            var courses = GetAll();
            var jsonCourses = new List<JSONCourseSend>();
            foreach (var course in courses)
            {
                jsonCourses.Add(jsonConverter.ConvertCourseToJSON(course));
            }
            return jsonCourses;
        }

        /// <summary>
        /// returns a list of all courses in DB
        /// </summary>
        /// <returns></returns>
        public List<Course> GetAll()
        {
            var courses = entities.Courses
                .Include(x => x.CourseContents).ThenInclude(x => x.Content)
                .Include(x => x.CourseSubventions).ThenInclude(x => x.Subvention)
                .Include(x => x.CourseTrainers).ThenInclude(x => x.Trainer)
                .Include(x => x.CourseClassrooms).ThenInclude(x => x.Classroom)
                .ToList();
            return courses;
        }

        /// <summary>
        /// filters for four filter options and returns courses as list
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<JSONCourseSend> GetFilteredCourses(CourseFilter filter)
        {
            var courses = GetAll();
            courses = FilterStatus(courses, filter);
            courses = FilterTrainer(courses, filter);
            courses = FilterCategory(courses, filter);
            courses = FilterSearch(courses, filter);
            courses = FilterContent(courses, filter);
            var jsonCourses = new List<JSONCourseSend>();
            foreach (var course in courses)
            {
                jsonCourses.Add(jsonConverter.ConvertCourseToJSON(course));
            }
            return jsonCourses;
        }

        /// <summary>
        /// filters courses for a certain status (planned, active, completed)
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> FilterStatus(List<Course> courses, CourseFilter filter)
        {
            if (filter.status != null && filter.status.Count > 0)
            {
                if (!filter.status.Contains("active"))
                {
                    var courseRemoveActive = courses.Where(x => x.Start < DateTime.Now && x.End > DateTime.Now).ToList();
                    foreach (var course in courseRemoveActive)
                    {
                        courses.Remove(course);
                    }
                }
                if (!filter.status.Contains("planned"))
                {
                    var courseRemovePlanned = courses.Where(x => x.Start > DateTime.Now).ToList();
                    foreach (var course in courseRemovePlanned)
                    {
                        courses.Remove(course);
                    }
                    
                }
                if (!filter.status.Contains("completed"))
                {
                    var courseRemoveCompleted = courses.Where(x => x.Start < DateTime.Now && x.End < DateTime.Now).ToList();
                    foreach (var course in courseRemoveCompleted)
                    {
                        courses.Remove(course);
                    }
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
            if (filter.trainer_id != null && filter.trainer_id != 0)
            {
                // get all course-trainer relations where a certain trainer exists
                var relations = entities.RelCourseTrainers.Where(x => x.TrainerId == filter.trainer_id).ToList();
                // filter courses for existing course-trainer relations
                courses = courses.Where(x => relations.Any(z => x.Id == z.CourseId)).ToList();
            }
            return courses;
        }

        /// <summary>
        /// filters for a certain course category (e.g. Coding Campus)
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> FilterCategory(List<Course> courses, CourseFilter filter)
        {
            if (filter.category != null && filter.category.Length > 0)
            {
                courses = courses.Where(x => x.Category == filter.category).ToList();
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
            // not the best looking code but working...
            if (filter.search != null && filter.search.Length > 0)
            {
                var filteredCourses = new List<Course>();
                foreach (var course in courses)
                {
                    var title = course.Title.ToLower();
                    var search = filter.search.ToLower();
                    if (title.Contains(search))
                    {
                        filteredCourses.Add(course);
                    }
                }
                return filteredCourses;
            }
            else
            {
                return courses;
            }
        }

        /// <summary>
        /// filters
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Course> FilterContent(List<Course> courses, CourseFilter filter)
        {
            if (filter.content_id != null && filter.content_id != 0)
            {
                // get all course-content relations where a certain content exists
                var relations = entities.RelCourseContents.Where(x => x.ContentId == filter.content_id).ToList();
                // filter courses for existing course-content relations
                courses = courses.Where(x => relations.Any(z => x.Id == z.CourseId)).ToList();
            }
            return courses;
        }

        /// <summary>
        /// adds a course to DB (+ relations to trainers and contents)
        /// </summary>
        /// <param name="jsonCourse"></param>
        /// <returns></returns>
        public JSONCourseSend PostCourse(JSONCourseReceive jsonCourse)
        {
            Course course = jsonConverter.ConvertJSONToCourse(jsonCourse);
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
            // create classroom relations
            foreach (var classroomId in jsonCourse.ClassroomArr)
            {
                relCourseClassroomController.CreateRelation(course.Id, classroomId);
            }
            // create subvention relations
            foreach (var subvention in jsonCourse.SubventionArr)
            {
                relCourseSubventionController.CreateRelation(course.Id, subvention);
            }
            return jsonConverter.ConvertCourseToJSON(course);
        }

        /// <summary>
        /// updates an existing course in DB
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="courseReceive"></param>
        /// <returns></returns>
        public JSONCourseSend UpdateCourse(int courseId, JSONCourseReceive courseReceive)
        {
            if (entities.Courses.FirstOrDefault(x => x.Id == courseId) != null)
            {
                Course courseNew = jsonConverter.ConvertJSONToCourse(courseReceive);
                courseNew.Id = courseId;
                entities.Entry(entities.Courses.FirstOrDefault(x => x.Id == courseId)).CurrentValues.SetValues(courseNew);
                entities.SaveChanges();
                // update trainer relations
                relCourseTrainerController.UpdateRelations(courseId, courseReceive.TrainerArr);
                // update content relations
                relCourseContentController.UpdateRelations(courseId, courseReceive.ContentArr);
                // update classroom relations
                relCourseClassroomController.UpdateRelations(courseId, courseReceive.ClassroomArr);
                // update subvention relations
                relCourseSubventionController.UpdateRelations(courseId, courseReceive.SubventionArr);
                return jsonConverter.ConvertCourseToJSON(entities.Courses.FirstOrDefault(x => x.Id == courseId));
            }
            else
            {
                throw new EntryCouldNotBeFoundException("The course you want to update could not be found in database");
            }
        }

        /// <summary>
        /// deletes a certain course in db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCourse(int id)
        {
            if (entities.Courses.FirstOrDefault(x => x.Id == id) != null)
            {
                entities.Courses.Remove(entities.Courses.Single(x => x.Id == id));
                entities.SaveChanges();
            }
            else
            {
                throw new EntryCouldNotBeFoundException("The course you want to delete could not be found in database");
            }
        }
    }
}