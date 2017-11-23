namespace Dpic.Connect.SharedDefinitions.MovieCodes
{
    /// <summary>
    /// Represents movie codes
    /// </summary>
    public class MovieCode
    {
        /// <summary>
        /// Movie code ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Movie code displayable name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Indicates whether this code is active
        /// </summary>
        public bool Active { get; set; }
    }
}