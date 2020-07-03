using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("course_subvention")]
    public class RelCourseSubvention
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("subvention_id")]
        public int SubventionId { get; set; }

        [NotMapped]
        public Course Course { get; set; }

        [NotMapped]
        public Subvention Subvention { get; set; }
    }
}