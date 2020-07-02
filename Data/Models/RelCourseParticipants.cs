using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("RelCourseParticipants")]
    public class RelCourseParticipants
    {
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("course_id", TypeName = "int")]
        public int CourseId { get; set; }

        [Column("participant_id", TypeName = "int")]
        public int ParticipantId { get; set; }
    }
}