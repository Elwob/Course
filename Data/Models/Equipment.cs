using Data.Models.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("equipment")]
    public class Equipment : BaseClassMaterial
    {
        /// <summary>
        /// type of the equipment like "Headphone" or "Keyboard"
        /// </summary>
        [Column("type", TypeName = "varchar(50)")]
        public string Type { get; set; }

        /// <summary>
        /// equipment's Brand
        /// </summary>
        [Column("make", TypeName = "varchar(50)")]
        public string Make { get; set; }

        /// <summary>
        /// equipment's model
        /// </summary>
        [Column("model", TypeName = "varchar(50)")]
        public string Model { get; set; }

        /// <summary>
        /// room where the notebook is in
        /// </summary>
        [Column("location_id")]
        public int? LocationId { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Classroom Location { get; set; }

        /// <summary>
        /// person which uses the notebook for a period of time
        /// </summary>
        [Column("person_id")]
        public int? PersonId { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Person Person { get; set; }

        /// <summary>
        /// the quantity for a material
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }
    }
}