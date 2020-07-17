using System;
using System.Collections.Generic;
using System.Text;
using Data.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models.BaseClasses;

namespace Data.Models
{
    [Table("notebook")]
    public class Notebook : BaseClassMaterial
    {
       
        /// <summary>
        /// notebook's serial number
        /// </summary>
        [Column("serial_number", TypeName = "varchar(50)")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// notebook's Brand
        /// </summary>
        [Column("make", TypeName = "varchar(50)")]
        public string Make { get; set; }

        /// <summary>
        /// notebook's model
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

    }
}
