using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("content")]
    public class Content
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("topic")]
        public string Topic { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("unit_estimation")]
        public int UnitEstimation { get; set; }
    }
}
