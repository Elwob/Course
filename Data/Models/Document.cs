using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("documents")]
    public class Document
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("url", TypeName = "varchar(200)")]
        public string Url { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("comment", TypeName = "varchar(500)")]
        public string Comment { get; set; }

        [Column("reminder_id")]
        public int ReminderId { get; set; }

        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        [Column("modified@")]
        public DateTime ModifiedAt { get; set; }

        [Column("type", TypeName = "varchar(50)")]
        public EDocumentType Type { get; set; }

        [NotMapped]
        public Absence Absence { get; set; }
    }
}