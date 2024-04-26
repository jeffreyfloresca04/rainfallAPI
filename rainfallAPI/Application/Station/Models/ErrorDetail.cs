namespace Rainfall.API.Application.Station.Models
{
    /// <summary>
    ///  Details of invalid request property
    /// </summary>
    public class ErrorDetail
    {
        public string message { get; private set; }
        public string propertyName { get; private set; }

        public ErrorDetail(string propertyName, string message)
        {
            this.message = message;
            this.propertyName = propertyName;
        }
    }
}
