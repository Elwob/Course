﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between courses and participants (Persons)
    /// </summary>
    [Table("RelCourseParticipants")]
    public class RelCourseParticipant
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id", TypeName = "int")]
        public int Id { get; set; }
        /// <summary>
        /// the courses' id
        /// </summary>
        [Column("course_id", TypeName = "int")]
        public int CourseId { get; set; }
        /// <summary>
        /// the participants' id
        /// </summary>
        [Column("participant_id", TypeName = "int")]
        public int ParticipantId { get; set; }
    }
}