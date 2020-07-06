using Data.Models;
using System.Linq;

namespace Logic
{
    public class AbsenceController : MainController
    {
        /// <summary>
        /// One new Absence Entry
        /// </summary>
        /// <returns>Absence</returns>
        public Absence Post(Absence absence)
        {
            entities.Absences.Add(absence);
            absence = Get(absence.ParticipantId);
            return absence;
        }

        /// <summary>
        /// Update one Absence Entry
        /// </summary>
        /// <returns>Absence</returns>
        public Absence Put(Absence absence)
        {
            entities.Absences.Add(absence);
            absence = Get(absence.ParticipantId);
            return absence;
        }

        /// <summary>
        /// //Request absence for one Person
        /// </summary>
        /// <param name="id">ParticipantId(Person)</param>
        /// <returns>Absence</returns>
        public Absence Get(int id)
        {
            var absence = entities.Absences.Where(p => p.ParticipantId == id).FirstOrDefault();
            return absence;
        }
    }
}