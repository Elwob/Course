using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table ("absence")]
    public class Absence
    {
        [Column ("id")]
        public int Id { get; set; }
        [Column("start")]
        public DateTime Start { get; set; }
        [Column("end")]
        public DateTime End { get; set; }
        [Column("participant_id")]
        public int ParticipantId { get; set; }
        [Column("course_id")]
        public int CourseId { get; set; }
        [Column("units")]
        public int Units { get; set; }
        [Column("excused")]
        public bool Excused { get; set; }
        [Column("document_id")]
        public int DocumentId { get; set; }
        [Column("completed")]
        public bool Completed { get; set; }
        [Column("reminder_id")]
        public int ReminderId { get; set; }
        [Column("comment", TypeName = "varchar(1000)")]
        public string comment { get; set; }
    }
}
