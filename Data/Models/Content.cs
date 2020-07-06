﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// contains information for a certain teaching content
    /// </summary>
    [Table("content")]
    public class Content
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// the main topic of a certain teaching content
        /// </summary>
        [Column("topic")]
        public string Topic { get; set; }
        /// <summary>
        /// a short description of the teaching content
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }
        /// <summary>
        /// an estimation of the used units
        /// </summary>
        [Column("unit_estimation")]
        public int? UnitEstimation { get; set; }
        /// <summary>
        /// contains relations to all courses the content is teached in
        /// </summary>
        [NotMapped]
        public List<RelCourseContent> CourseContents { get; set; }

        public Content(string topic, string description, int? unitEstimation)
        {
            Topic = topic;
            Description = description;
            UnitEstimation = unitEstimation;
        }
    }
}