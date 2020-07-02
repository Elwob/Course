using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table ("subvention")]
    public class Subvention
    {
        [Column ("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(250)")]
        public int Name { get; set; }
        [Column("percentage")]
        public double Percentage { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
    }
}
