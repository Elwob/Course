using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("RelCourseTrainer")]
    public class RelCourseTrainer
    {
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("course_id", TypeName = "int")]
        public int CourseId { get; set; }

        [Column("trainer_id", TypeName = "int")]
        public int TrainerID { get; set; }

        [NotMapped]
        public Person Person { get; set; }

        [NotMapped]
        public Course Course { get; set; }
    }
}