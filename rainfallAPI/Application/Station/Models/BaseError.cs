namespace Rainfall.API.Application.Station.Models
{

    /// <summary>
    /// Base Error
    /// </summary>
    public class BaseError
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string message { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">error message</param>
        public BaseError(string message)
        {
            this.message = message;
        }
    }
}
