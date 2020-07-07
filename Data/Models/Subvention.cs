using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// contains information to a certain subvention
    /// </summary>
    [Table("subvention")]
    public class Subvention
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the subventions' name
        /// </summary>
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }

        /// <summary>
        /// the percentage the subvention
        /// </summary>
        [Column("percentage")]
        public double? Percentage { get; set; }

        /// <summary>
        /// the amount of the subvention
        /// </summary>
        [Column("amount")]
        public double? Amount { get; set; }

        /// <summary>
        /// a list of relations to courses
        /// </summary>
        [NotMapped]
        public List<RelCourseSubvention> CourseSubventions { get; set; }

        public Subvention(string name, double? percentage, double? amount)
        {
            Name = name;
            Percentage = percentage;
            Amount = amount;
        }
    }
}