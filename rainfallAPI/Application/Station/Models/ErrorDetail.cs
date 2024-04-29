namespace Rainfall.API.Application.Station.Models
{
    /// <summary>
    ///  Details of invalid request property
    /// </summary>
    public class ErrorDetail : BaseError
    {
        /// <summary>
        /// Name of propery
        /// </summary>
        public string propertyName { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="message"></param>
        public ErrorDetail(string propertyName, string message) : base(message)
        {
            this.propertyName = propertyName;
        }
    }

}
