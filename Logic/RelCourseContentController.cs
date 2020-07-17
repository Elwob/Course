using Data.Models;
using Data.Models.JSONModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Logic
{
    internal class RelCourseContentController : MainController
    {
        /// <summary>
        /// creates relations between courses and contents
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="content"></param>
        public void CreateRelation(int courseId, JSONContentReceive content)
        {
            entities.RelCourseContents.Add(new RelCourseContent() { CourseId = courseId, ContentId = content.Id, Units = content.Units });
            entities.SaveChanges();
        }

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
                if (contentIds.Contains(courseRel.ContentId) && courseRel.Units != contents.FirstOrDefault(x => x.Id == courseRel.ContentId).Units);
                {
                    courseRel.Units = contents.FirstOrDefault(x => x.Id == courseRel.ContentId).Units;
                    entities.SaveChanges();
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
    }
}
