using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Relations
{
    [Table("adresslocation")]
    public class RelClassroomAddress : BaseClassRelation
    {
        // foreign keys are not built in entity model builder (not needed for our purpose)

        [Column("adressId")]
        public int AddressId { get; set; }

        [Column("locationId")]
        public int LocationId { get; set; }
    }
}