using System;

namespace Data.Models
{
    public class Absence

    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int participant_id { get; set; }
        public int course_id { get; set; }
        public int units { get; set; }
        public Boolean excused { get; set; }
        public int document_id { get; set; }
        public Boolean completed { get; set; }
        public int reminder_id { get; set; }
        public string comment { get; set; }
    }
}