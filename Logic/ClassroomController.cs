using Data.Entities;
using Data.Models.JSONModels;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class ClassroomController
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        public List<JSONClassroom> GetRooms()
        {
            var rooms = new List<JSONClassroom>();
            foreach (var room in entities.Classrooms.ToList())
            {
                var jId = room.Id;
                var jRoom = room.Room;
                var addressId = entities.RelClassroomAddresses.Where(x => x.LocationId == room.Id).DefaultIfEmpty().FirstOrDefault().AddressId;
                var jPlace = entities.Addresses.Where(x => x.Id == addressId).DefaultIfEmpty().FirstOrDefault().Place;
                rooms.Add(new JSONClassroom(jId, jRoom, jPlace));
            }
            return rooms;
        }
    }
}