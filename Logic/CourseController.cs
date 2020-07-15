using Data.Entities;
using Data.Models;
using Data.Models.JSONModels;
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
        ClassroomController classroomController = new ClassroomController();

        public List<JSONCourseSend> GetAllCourses()
        {
            var courses = GetAll();
            var jsonCourses = new List<JSONCourseSend>();
            foreach (var course in courses)
            {
                jsonCourses.Add(ConvertCourseToJSON(course));
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
                jsonCourses.Add(ConvertCourseToJSON(course));
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
            Course course = ConvertJSONToCourse(jsonCourse);
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
            foreach (var classroom in jsonCourse.ClassroomArr)
            {
                relCourseClassroomController.CreateRelation(course.Id, classroom);
            }
            return ConvertCourseToJSON(course);
        }

        /// <summary>
        /// converts a JSONCourseReceive to Course
        /// </summary>
        /// <returns></returns>
        private Course ConvertJSONToCourse(JSONCourseReceive jasonCourse)
        {
            var course = new Course();
            course.Title = jasonCourse.Title;
            course.CourseNumber = jasonCourse.CourseNumber;
            course.Description = jasonCourse.Description;
            Enum.TryParse(jasonCourse.Category, out ECourseCategory courseCategory);
            course.Category = courseCategory;
            course.Start = DateTime.ParseExact(jasonCourse.Start.Replace('T', ' '), "yyyy-MM-dd HH:mm", null);
            course.End = DateTime.ParseExact(jasonCourse.End.Replace('T', ' '), "yyyy-MM-dd HH:mm", null);
            course.Unit = jasonCourse.Units;
            course.Price = jasonCourse.Price;
            course.MaxParticipants = jasonCourse.MaxParticipants;
            course.MinParticipants = jasonCourse.MinParticipants;
            course.CreatedAt = DateTime.Now;
            course.ModifiedAt = DateTime.Now;
            return course;
        }

        private JSONCourseSend ConvertCourseToJSON(Course course)
        {
            var jC = new JSONCourseSend();
            jC.Id = course.Id;
            jC.Title = course.Title;
            jC.CourseNumber = course.CourseNumber;
            jC.Description = course.Description;
            jC.Category = course.Category.ToString();
            jC.Start = course.Start.ToString();
            jC.End = course.End.ToString();
            jC.Content = CreateContentArr(course.Id);
            jC.Units = course.Unit;
            jC.Price = course.Price;
            
            
            //jC.ClassroomArr = classroomController.ConvertClassroomToJSON(course.Classroom);


            jC.participant_max = course.MaxParticipants;
            jC.participant_min = course.MinParticipants;
            jC.TrainerArr = CreateTrainerArr(course.Id);
            jC.CreatedAt = course.CreatedAt;
            jC.ModifiedAt = course.ModifiedAt;
            return jC;
        }

        private List<JSONContentSend> CreateContentArr(int courseId)
        {
            var jsonContents = new List<JSONContentSend>();
            // get all course-content relations where a certain course exists
            var relations = entities.RelCourseContents.Where(x => x.CourseId == courseId).ToList();
            // filter contents for existing course-content relations
            var c = entities.Contents.ToList();
            var contents = c.Where(x => relations.Any(z => x.Id == z.ContentId)).ToList();
            // convert to JSONContentSend
            foreach (var content in contents)
            {
                // get correct units (in a specific course, not estimation)
                var units = relations.Where(x => x.CourseId == courseId && x.ContentId == content.Id).FirstOrDefault().Units;
                // create and add jContent
                jsonContents.Add(new JSONContentSend(content.Id, content.Topic, content.Description, units));
            }
            return jsonContents;
        }

        private List<JSONTrainer> CreateTrainerArr(int courseId)
        {
            var jsonTrainers = new List<JSONTrainer>();
            // get all belonging trainers
            var relations = entities.RelCourseTrainers.Where(x => x.CourseId == courseId).ToList();
            var t = entities.Persons.Where(x => x.Function == "0" || x.Function == "1").ToList();
            var trainers = t.Where(x => relations.Any(z => x.Id == z.TrainerId)).ToList();
            //Convert to JSONTrainer
            foreach (var trainer in trainers)
            {
                jsonTrainers.Add(new JSONTrainer(trainer.Id, trainer.FirstName, trainer.LastName));
            }
            return jsonTrainers;
        }

        public void DeleteCourse(int id)
        {
            entities.Courses.Remove(entities.Courses.Single(x => x.Id == id));
            entities.SaveChanges();
        }
    }
}