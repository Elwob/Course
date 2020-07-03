using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// contains contact information
    /// </summary>
    [Table("contact")]
    public class Contact
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// the id of the person the contact information belongs to
        /// </summary>
        [Column("person_id")]
        public int PersonId { get; set; }
        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Person Person { get; set; }
        /// <summary>
        /// the kind of communication ???????
        /// </summary>
        [Column("art_of_communication", TypeName = "varchar(200)")]
        public string ArtOfCommunication { get; set; }
        /// <summary>
        /// the contact value ???????
        /// </summary>
        [Column("contact_value", TypeName = "varchar(200)")]
        public string ContactValue { get; set; }
        /// <summary>
        /// the type of contact
        /// </summary>
        [Column("contact_type", TypeName = "varchar(200)")]
        public string ContactType { get; set; }
        /// <summary>
        /// says if a contact is a persons main contact or not
        /// </summary>
        [Column("main_contact")]
        public bool MainContact { get; set; }
        /// <summary>
        /// date when the contact was created
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// date when the contact was modified
        /// </summary>
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }
    }
}