using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Adds & Updates Trainers in a Course
    /// </summary>
    internal class RelCourseTrainerController : MainController
    {
        /// <summary>
        /// creates relations between courses and trainers
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="trainerId"></param>
        public void CreateRelation(int courseId, int trainerId)
        {
            entities.RelCourseTrainers.Add(new RelCourseTrainer() { CourseId = courseId, TrainerId = trainerId});
            entities.SaveChanges();
        }

        /// <summary>
        /// Updates relations between courses and trainers
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="trainerIds"></param>
        public void UpdateRelations(int courseId, List<int> trainerIds)
        {
            // add not already existing relations
            var courseRels = entities.RelCourseTrainers.Where(x => x.CourseId == courseId).ToList();
            foreach (var trainerId in trainerIds)
            {
                if (!courseRels.Any(x => x.TrainerId == trainerId))
                {
                    entities.RelCourseTrainers.Add(new RelCourseTrainer() { CourseId = courseId, TrainerId = trainerId });
                    entities.SaveChanges();
                }
            }
            // delete relations
            foreach (var courseRel in courseRels)
            {
                if (!trainerIds.Contains(courseRel.TrainerId))
                {
                    entities.RelCourseTrainers.Remove(courseRel);
                    entities.SaveChanges();
                }
            }
        }
    }
}