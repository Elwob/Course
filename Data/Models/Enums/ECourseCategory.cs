using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Data.Models
{
    /// <summary>
    /// different kinds of course categories
    /// </summary>

    //needs to be converted to DB-solution

    /// that Enums will be shown correctly in JSON
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ECourseCategory
    {
        [EnumMember(Value = "Coding Campus")]
        CodingCampus,
        [EnumMember(Value = "Digital Marketing Academy")]
        DigitalMarketingAcademy,
        [EnumMember(Value = "E-Learning Diploma")]
        ELearningDiploma,
        [EnumMember(Value = "Digital School")]
        DigitalSchool,
        [EnumMember(Value = "Skills Initiative")]
        SkillsInitiative,
        [EnumMember(Value = "Digital Masterclass")]
        DigitalMasterclass,
        [EnumMember(Value = "Digital Studies")]
        DigitalStudies,
        [EnumMember(Value = "Digital Business")]
        DigitalBusiness
    }
}