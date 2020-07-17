using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models.Relations
{
    /// <summary>
    /// intermediate data used to create relations between courses and classrooms
    /// </summary>
    [Table("course_classroom")]
    public class RelCourseClassroom
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the courses' id
        /// </summary>
        [Column("course_id")]
        public int CourseId { get; set; }

        /// <summary>
        /// the classrooms' id
        /// </summary>
        [Column("classroom_id")]
        public int ClassroomId { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Course Course { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Classroom Classroom { get; set; }
    }
}
