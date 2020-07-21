using Data.Models;
using DocumentFormat.OpenXml.Bibliography;
using iText.StyledXmlParser.Jsoup.Nodes;
using Logic.RelationControllers;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    internal class RelCourseSubventionController : MainRelController<RelCourseSubvention>
    {
        /// <summary>
        ///  creates relations between courses and subventions
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="objId"></param>
        public void CreateRelation(int courseId, int objId)
        {
            CreateRel(courseId, objId, null, "CourseId", "SubventionId", null);
        }

        public void UpdateRelations(int courseId, List<int> subventionIds)
        {
            // add not already existing relations
            var courseRels = entities.RelCourseSubventions.Where(x => x.CourseId == courseId).ToList();
            foreach (var subventionId in subventionIds)
            {
                if (!courseRels.Any(x => x.SubventionId == subventionId))
                {
                    entities.RelCourseSubventions.Add(new RelCourseSubvention() { CourseId = courseId, SubventionId = subventionId });
                    entities.SaveChanges();
                }
            }
            // delete relations
            foreach (var courseRel in courseRels)
            {
                if (!subventionIds.Contains(courseRel.SubventionId))
                {
                    entities.RelCourseSubventions.Remove(courseRel);
                    entities.SaveChanges();
                }
            }
        }

        /// <summary>
        /// creates a list of all subventions of a certain course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<Subvention> CreateSubventionArr(int courseId)
        {
            var relations = entities.RelCourseSubventions.Where(x => x.CourseId == courseId).ToList();
            var s = entities.Subventions.ToList();
            return s.Where(x => relations.Any(z => x.Id == z.SubventionId)).ToList();
        }
    }
}
