using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// a address (for students, trainers, supplier, etc.)
    /// </summary>
    [Table("address")]
    public class Address
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// name of the street
        /// </summary>
        [Column("street", TypeName = "varchar(200)")]
        public string? Street { get; set; }
        /// <summary>
        /// name of the town/city
        /// </summary>
        [Column("place", TypeName = "varchar(200)")]
        public string? Place { get; set; }
        /// <summary>
        /// zip code
        /// </summary>
        [Column("zip")]
        public int? Zip { get; set; }
        /// <summary>
        /// name of the country
        /// </summary>
        [Column("country", TypeName = "varchar(200)")]
        public string? Country { get; set; }
        /// <summary>
        /// contains the ContactType
        /// </summary>
        [Column("contact_type", TypeName = "varchar(200)")]
        public string? ContactType { get; set; }
        /// <summary>
        /// says if address is a billing address or not
        /// </summary>
        [Column("billing_address")]
        public bool? BillingAddress { get; set; }
        /// <summary>
        /// date when the address was created
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// date when the address was modified
        /// </summary>
        [Column("modify@")]
        public DateTime? ModifiedAt { get; set; }
    }
}