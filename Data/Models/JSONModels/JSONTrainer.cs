using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.JSONModels
{
    class JSONTrainer
    {
        /// <summary>
        /// the trainers' id in DB
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// the trainers' first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// the trainers' last name
        /// </summary>
        public string LastName { get; set; }
    }
}
