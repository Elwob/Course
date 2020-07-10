using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// contains information to a certain email template
    /// </summary>
    [Table("EmailTemplate")]
    public class EmailTemplate
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// contains the type of document a template is for
        /// </summary>
        [Column("document_type", TypeName = "varchar(100)")]
        public EDocumentType DocumentType { get; set; }

        /// <summary>
        /// text of the template
        /// </summary>
        [Column("text", TypeName = "text(4000)")]
        public string Text { get; set; }

        /// <summary>
        /// Person id`s for Diplomas...
        /// </summary>
        [NotMapped]
        public int[] PersonIds { get; set; }

        /// Course id for Communication Entry
        /// </summary>
        [NotMapped]
        public int CourseId { get; set; }

        /// <summary>
        /// An Id from whom the messages are send
        /// </summary>
        [NotMapped]
        public int? TrainerId { get; set; }
        /// <summary>
        /// Comment for Communication
        /// </summary>
        [NotMapped]
        public string Comment { get; set; }

    }
}