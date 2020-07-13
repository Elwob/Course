using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.JSONModels
{
    public class JSONContentSend
    {
        /// <summary>
        /// the contents' id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// the contents' topic
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// the contents' description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// the contents' true units (not the estimation)
        /// </summary>
        public int? Units { get; set; }

        public JSONContentSend(int id, string topic, string description, int? units)
        {
            Id = id;
            Topic = topic;
            Description = description;
            Units = units;
        }
    }
}
