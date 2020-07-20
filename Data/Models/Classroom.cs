using Data.Models.Relations;
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
        /// says if relations should be displayed when generating jsons
        /// </summary>
        public static bool ShouldIgnoreRelation = false;

        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// name of the room
        /// </summary>
        [Column("room")]
        public string? Room { get; set; }

        /// <summary>
        /// ???
        /// </summary>
        [Column("facility_id")]
        public int? FacilityId { get; set; }

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

        private List<RelCourseClassroom> _classroomCourses;

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public List<RelCourseClassroom> ClassroomCourses
        {
            get
            {
                if (ShouldIgnoreRelation)
                {
                    return new List<RelCourseClassroom>();
                }
                else
                {
                    return _classroomCourses;
                }
            }
            set
            {
                _classroomCourses = value;
            }
        }
    }
}