using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between courses and trainers (Persons)
    /// </summary>
    [Table("course_trainer")]
    public class RelCourseTrainer : BaseClassCourseRelation
    {
        /// <summary>
        /// the trainers' id
        /// </summary>
        [Column("trainer_id", TypeName = "int")]
        public int TrainerId { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Person Trainer { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Course Course { get; set; }
    }
}