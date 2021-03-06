﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("course_category")]
    public class CourseCategory
    {
        /// <summary>
        /// the categories' id in DB
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the categories' name in DB
        /// </summary>
        [Column("category")]
        public string Name { get; set; }

        /// <summary>
        /// the categories' background color (set per default if not specified otherwise)
        /// </summary>
        [Column("color")]
        public string Color { get; set; } = "#6CFACB";

        /// <summary>
        /// the categories' font color (set per default if not specified otherwise)
        /// </summary>
        [Column("font_color")]
        public string FontColor { get; set; } = "#00426A";
    }
}