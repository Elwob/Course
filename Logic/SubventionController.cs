using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class SubventionController : MainController
    {
        /// <summary>
        /// returns all Contents from DB
        /// </summary>
        /// <returns></returns>
        public List<Subvention> GetAllSubventions()
        {
            return entities.Subventions.ToList();
        }

        /// <summary>
        /// inserts a Subvention in DB and returns created Subvention (including auto-generated id)
        /// </summary>
        /// <param name="recSub"></param>
        /// <returns></returns>
        public Subvention PostSubvention(Subvention recSub)
        {
            entities.Subventions.Add(new Subvention(recSub.Name, recSub.Percentage, recSub.Amount));
            entities.SaveChanges();
            return entities.Subventions.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        /// <summary>
        /// updates a Subvention in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recSub"></param>
        public Subvention PutSubvention(int id, Subvention recSub)
        {
            var putSubvention = entities.Subventions.Where(x => x.Id == id).FirstOrDefault();
            putSubvention.Name = recSub.Name;
            putSubvention.Percentage = recSub.Percentage;
            putSubvention.Amount = recSub.Amount;
            entities.SaveChanges();
            return entities.Subventions.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// deletes a Subvention in DB
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSubvention(int id)
        {
            entities.Subventions.Remove(entities.Subventions.Single(x => x.Id == id));
            entities.SaveChanges();
        }
    }
}