using Data.Entities;
using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class CourseCategoryController
    {
        CourseEntities entities = CourseEntities.GetInstance();

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
            putCat.Name = courseCategory.Name;
            putCat.Color = courseCategory.Color;
            putCat.FontColor = courseCategory.FontColor;
            entities.SaveChanges();
            return putCat;
        }

        public void DeleteCategory(int id)
        {
            entities.CourseCategories.Remove(entities.CourseCategories.Single(x => x.Id == id));
            entities.SaveChanges();
        }
    }
}
