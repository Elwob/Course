using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Data.Models
{
    /// <summary>
    /// different classes a document can belong to
    /// </summary>

    /// that Enums will be shown correctly in JSON
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EClass
    {
        [EnumMember(Value = "Person")]
        Person,
        [EnumMember(Value = "Course")]
        Course
    }
}