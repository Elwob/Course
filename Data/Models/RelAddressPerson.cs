using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("addressperson")]
    public class RelAddressPerson
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("addressId")]
        public int AdressId { get; set; }

        [Column("personId")]
        public int PersonId { get; set; }
    }
}