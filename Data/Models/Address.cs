using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("address")]
    public class Address
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("street", TypeName = "varchar(200)")]
        public string Street { get; set; }

        [Column("place", TypeName = "varchar(200)")]
        public string Place { get; set; }

        [Column("zip")]
        public int Zip { get; set; }

        [Column("country", TypeName = "varchar(200)")]
        public string Country { get; set; }

        [Column("contact_type", TypeName = "varchar(200)")]
        public string ContactType { get; set; }

        [Column("billing_address")]
        public bool BillingAddress { get; set; }

        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        [Column("modify@")]
        public DateTime ModifiedAt { get; set; }
    }
}