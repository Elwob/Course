using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Attributes
{
    public class CommunicationAttribute : Attribute
    {
        public string ClassName { get; set; }

        public CommunicationAttribute(Type entityType)
        {
            ClassName = entityType.Name;
        }
    }
}
