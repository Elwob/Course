using Data.Models.BaseClasses;
using Data.Models.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Logic.RelationControllers
{
    public class MainRelController<T> : MainController where T : BaseClassRelation
    {
        /// <summary>
        /// builds a generic relation between two classes
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="IdName1"></param>
        /// <param name="IdName2"></param>
        public void CreateRel(int id1, int id2, int? value, string IdName1, string IdName2, string valueName)
        {
            // create instance of T and set values
            var relType = typeof(T);
            var rel = (T)Activator.CreateInstance(relType);
            rel.GetType().GetProperty(IdName1).SetValue(rel, id1);
            rel.GetType().GetProperty(IdName2).SetValue(rel, id2);
            if (valueName != null && value != null)
            {
                rel.GetType().GetProperty(valueName).SetValue(rel, value);
            }
            // select belonging entity and add instance
            var property = entities.GetType().GetProperty(typeof(T).Name + "s");
            var relEntity = property.GetValue(entities, null) as DbSet<T>;
            relEntity.Add(rel);
            entities.SaveChanges();
        }
    }
}
