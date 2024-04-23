using System.Text.Json.Serialization;

namespace Rainfall.API.Application.Station.Models
{
    public class StationReadingModel
    {
        public string dateMeasured { get; set; }
        public float amountMeasured { get; set; }
    }
}
