using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between courses and teaching contents
    /// </summary>
    [Table("course_content")]
    public class RelCourseContent : BaseClassCourseRelation
    {
        /// <summary>
        /// the contents' id
        /// </summary>
        [Column("content_id")]
        public int ContentId { get; set; }

        /// <summary>
        /// the teaching units needed for the content in that particular course
        /// </summary>
        [Column("units")]
        public int? Units { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Course Course { get; set; }

        /// <summary>
        /// needed for creating link
        /// </summary>
        [NotMapped]
        public Content Content { get; set; }
    }
}