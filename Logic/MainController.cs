using Data.Entities;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class MainController
    {
        public CourseEntities entities = CourseEntities.GetInstance();

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