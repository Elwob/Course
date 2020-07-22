using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.Models.Enums
{
    /// <summary>
    /// Employment relationship
    /// </summary>
    /// that Enums will be shown correctly in JSON
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EFunction
    {
        [EnumMember(Value = "Trainer Intern")]
        Trainer_Intern,
        [EnumMember(Value = "Trainer Extern")]
        Trainer_Extern,
        Büro
        
    }
}
