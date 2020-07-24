using System;

namespace Data.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// this Method tries to parse a string value of a class and returns the Value if possible
        /// for Example the string enumVal == "0" and the class is EFunction, then Trainer_Intern would be returned
        /// not used at the moment
        /// what if enumVal is null? ... throws Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static T GetEnumValue<T>(this string enumVal) where T : Enum
        {
            Enum.TryParse(typeof(T), enumVal, out object myVal);

            return (T)myVal;
        }
    }
}