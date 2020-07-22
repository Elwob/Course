using Data.Models;
using Data.Models.JSONModels;
using Logic.RelationControllers;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Adds & Updates Trainers in a Course
    /// </summary>
    internal class RelCourseTrainerController : MainRelController<RelCourseTrainer>
    {
        /// <summary>
        ///  creates relations between courses and trainers
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="objId"></param>
        public void CreateRelation(int courseId, int objId)
        {
            CreateRel(courseId, objId, null, "CourseId", "TrainerId", null);
        }

        /// <summary>
        /// Updates relations between courses and trainers
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="trainerIds"></param>
        public void UpdateRelations(int courseId, List<int> trainerIds)
        {
            UpdateRels(courseId, trainerIds, "CourseId", "TrainerId");
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
            // TODO: change to enums / function "0" and "1" are trainers 
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