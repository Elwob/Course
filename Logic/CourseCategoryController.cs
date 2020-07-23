using Data.Entities;
using Data.Models;
using Logic.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// contains logic for handling CourseCategories
    /// </summary>
    public class CourseCategoryController
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        /// <summary>
        /// gets all CourseCategories in DB
        /// </summary>
        /// <returns>a list of CourseCategories</returns>
        public List<CourseCategory> GetAllCategories()
        {
            return entities.CourseCategories.ToList();
        }

        /// <summary>
        /// creates a new CourseCategory
        /// </summary>
        /// <param name="courseCategory"></param>
        /// <returns>the created CourseCategory</returns>
        public CourseCategory PostCategory(CourseCategory courseCategory)
        {
            entities.CourseCategories.Add(courseCategory);
            entities.SaveChanges();
            return courseCategory;
        }

        /// <summary>
        /// updates a certain CourseCategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseCategory"></param>
        /// <returns>the updated CourseCategory</returns>
        public CourseCategory UpdateCategory(int id, CourseCategory courseCategory)
        {
            var putCat = entities.CourseCategories.FirstOrDefault(x => x.Id == id);
            if (putCat != null)
            {
                putCat.Name = courseCategory.Name;
                putCat.Color = courseCategory.Color;
                putCat.FontColor = courseCategory.FontColor;
                entities.SaveChanges();
                return putCat;
            }
            else
            {
                throw new EntryCouldNotBeFoundException("the course category you want to change could not be found");
            }
        }

        /// <summary>
        /// deletes a certain CourseCategory
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int id)
        {
            if (entities.CourseCategories.FirstOrDefault(x => x.Id == id) != null)
            {
                entities.CourseCategories.Remove(entities.CourseCategories.Single(x => x.Id == id));
                entities.SaveChanges();
            }
            else
            {
                throw new EntryCouldNotBeFoundException("the course category you want to delete could not be found");
            }
        }
    }
}