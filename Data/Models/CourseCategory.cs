using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("course_category")]
    public class CourseCategory
    {
        /// <summary>
        /// the categorys' id in DB
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the categorys' name in DB
        /// </summary>
        [Column("category")]
        public string Name { get; set; }

        /// <summary>
        /// the categorys' background color
        /// </summary>
        [Column("color")]
        public string Color { get; set; }

        /// <summary>
        /// the categorys' font color
        /// </summary>
        [Column("font_color")]
        public string FontColor { get; set; }
    }
}
