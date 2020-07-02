using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table ("course")]
    public class Course
    {
        [Column ("id")]
        public int Id { get; set; }
        [Column ("title", TypeName = "varchar(250)")]
        public string Title { get; set; }
        [Column ("course_number", TypeName = "varchar(15)")]
        public string CourseNumber { get; set; }
        [Column ("description")]
        public string Description { get; set; }
        [Column ("category", TypeName = "varchar(100)")]
        public ECourseCategory Category { get; set; }
        [Column ("start")]
        public DateTime? Start { get; set; }
        [Column ("end")]
        public DateTime? End { get; set; }
        [Column ("unit")]
        public int Unit { get; set; }
        [Column ("price")]
        public double Price { get; set; }
        [Column ("classroom_id")]
        public int ClassroomID { get; set; }
        [Column("participant_max")]
        public int MaxParticipants { get; set; }
        [Column("participant_min")]
        public int MinParticipants { get; set; }
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }
        [NotMapped]
        public List<RelCourseContent> CourseContents { get; set; }
        public List<RelCourseSubvention> CourseSubventions { get; set; }
    }

   
}
