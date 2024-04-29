namespace Rainfall.API.Application.Station.Models
{
    /// <summary>
    /// Rainfall reading list
    /// </summary>
    public class StationReadingModel
    {
        public IList<StationReadingItem> Items { get; set; }
    }

    /// <summary>
    /// Reading reading content
    /// </summary>
    public class StationReadingItem
    {
        public string Datetime { get; set; }
        public float Value { get; set; }
    }
}
