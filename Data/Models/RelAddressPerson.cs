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
        public int AdressId { get; set; }
        /// <summary>
        /// the persons' id
        /// </summary>
        [Column("personId")]
        public int PersonId { get; set; }
    }
}