using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("addressperson")]
    public class RelAddressPerson
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("addressId")]
        public int AddressId { get; set; }

        [NotMapped]
        public Person Person { get; set; }

        [NotMapped]
        public Address Address { get; set; }

        [Column("personId")]
        public int PersonId { get; set; }
    }
}