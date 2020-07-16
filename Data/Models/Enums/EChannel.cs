using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Data.Models
{
    /// <summary>
    /// different communication channels
    /// </summary>
    
    /// that Enums will be shown correctly in JSON

    public enum EChannel
    {
        [EnumMember(Value = "Phone Call")]
        PhoneCall,
        [EnumMember(Value = "Email")]
        Email,
        [EnumMember(Value = "Personal")]
        Personal,
        [EnumMember(Value = "Letter")]
        Letter,
        [EnumMember(Value = "Fax")]
        Fax,
        [EnumMember(Value = "Smoke Signs")]
        SmokeSigns
    }
}