using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
  public  class Course_Participants
    {
        public int id { get; set; }
        public int course_id{ get; set; }

        public Course Course { get; set; }
        public int participant_id { get; set; }


    }
}






