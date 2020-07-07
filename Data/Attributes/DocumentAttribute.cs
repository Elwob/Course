using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Attributes
{
    public class DocumentAttribute : Attribute
    {
        public string ClassName { get; set; }
        public DocumentAttribute(Type entityType)
        {
            ClassName = entityType.Name;
        }
    }
}
