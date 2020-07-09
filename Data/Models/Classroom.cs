using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// contains information to a certain classroom
    /// </summary>
    [Table("classrooms")]
    public class Classroom
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// name of the room
        /// </summary>
        [Column("room")]
        public string Room { get; set; }

        /// <summary>
        /// ???
        /// </summary>
        [Column("facility_id")]
        public int? FacilityId { get; set; }

        /// <summary>
        /// ??? what for ???
        /// </summary>
        [Column("course_id")]
        public int? CourseId { get; set; }

        /// <summary>
        /// ???
        /// </summary>
        [Column("image")]
        public string? Image { get; set; }

        /// <summary>
        /// the classroom's description
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }

        /// <summary>
        /// the classroom's title
        /// </summary>
        [Column("title")]
        public string? Title { get; set; }

        /// <summary>
        /// the classroom's subtitle
        /// </summary>
        [Column("subtitle")]
        public string? Subtitle { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public List<Course> Courses { get; set; }
    }
}
