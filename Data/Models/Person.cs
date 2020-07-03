using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// a persons main data
    /// </summary>
    [Table("person")]
    public class Person
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// name one of a entity (if person first name)
        /// </summary>
        [Column("name1", TypeName = "varchar(200)")]
        public string FirstName { get; set; }
        /// <summary>
        /// name two of a entity (if person last name)
        /// </summary>
        [Column("name2", TypeName = "varchar(200)")]
        public string LastName { get; set; }
        /// <summary>
        /// academic title
        /// </summary>
        [Column("title", TypeName = "varchar(200)")]
        public string? Title { get; set; }
        /// <summary>
        /// the persons' social security number
        /// </summary>
        [Column("sv_nr")]
        public int? InsuranceNumber { get; set; }
        /// <summary>
        /// the persons' birth date
        /// </summary>
        [Column("date")]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// the persons' gender
        /// </summary>
        [Column("gender", TypeName = "varchar(200)")]
        public string? Gender { get; set; }
        /// <summary>
        /// the persons' employment status ???????
        /// </summary>
        [Column("busy", TypeName = "varchar(200)")]
        public string? Busy { get; set; }
        /// <summary>
        /// the persons' employer ?????????
        /// </summary>
        [Column("busy_by", TypeName = "varchar(200)")]
        public string? BusyBy { get; set; }
        /// <summary>
        /// picture of the person
        /// </summary>
        [Column("picture")]
        public string? Picture { get; set; }
        /// <summary>
        /// the persons' current function
        /// </summary>
        [Column("function", TypeName = "varchar(200)")]
        public string Function { get; set; }
        /// <summary>
        /// says if a person is an active user ???????
        /// </summary>
        [Column("aktiv")]
        public bool Active { get; set; }
        /// <summary>
        /// says if a person was deleted ???????
        /// </summary>
        [Column("deleted_inaktiv")]
        public bool Deleted { get; set; }
        /// <summary>
        /// says if a person wants to recieve newsletters or not
        /// </summary>
        [Column("newsletter_flag")]
        public bool WantsNewsletter { get; set; }
        /// <summary>
        /// Date the person was created on
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// the date the person was modified on
        /// </summary>
        [Column("modify@")]
        public DateTime? ModifiedAt { get; set; }
        /// <summary>
        /// a list of relations to addresses
        /// </summary>
        [NotMapped]
        public List<RelAddressPerson> Addresses { get; set; }
        /// <summary>
        /// a list of relations to comments
        /// </summary>
        [NotMapped]
        public List<Comment> Comments { get; set; }
        /// <summary>
        /// a list of relations to contacts
        /// </summary>
        [NotMapped]
        public List<Contact> Contacts { get; set; }
    }
}