using Data.Models;
using Data.Models.Enums;
using Data.Models.JSONModels;
using Logic.RelationControllers;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Adds & Updates relations between Courses and Trainers
    /// </summary>
    internal class RelCourseTrainerController : MainRelController<RelCourseTrainer>
    {
        /// <summary>
        ///  creates relations between Courses and Trainers
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="objId"></param>
        public void CreateRelation(int courseId, int objId)
        {
            CreateRel(courseId, objId, null, "CourseId", "TrainerId", null);
        }

        /// <summary>
        /// Updates relations between Courses and Trainers
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="trainerIds"></param>
        public void UpdateRelations(int courseId, List<int> trainerIds)
        {
            UpdateRels(courseId, trainerIds, "CourseId", "TrainerId");
        }

        /// <summary>
        /// creates a list of JSONTrainers for a specific Course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<JSONTrainer> CreateTrainerArr(int courseId)
        {
            var jsonTrainers = new List<JSONTrainer>();
            // get all belonging trainers
            var relations = entities.RelCourseTrainers.Where(x => x.CourseId == courseId).ToList();
            var t = entities.Persons.Where(x => x.Function.Equals(EFunction.Trainer_Intern) || x.Function.Equals(EFunction.Trainer_Extern)).ToList();
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