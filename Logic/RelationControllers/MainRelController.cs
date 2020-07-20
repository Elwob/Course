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
    //TODO: Finish or delete
    public class MainRelController<T> : MainController where T : BaseClassRelation
    {
        public void CreateRel(int Id1, int Id2, string IdName1, string IdName2)
        {
            var type = typeof(T);
            var rel = Activator.CreateInstance(type) as BaseClassRelation;

            type.GetProperty(IdName1).SetValue(rel, Id1);
            type.GetProperty(IdName2).SetValue(rel, Id2);

            var property = entities.GetType().GetProperty(typeof(T).Name + "s");
            var relEntity = property.GetValue(entities, null) as DbSet<T>;
            relEntity.Add((T)rel);
            entities.SaveChanges();

            //// get assembly name
            //Type type = typeof(System.Data.DataSet);
            //string assemblyName = type.Assembly.FullName.ToString();
            //// get Name of relation entity
            //string tStr = typeof(T).Name + "s";
            //// 
            //Type t = Type.GetType("Logic." + tStr + ", " + assemblyName);

            //var property = entities.GetType().GetProperty(typeof(T).Name + "s");
            //var relEntity = (property.GetValue(entities, null) as DbSet<T>);
            //Type type = typeof(T);

            //entities.RelCourseClassrooms.Add(new RelCourseClassroom() { CourseId = courseId, ClassroomId = classroomId });
            //entities.SaveChanges();
        }
    }
}
