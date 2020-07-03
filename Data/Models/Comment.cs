using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// a comment
    /// </summary>
    [Table("comment")]
    public class Comment
    {
        /// <summary>
        /// /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// the person a comment belongs to
        /// </summary>
        [Column("person_id")]
        public int PersonId { get; set; }
        /// <summary>
        /// the text of the comment
        /// </summary>
        [Column("comment_value")]
        public string CommentValue { get; set; }
        /// <summary>
        /// contains a date ??????????
        /// </summary>
        [Column("value_date")]
        public DateTime ValueDate { get; set; }
        /// <summary>
        /// date when the address was created
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// date when the address was modified
        /// </summary>
        [Column("modify@")]
        public DateTime? ModifiedAt { get; set; }
    }
}