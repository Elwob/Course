using Data.Models;
using Logic.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// contains logic for handling Subventions
    /// </summary>
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
        /// inserts a subvention in DB and returns created subvention (including auto-generated id)
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
        /// updates a subvention in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recSub"></param>
        public Subvention PutSubvention(int id, Subvention recSub)
        {
            var putSubvention = entities.Subventions.Where(x => x.Id == id).FirstOrDefault();
            if (putSubvention != null)
            {
                putSubvention.Name = recSub.Name;
                putSubvention.Percentage = recSub.Percentage;
                putSubvention.Amount = recSub.Amount;
                entities.SaveChanges();
                return entities.Subventions.Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                throw new EntryCouldNotBeFoundException("Could not find subvention in database");
            }
        }

        /// <summary>
        /// deletes a subvention in DB
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSubvention(int id)
        {
            if (entities.Subventions.FirstOrDefault(x => x.Id == id) != null)
            {
                entities.Subventions.Remove(entities.Subventions.Single(x => x.Id == id));
                entities.SaveChanges();
            }
            else
            {
                throw new EntryCouldNotBeFoundException("Could not find subvention in database");
            }
        }
    }
}