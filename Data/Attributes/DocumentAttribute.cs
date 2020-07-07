using System;

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