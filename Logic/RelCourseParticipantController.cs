using Data.Entities;
using Data.Models;

namespace Logic
{
    /// <summary>
    ///  Adds & Updates Persons to a Course
    /// </summary>
    internal class RelCourseParticipantController
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        public RelCourseParticipant Post(RelCourseParticipant relCourseParticipant)
        {
            entities.RelCourseParticipants.Add(relCourseParticipant);
            entities.SaveChanges();
            return relCourseParticipant;
        }
        public RelCourseParticipant Delete(RelCourseParticipant relCourseParticipant)
        {
            entities.RelCourseParticipants.Remove(relCourseParticipant);
            entities.SaveChanges();
            return relCourseParticipant;
        }

    }
}