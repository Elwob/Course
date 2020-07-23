using Data.Entities;
using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// base class for all controllers (contains entities)
    /// </summary>
    public class MainController
    {
        public string documentMainPath = "\\\\LAPTOP-HM9V9LIQ\\courseRest\\Documents";
        public string templateMainPath = "\\\\LAPTOP-HM9V9LIQ\\courseRest\\DcvDokumente";
        public CourseEntities entities = CourseEntities.GetInstance();

        /// <summary>
        /// a generic method for converting enums to a list of strings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>enums as list of strings</returns>
        public List<string> GetEnums<T>()
        {
            List<string> enumsList = new List<string>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                string itemString = item.ToString();
                enumsList.Add(itemString);
            }
            return enumsList;
        }
    }
}