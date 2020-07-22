using Data.Entities;
using Data.Models;
using Logic.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class CourseCategoryController
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        public List<CourseCategory> GetAllCategories()
        {
            return entities.CourseCategories.ToList();
        }

        public CourseCategory PostCategory(CourseCategory courseCategory)
        {
            entities.CourseCategories.Add(courseCategory);
            entities.SaveChanges();
            return courseCategory;
        }

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