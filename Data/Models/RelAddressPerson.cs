using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table ("addressperson")]
    public class RelAddressPerson
    {
        [Column ("id")]
        public int Id { get; set; }
        [Column("addressId")]
        public int AdressId { get; set; }
        [Column("personId")]
        public int PersonId { get; set; }
    }
}
