using Data.Models;
using Data.Models.JSONModels;
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

        /// <summary>
        /// creates a list of JSONTrainers for a specific course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<JSONTrainer> CreateTrainerArr(int courseId)
        {
            var jsonTrainers = new List<JSONTrainer>();
            // get all belonging trainers
            var relations = entities.RelCourseTrainers.Where(x => x.CourseId == courseId).ToList();
            // function 0 and 1 are trainers TODO: change to enums
            var t = entities.Persons.Where(x => x.Function == "0" || x.Function == "1").ToList();
            var trainers = t.Where(x => relations.Any(z => x.Id == z.TrainerId)).ToList();
            //Convert to JSONTrainer
            foreach (var trainer in trainers)
            {
                jsonTrainers.Add(new JSONTrainer(trainer.Id, trainer.FirstName, trainer.LastName));
            }
            return jsonTrainers;
        }
    }
}