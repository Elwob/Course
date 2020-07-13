using System;
using System.Collections.Generic;
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

        ///// <summary>
        ///// contains the ContactType (not used in DB anymore)
        ///// </summary>
        //[Column("contact_type", TypeName = "varchar(200)")]
        //public string? ContactType { get; set; }

        ///// <summary>
        ///// says if address is a billing address or not (not used in DB anymore)
        ///// </summary>
        //[Column("billing_address")]
        //public bool? BillingAddress { get; set; }

        /// <summary>
        /// date when the address was created
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// date when the address was modified
        /// </summary>
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }

        [NotMapped]
        public List<RelAddressPerson> AddressPersons { get; set; }
    }
}