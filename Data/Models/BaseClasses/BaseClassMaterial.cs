using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.BaseClasses
{
    public class BaseClassMaterial
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

    }
}
