using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between documents and classes (e.g. Person, Course, etc.)
    /// </summary>
    [Table("document_class")]
    public class RelDocumentClass : BaseClassRelation
    {
        /// <summary>
        /// the documents' id
        /// </summary>
        [Column("doc_id")]
        public int DocId { get; set; }

        /// <summary>
        /// the class (e.g. Person or Course)
        /// </summary>
        [Column("class", TypeName = "varchar(200)")]
        public string Class { get; set; }

        /// <summary>
        /// the id of the item to link to
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Document Document { get; set; }
    }
}