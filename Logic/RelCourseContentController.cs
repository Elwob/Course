﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class RelCourseContentController : MainController
    {
        public void CreateRelation(int courseId, Content content)
        {
            entities.RelCourseContents.Add(new RelCourseContent() { CourseId = courseId, ContentId = content.Id, UnitEstimation = content.UnitEstimation });
            entities.SaveChanges();
        }
    }
}