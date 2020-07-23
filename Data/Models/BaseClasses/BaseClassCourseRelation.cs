using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.BaseClasses
{
    /// <summary>
    /// applies to all Rel-Classes that map a relation between a Course and another class
    /// </summary>
    public class BaseClassCourseRelation
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
    }
}