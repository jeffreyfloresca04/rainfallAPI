using System.Text.Json.Serialization;

namespace Rainfall.API.Application.Station.Models
{
    /// <summary>
    ///  Details of a rainfall reading
    /// </summary>
    public class RainfallReading
    {
        public string DateMeasured { get; set; }
        public float AmountMeasured { get; set; }
    }
}
