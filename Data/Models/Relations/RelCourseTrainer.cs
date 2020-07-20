using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between courses and trainers (Persons)
    /// </summary>
    [Table("course_trainer")]
    public class RelCourseTrainer : BaseClassRelation
    {
        /// <summary>
        /// the courses' id
        /// </summary>
        [Column("course_id", TypeName = "int")]
        public int CourseId { get; set; }

        /// <summary>
        /// the trainers' id
        /// </summary>
        [Column("trainer_id", TypeName = "int")]
        public int TrainerId { get; set; }

        [NotMapped]
        public Person Trainer { get; set; }

        [NotMapped]
        public Course Course { get; set; }
    }
}