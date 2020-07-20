namespace Data.Models.JSONModels
{
    public class JSONTrainer
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

        public JSONTrainer(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}