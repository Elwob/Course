using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models.JSONModels
{
    public class JSONContent
    {
        /// <summary>
        /// the main topic of a certain teaching content
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// a short description of the teaching content
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// an estimation of the used units
        /// </summary>
        public int? UnitEstimation { get; set; }
    }
}
