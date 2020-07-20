using System.Collections.Generic;

namespace Data.Models.JSONModels
{
    public class CourseFilter
    {
        /// <summary>
        /// filters planned / active / completed courses
        /// </summary>
        public List<string> status { get; set; }

        /// <summary>
        /// filters courses of a specific trainer
        /// </summary>
        public int? trainer_id { get; set; }

        /// <summary>
        /// filters course.Name for a string
        /// </summary>
        public string search { get; set; }

        /// <summary>
        /// filters a certain course.Category
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// filters
        /// </summary>
        public int? content_id { get; set; }
    }
}