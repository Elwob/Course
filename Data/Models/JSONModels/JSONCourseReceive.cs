using PersonData;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Data.Models.JSONModels
{
    public class JSONCourseReceive
    {
        /// <summary>
        /// the courses' title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the courses number (can contain letters)
        /// </summary>
        public string? CourseNumber { get; set; }

        /// <summary>
        /// a short description for the course
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// the category the course belongs to
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// the courses' start date
        /// </summary>
        public string? Start { get; set; }

        /// <summary>
        /// the courses' end date
        /// </summary>
        public string? End { get; set; }

        /// <summary>
        /// the amount a teaching units of the course
        /// </summary>
        public int? Unit { get; set; }

        /// <summary>
        /// the price of the course
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// a list of classrooms the course is held in
        /// </summary>
        public List<int> ClassroomArr { get; set; }

        /// <summary>
        /// the amount of maximum participants
        /// </summary>
        public int? MaxParticipants { get; set; }

        /// <summary>
        /// the amount of minimum participants
        /// </summary>
        public int? MinParticipants { get; set; }

        /// <summary>
        /// contains all trainerIds
        /// </summary>
        public List<int> TrainerArr { get; set; }

        /// <summary>
        /// contains all contents (as JSONContentReceive)
        /// </summary>
        public List<JSONContentReceive> ContentArr { get; set; }

        /// <summary>
        /// contains all subventionIds
        /// </summary>
        public List<int> SubventionsArr { get; set; }
    }
}
