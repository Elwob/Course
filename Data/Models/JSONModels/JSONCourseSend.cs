﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.JSONModels
{
    public class JSONCourseSend
    {
        /// <summary>
        /// the courses' id (autoIncrement generated by database)
        /// </summary>
        public int Id { get; set; }

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
        public CourseCategory Category { get; set; }

        /// <summary>
        /// the courses' start date
        /// </summary>
        public string? Start { get; set; }

        /// <summary>
        /// the courses' end date
        /// </summary>
        public string? End { get; set; }

        /// <summary>
        /// contains all contents
        /// </summary>
        public List<JSONContentSend> Content { get; set; }

        /// <summary>
        /// the amount a teaching units of the course
        /// </summary>
        public int? Units { get; set; }

        /// <summary>
        /// the price of the course
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// the id of the classromm the course is held in
        /// </summary>
        public List<JSONClassroom>? ClassroomArr { get; set; }

        /// <summary>
        /// the amount of maximum participants
        /// </summary>
        public int? participant_max { get; set; }

        /// <summary>
        /// the amount of minimum participants
        /// </summary>
        public int? participant_min { get; set; }

        /// <summary>
        /// contains all trainerIds
        /// </summary>
        public List<JSONTrainer> TrainerArr { get; set; }

        /// <summary>
        /// contains all subventions
        /// </summary>
        public List<Subvention> SubventionArr { get; set; }

        /// <summary>
        /// the date the document was created at
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// the date the document was modified at
        /// </summary>
        public DateTime? ModifiedAt { get; set; }
    }
}
