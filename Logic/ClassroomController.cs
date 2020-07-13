using Data.Entities;
using Data.Models;
using Data.Models.JSONModels;
using System;
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
                string jPlace = "unknown";
                try
                {
                    if (entities.RelClassroomAddresses.Where(x => x.LocationId == room.Id).FirstOrDefault() != null)
                    {
                        int addressId = entities.RelClassroomAddresses.Where(x => x.LocationId == room.Id).FirstOrDefault().AddressId;
                        jPlace = entities.Addresses.Where(x => x.Id == addressId).DefaultIfEmpty().FirstOrDefault().Place;
                    }
                    
                } catch (NullReferenceException ex)
                {
                    Console.WriteLine("Classroom doesn't have an address");
                }
                finally
                {
                    rooms.Add(new JSONClassroom(jId, jRoom, jPlace));
                }
                
            }
            return rooms;
        }

        public JSONClassroom ConvertClassroomToJSON(Classroom classroom)
        {
            var rel = entities.RelClassroomAddresses.Where(x => x.LocationId == classroom.Id).FirstOrDefault();
            string place = entities.Addresses.Where(x => x.Id == rel.AddressId).FirstOrDefault().Place;
            return new JSONClassroom(classroom.Id, classroom.Room, place);
        }
    }
}