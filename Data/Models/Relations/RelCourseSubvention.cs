﻿using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between courses and subventions
    /// </summary>
    [Table("course_subvention")]
    public class RelCourseSubvention : BaseClassCourseRelation
    {
        /// <summary>
        /// the subventions' id
        /// </summary>
        [Column("subvention_id")]
        public int SubventionId { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Course Course { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Subvention Subvention { get; set; }
    }
}