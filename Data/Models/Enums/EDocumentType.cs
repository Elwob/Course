using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Data.Models
{
    /// <summary>
    /// different kinds of document types
    /// </summary>

    /// that Enums will be shown correctly in JSON
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EDocumentType
    {
        [EnumMember(Value = "Invitation")]
        Invitation,

        [EnumMember(Value = "Registration Confirmation")]
        RegistrationConfirmation,

        [EnumMember(Value = "Bill")]
        Bill,

        [EnumMember(Value = "Dun")]
        Dun,

        [EnumMember(Value = "Diploma")]
        Diploma,

        [EnumMember(Value = "Information")]
        Information,

        [EnumMember(Value = "Note")]
        Note,

        [EnumMember(Value = "Other")]
        Other,

        [EnumMember(Value = "Mail")]
        Mail
    }
}