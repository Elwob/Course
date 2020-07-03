using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// a student's absence to a certain course
    /// </summary>
    [Table("absence")]
    public class Absence
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// a absences' start date
        /// </summary>
        [Column("start")]
        public DateTime Start { get; set; }
        /// <summary>
        /// a absences' end date
        /// </summary>
        [Column("end")]

        public DateTime? End { get; set; }
        /// <summary>
        /// the absent participant's id
        /// </summary>
        [Column("participant_id")]
        public int ParticipantId { get; set; }
        /// <summary>
        /// the course the participant is absent from (id)
        /// </summary>
        [Column("course_id")]
        public int CourseId { get; set; }
        /// <summary>
        /// the amount of units the participant was absent
        /// </summary>
        [Column("units")]

        public int? Units { get; set; }
        /// <summary>
        /// says if the participant was excused or not
        /// </summary>
        [Column("excused")]
        public bool? Excused { get; set; }
        /// <summary>
        /// the document id of e.g. a medical certificate
        /// </summary>
        [Column("document_id")]
        public int? DocumentId { get; set; }
        /// <summary>
        /// says if the absence has ended yet
        /// </summary>
        [Column("completed")]
        public bool Completed { get; set; }
        /// <summary>
        /// contains the id of a belonging reminder (Reminders not implemented yet)
        /// </summary>
        [Column("reminder_id")]
        public int? ReminderId { get; set; }
        /// <summary>
        /// a open comment
        /// </summary>
        [Column("comment", TypeName = "varchar(1000)")]
        public string? comment { get; set; }

        [NotMapped]
        public Course Course { get; set; }

        [NotMapped]
        public Person Person { get; set; }

        [NotMapped]
        public List<Document> Documents { get; set; }
    }
}