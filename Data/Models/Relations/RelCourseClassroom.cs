using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Relations
{
    /// <summary>
    /// intermediate data used to create relations between courses and classrooms
    /// </summary>
    [Table("course_classroom")]
    public class RelCourseClassroom : BaseClassCourseRelation
    {
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