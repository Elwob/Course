using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// intermediate data used to create relations between persons and addresses
    /// </summary>
    [Table("addressperson")]
    public class RelAddressPerson
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the addresses' id
        /// </summary>
        [Column("addressId")]
        public int AddressId { get; set; }

        /// <summary>
        /// the persons' id
        /// </summary>
        [Column("personId")]
        public int PersonId { get; set; }

        /// <summary>
        /// /Privat or Buissness contact
        /// </summary>
        [Column("contact_type")]
        public int ContactType { get; set; }

        /// <summary>
        /// Billing Adress
        /// </summary>
        [Column("billing_address")]
        public int? BillingAddress { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Person Person { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Address Address { get; set; }
    }
}