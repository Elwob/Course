using Data.Models.BaseClasses;
using Data.Models.Relations;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Logic.RelationControllers
{
    public class MainRelController<T> : MainController where T : BaseClassCourseRelation
    {
        /// <summary>
        /// builds a relation between two classes
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

        //Todo: RelCourseContent could also be implemented here
        /// <summary>
        /// updates relations between two classes
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="relIds"></param>
        /// <param name="IdName1"></param>
        /// <param name="RelIdsName"></param>
        public void UpdateRels(int id1, List<int> relIds, string IdName1, string RelIdsName)
        {
            var relType = typeof(T);
            // select belonging entity
            var property = entities.GetType().GetProperty(typeof(T).Name + "s");
            var relEntity = property.GetValue(entities, null) as DbSet<T>;
            // get entries wih certain courseId
            var rels = relEntity.Where(x => x.CourseId == id1).ToList();
            // add not already existing relations
            foreach (var relObjId in relIds)
            {
                if(!rels.Any(x => (int)x.GetType().GetProperty(RelIdsName).GetValue(x) == relObjId))
                {
                    // create instance and set values
                    var rel = (T)Activator.CreateInstance(relType);
                    rel.GetType().GetProperty(IdName1).SetValue(rel, id1);
                    rel.GetType().GetProperty(RelIdsName).SetValue(rel, relObjId);
                    relEntity.Add(rel);
                    entities.SaveChanges();
                }
            }
            // delete relations
            foreach (var rel in rels)
            {
                if (!relIds.Contains((int)rel.GetType().GetProperty(RelIdsName).GetValue(rel)))
                {
                    relEntity.Remove(rel);
                    entities.SaveChanges();
                }
            }
        }
    }
}
