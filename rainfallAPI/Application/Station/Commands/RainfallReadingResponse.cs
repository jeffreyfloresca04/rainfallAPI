using Rainfall.API.Application.Station.Models;
using System.Text.Json.Serialization;

namespace Rainfall.API.Application.Station.Commands
{
    /// <summary>
    /// Details of a rainfall reading
    /// </summary>
    public class RainfallReadingResponse
    {
        public IList<RainfallReading> Readings { get; set; }
    }

}
