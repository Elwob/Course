using Data.Models.JSONModels;
using Data.Models.Relations;
using Logic.RelationControllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Adds & Updates Classrooms in a Course
    /// </summary>
    public class RelCourseClassroomController : MainRelController<RelCourseClassroom>
    {
        /// <summary>
        ///  creates relations between courses and classrooms
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="objId"></param>
        public void CreateRelation(int courseId, int objId)
        {
            CreateRel(courseId, objId, null, "CourseId", "ClassroomId", null);
        }

        /// <summary>
        /// Updates relations between courses and classrooms
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classroomIds"></param>
        public void UpdateRelations(int courseId, List<int> classroomIds)
        {
            UpdateRels(courseId, classroomIds, "CourseId", "ClassroomId");
        }

        /// <summary>
        /// gets all classrooms as List<JSONClassroom>
        /// </summary>
        /// <returns></returns>
        public List<JSONClassroom> GetRooms()
        {
            var rooms = new List<JSONClassroom>();
            foreach (var room in entities.Classrooms.ToList())
            {
                var jId = room.Id;
                var jRoom = room.Title;
                if (room.Title == "" || room.Title == null)
                {
                    jRoom = room.Room;
                }
                string jPlace = "unknown";
                try
                {
                    if (entities.RelClassroomAddresses.Where(x => x.LocationId == room.Id).FirstOrDefault() != null)
                    {
                        int addressId = entities.RelClassroomAddresses.Where(x => x.LocationId == room.Id).FirstOrDefault().AddressId;
                        jPlace = entities.Addresses.Where(x => x.Id == addressId).DefaultIfEmpty().FirstOrDefault().Place;
                    }
                }
                catch (NullReferenceException)
                {
                    // no logic needed here
                }
                finally
                {
                    rooms.Add(new JSONClassroom(jId, jRoom, jPlace));
                }
            }
            return rooms;
        }

        /// <summary>
        /// returns List<JSONClassroom> of a certain course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<JSONClassroom> CreateClassroomArr(int courseId)
        {
            // get all course-classroom relations where a certain course exists
            if (entities.RelCourseClassrooms.FirstOrDefault(x => x.CourseId == courseId) != null)
            {
                var relations = entities.RelCourseClassrooms.Where(x => x.CourseId == courseId).ToList();
                // filter classrooms for existing course-classroom relations
                var c = GetRooms();
                return c.Where(x => relations.Any(z => x.Id == z.ClassroomId)).ToList();
            }
            else
            {
                return new List<JSONClassroom>();
            }
        }
    }
}