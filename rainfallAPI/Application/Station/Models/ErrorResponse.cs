using Rainfall.API.Application.Station.Commands;

namespace Rainfall.API.Application.Station.Models
{
    /// <summary>
    ///   Details of a rainfall reading
    /// </summary>
    public class ErrorResponse : BaseError
    {
        public ErrorResponse(string message, List<ErrorDetail> errorDetails) : base(message) {
            this.Detail = errorDetails;
        }

        public ErrorResponse(string message) : base(message)
        {
        }


        public List<ErrorDetail> Detail { get; private set; }
    }

}
