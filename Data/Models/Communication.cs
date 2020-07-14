using Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Data.Models
{
    /// <summary>
    /// contains information to a certain communication
    /// </summary>
    [Table("communication")]
    public class Communication
    {
        public Communication()
        {
            CommunicationClasses = new List<RelCommunicationClass>();
        }

        /// <summary>
        /// /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the channel the communication was held on
        /// </summary>
        [Column("channel", TypeName = ("varchar(50)"))]
        public EChannel Channel { get; set; }

        /// <summary>
        /// the id of the person a communication was held with
        /// </summary>
        [Column("person_id")]
        public int PersonId { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Person Person { get; set; }

        /// <summary>
        /// the date the communication was held on
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// a open comment for the communication
        /// </summary>
        [Column("comment", TypeName = ("varchar(500)"))]
        public string? Comment { get; set; }

        /// <summary>
        /// the id of a belonging document
        /// </summary>
        [Column("document_id")]
        public int? DocumentId { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Document Document { get; set; }

        /// <summary>
        /// contains the id of a belonging reminder (Reminders not implemented yet)
        /// </summary>
        [Column("reminder_id")]
        public int? ReminderId { get; set; }

        /// <summary>
        /// date when the communication was created
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// date when the communication was modified
        /// </summary>
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// temporarily needed for building the right relationships in RelCommunicationClass
        /// </summary>
        [NotMapped]
        [Communication(typeof(Course))]
        public int? CourseId { get; set; }

        /// <summary>
        /// temporarily needed for building the right relationships in RelCommunicationClass
        /// </summary>
        [NotMapped]
        [Communication(typeof(Person))]
        public int? TrainerId { get; set; }

        /// <summary>
        /// list of all relations between communications and classes
        /// </summary>
        [NotMapped]
        public List<RelCommunicationClass> CommunicationClasses { get; set; }

        public void CreateRelation()
        {
            var properties = this.GetType().GetProperties().Where(c => c.GetCustomAttribute<CommunicationAttribute>() != null);

            foreach (var property in properties)
            {
                CreateRelation(property);
            }
        }

        private void CreateRelation(PropertyInfo prop)
        {
            var communicationAttr = prop.GetCustomAttribute<CommunicationAttribute>();

            if (prop != null)
            {
                var id = prop.GetValue(this) as int?;
                if (id.HasValue)
                {
                    RelCommunicationClass relCommunicationClass = new RelCommunicationClass();

                    relCommunicationClass.Communication = this;
                    relCommunicationClass.Class = communicationAttr.ClassName;
                    relCommunicationClass.ClassId = id.Value;
                    this.CommunicationClasses.Add(relCommunicationClass);
                }
            }
        }
    }
}