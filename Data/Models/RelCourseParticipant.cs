using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("RelCourseParticipants")]
    public class RelCourseParticipant
    {
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("course_id", TypeName = "int")]
        public int CourseId { get; set; }

        [Column("participant_id", TypeName = "int")]
        public int ParticipantId { get; set; }

        [NotMapped]
        public Course Course { get; set; }

        [NotMapped]
        public Person Person { get; set; }

    }
}