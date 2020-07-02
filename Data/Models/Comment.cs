using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("comment")]
    public class Comment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("comment_value")]
        public string CommentValue { get; set; }

        [Column("value_date")]
        public DateTime ValueDate { get; set; }

        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        [Column("modify@")]
        public DateTime ModifiedAt { get; set; }
    }
}