namespace Data.Models.JSONModels
{
    /// <summary>
    /// JSON format for Classrooms (only contains data necessary for frontend)
    /// </summary>
    public class JSONClassroom
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ???
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// name of the location
        /// </summary>
        public string Place { get; set; }

        public JSONClassroom(int id, string room, string place)
        {
            Id = id;
            Room = room;
            Place = place;
        }
    }
}