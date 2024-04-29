using System.Text.Json.Serialization;

namespace Rainfall.API.Application.Station.Models
{
    /// <summary>
    ///  Details of a rainfall reading
    /// </summary>
    public class RainfallReading
    {
        /// <summary>
        /// date measured
        /// </summary>
        public string DateMeasured { get; set; }

        /// <summary>
        /// measured value
        /// </summary>
        public float AmountMeasured { get; set; }
    }
}
