namespace Data.Models.JSONModels
{
    /// <summary>
    /// maps how Contents are received
    /// </summary>
    public class JSONContentReceive
    {
        /// <summary>
        /// the contents' id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// the contents' true units (not the estimation)
        /// </summary>
        public int Units { get; set; }
    }
}