using Data.Models;
using Data.Models.JSONModels;
using Logic.RelationControllers;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    internal class RelCourseContentController : MainRelController<RelCourseContent>
    {
        /// <summary>
        /// creates relations between courses and contents
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="content"></param>
        public void CreateRelation(int courseId, JSONContentReceive content)
        {
            CreateRel(courseId, content.Id, content.Units, "CourseId", "ContentId", "Units");
        }

        // TODO: could also use UpdateRels in MainRelController 
        //(but is a bit different than the other classes using that method --> would need an adjustment of UpdateRels)
        /// <summary>
        /// Updates relations between courses and contents
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="contents"></param>
        public void UpdateRelations(int courseId, List<JSONContentReceive> contents)
        {
            // add not already existing relations
            var courseRels = entities.RelCourseContents.Where(x => x.CourseId == courseId).ToList();
            foreach (var content in contents)
            {
                if (!courseRels.Any(x => x.ContentId == content.Id))
                {
                    entities.RelCourseContents.Add(new RelCourseContent() { CourseId = courseId, ContentId = content.Id, Units = content.Units });
                    entities.SaveChanges();
                }
            }
            // change units
            var contentIds = new List<int>();
            foreach (var cont in contents)
            {
                contentIds.Add(cont.Id);
            }
            foreach (var courseRel in courseRels)
            {
                if (courseRels.Any(x => x.ContentId == courseRel.ContentId))
                {
                    if (contentIds.Contains(courseRel.ContentId) && courseRel.Units != contents.FirstOrDefault(x => x.Id == courseRel.ContentId).Units)
                    {
                        courseRel.Units = contents.FirstOrDefault(x => x.Id == courseRel.ContentId).Units;
                        entities.SaveChanges();
                    }
                }
            }
            // delete relations
            foreach (var courseRel in courseRels)
            {
                if (!contentIds.Contains(courseRel.ContentId))
                {
                    entities.RelCourseContents.Remove(courseRel);
                    entities.SaveChanges();
                }
            }
        }

        /// <summary>
        /// creates a list of JSONContentSends for a specific course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<JSONContentSend> CreateContentArr(int courseId)
        {
            var jsonContents = new List<JSONContentSend>();
            // get all course-content relations where a certain course exists
            var relations = entities.RelCourseContents.Where(x => x.CourseId == courseId).ToList();
            // filter contents for existing course-content relations
            var c = entities.Contents.ToList();
            var contents = c.Where(x => relations.Any(z => x.Id == z.ContentId)).ToList();
            // convert to JSONContentSend
            foreach (var content in contents)
            {
                // get correct units (in a specific course, not estimation)
                var units = relations.Where(x => x.CourseId == courseId && x.ContentId == content.Id).FirstOrDefault().Units;
                // create and add jContent
                jsonContents.Add(new JSONContentSend(content.Id, content.Topic, content.Description, units));
            }
            return jsonContents;
        }
    }
}