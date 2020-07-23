using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.BaseClasses
{
    /// <summary>
    /// applies to all classes that map different kind of materials
    /// </summary>
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