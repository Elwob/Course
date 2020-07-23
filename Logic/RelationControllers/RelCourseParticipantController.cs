using Data.Entities;
using Data.Models;

namespace Logic
{
    /// <summary>
    ///  Adds & Updates relations between courses and participants
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
    }
}