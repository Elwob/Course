using Data.Entities;
using Data.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class MainController

    {
        public string documentMainPath = "\\\\LAPTOP-HM9V9LIQ\\courseRest\\Documents";
        public string templateMainPath = "\\\\LAPTOP-HM9V9LIQ\\courseRest\\DcvDokumente";
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