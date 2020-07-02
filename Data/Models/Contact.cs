using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table ("contact")]
    public class Contact
    {
        [Column ("id")]
        public int Id { get; set; }
        [Column("person_id")]
        public int PersonId { get; set; }
        [Column("art_of_communication", TypeName = "varchar(200)")]
        public string ArtOfCommunication { get; set; }
        [Column("contact_value", TypeName = "varchar(200)")]
        public string ContactValue { get; set; }
        [Column("contact_type", TypeName = "varchar(200)")]
        public string ContactType { get; set; }
        [Column("main_contact")]
        public bool MainContact { get; set; }
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        [Column("modified@")]
        public DateTime ModifiedAt { get; set; }

    }
}
