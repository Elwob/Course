using Data.Models;
using Logic.RelationControllers;
using System.Collections.Generic;
using System.Linq;

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
            UpdateRels(courseId, subventionIds, "CourseId", "SubventionId");
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
