using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.BaseClasses
{
    public class BaseClassRelation
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
    }
}