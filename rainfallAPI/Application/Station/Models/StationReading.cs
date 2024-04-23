namespace Rainfall.API.Application.Station.Models
{
    public class StationReading
    {
        public IList<StationReadingItem> items { get; set; }
    }

    public class StationReadingItem
    {
        public string dateTime { get; set; }
        public float value { get; set; }
    }
}
