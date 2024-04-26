using Rainfall.API.Application.Station.Models;
using System.Text.Json.Serialization;

namespace Rainfall.API.Application.Station.Commands
{
    /// <summary>
    /// Get rainfall readings response
    /// </summary>
    public class RainfallReadingResponse
    {
        public IList<RainfallReading> Readings { get; set; }
    }

}
