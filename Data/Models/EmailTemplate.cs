using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("EmailTemplate")]
    public class EmailTemplate
    {
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("document_type", TypeName = "varchar(100)")]
        public string DocumentType { get; set; }

        [Column("text", TypeName = "text(4000)")]
        public string Text { get; set; }
    }
}