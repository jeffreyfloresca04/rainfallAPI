namespace Rainfall.API.Application.Station.Models
{
    public class StationReadingModel
    {
        public IList<StationReadingItem> Items { get; set; }
    }

    public class StationReadingItem
    {
        public string Datetime { get; set; }
        public float Value { get; set; }
    }
}
