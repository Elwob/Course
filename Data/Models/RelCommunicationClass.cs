using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("communication_class")]
    public class RelCommunicationClass
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("communication_id")]
        public int CommunicationId { get; set; }
        [Column("class", TypeName = ("varchar(200)"))]
        public string Class { get; set; }
        [Column("class_id")]
        public int ClassId { get; set; }
        
    }
}
