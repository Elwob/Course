using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between courses and participants (Persons)
    /// </summary>
    [Table("course_participants")]
    public class RelCourseParticipant : BaseClassCourseRelation
    {
        /// <summary>
        /// the participants' id
        /// </summary>
        [Column("participant_id", TypeName = "int")]
        public int ParticipantId { get; set; }

        /// <summary>
        /// the participants Status
        /// </summary>
        [Column("completed", TypeName = "tinyint")]
        public bool Completed { get; set; }

        [NotMapped]
        public Course Course { get; set; }

        [NotMapped]
        public Person Person { get; set; }
    }
}