using Data.Models;

namespace Logic
{/// <summary>
/// Adds & Updates Trainers in a Course
/// </summary>
    internal class RelCourseTrainerController : MainController
    {
        public void CreateRelation(int courseId, int trainerId)
        {
            entities.RelCourseTrainers.Add(new RelCourseTrainer() { CourseId = courseId, TrainerId = trainerId });
            entities.SaveChanges();
        }
    
    }
}