using Rainfall.API.Application.Station.Commands;

namespace Rainfall.API.Application.Station.Models
{
    /// <summary>
    ///   Details of a rainfall reading
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<ErrorDetail> Detail { get; set; }
    }

}
