using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
    }
}
