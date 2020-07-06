using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// contains information to a certain document
    /// </summary>
    [Table("documents")]
    public class Document
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the filePath where the document is placed
        /// </summary>
        [Column("url", TypeName = "varchar(200)")]
        public string Url { get; set; }

        /// <summary>
        /// the name of the document
        /// </summary>
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        /// <summary>
        /// an open comment concerning the document
        /// </summary>
        [Column("comment", TypeName = "varchar(500)")]
        public string? Comment { get; set; }

        /// <summary>
        /// contains the id of a belonging reminder (Reminders not implemented yet)
        /// </summary>
        [Column("reminder_id")]
        public int? ReminderId { get; set; }

        /// <summary>
        /// the date the document was created at
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// the date the document was modified at
        /// </summary>
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// the documents' type
        /// </summary>
        [Column("type", TypeName = "varchar(50)")]
        public EDocumentType Type { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Absence Absence { get; set; }

        /// <summary>
        /// temporarily needed for building the right relationships in RelDocumentClass
        /// </summary>
        [NotMapped]
        public int? CourseId { get; set; }
        /// <summary>
        /// temporarily needed for building the right relationships in RelDocumentClass
        /// </summary>
        [NotMapped]
        public int? PersonId { get; set; }

        /// <summary>
        /// contains relations to "classes" (e.g. Persons or Courses)
        /// </summary>
        [NotMapped]
        public List<RelDocumentClass> DocumentClasses { get; set; }
    }
}