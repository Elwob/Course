using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Relations
{
    /// <summary>
    /// intermediate data used to create relations between addresses and locations
    /// </summary>
    [Table("adresslocation")]
    public class RelClassroomAddress
    {
        // foreign keys are not built in entity model builder (not needed for our purpose)

        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        [Column("adressId")]
        public int AddressId { get; set; }

        [Column("locationId")]
        public int LocationId { get; set; }
    }
}