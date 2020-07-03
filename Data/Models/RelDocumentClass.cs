using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("document_class")]
    public class RelDocumentClass
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("doc_id")]
        public int DocId { get; set; }

        [Column("class", TypeName = "varchar(200)")]
        public string Class { get; set; }

        [Column("class_id")]
        public int ClassId { get; set; }
        [NotMapped]
        public Document Document { get; set; }
    }
}