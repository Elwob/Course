using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("course_content")]
    public class RelCourseContent
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("content_id")]
        public int ContentId { get; set; }

        [Column("units")]
        public int? UnitEstimation { get; set; }

        [NotMapped]
        public Course Course { get; set; }

        [NotMapped]
        public Content Content { get; set; }
    }
}