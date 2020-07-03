using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name1", TypeName = "varchar(200)")]
        public string FirstName { get; set; }

        [Column("name2", TypeName = "varchar(200)")]
        public string LastName { get; set; }

        [Column("title", TypeName = "varchar(200)")]
        public string Title { get; set; }

        [Column("sv_nr")]
        public int InsuranceNumber { get; set; }

        [Column("date")]
        public DateTime DateOfBirth { get; set; }

        [Column("gender", TypeName = "varchar(200)")]
        public string Gender { get; set; }

        [Column("busy", TypeName = "varchar(200)")]
        public string Busy { get; set; }

        [Column("busy_by", TypeName = "varchar(200)")]
        public string BusyBy { get; set; }

        [Column("picture")]
        public string Picture { get; set; }

        [Column("function", TypeName = "varchar(200)")]
        public string Function { get; set; }

        [Column("aktiv")]
        public bool Active { get; set; }

        [Column("deleted_inaktiv")]
        public bool Deleted { get; set; }

        [Column("newsletter_flag")]
        public bool WantsNewsletter { get; set; }

        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        [Column("modify@")]
        public DateTime ModifiedAt { get; set; }

        [NotMapped]
        public List<RelAddressPerson> Adresses { get; set; }

        [NotMapped]
        public List<Comment> Comments { get; set; }

        [NotMapped]
        public List<Contact> Contacts { get; set; }

        [NotMapped]
        public List<RelCourseParticipant> RelCourseParticipants { get; set; }

        [NotMapped]
        public List<RelCourseTrainer> RelCourseTrainers { get; set; }

        [NotMapped]
        public List<Absence> Absences { get; set; }
    }
}