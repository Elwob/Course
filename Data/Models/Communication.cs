using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table ("communication")]
    public class Communication
    {
        [Column ("id")]
        public int Id { get; set; }
        [Column("channel", TypeName = ("varchar(50)"))]
        public string Channel { get; set; }
        [Column("person_id")]
        public int PersonId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("comment", TypeName = ("varchar(500)"))]
        public string Comment { get; set; }
        [Column("document_id")]
        public int DocumentId { get; set; }
        [Column("reminder_id")]
        public int ReminderId { get; set; }
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        [Column("modified@")]
        public DateTime ModifiedAt { get; set; }

    }
}
