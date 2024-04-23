using Rainfall.API.Application.Station.Models;

namespace Rainfall.API.Application.Station.Commands
{

    public class BaseResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<string> ValidationErrors { get; set; }

        public BaseResponse()
        {
            Success = true;
        }
    }

    public class ReadingByStationIdCommandResponse : BaseResponse
    {
        public IList<StationReadingModel> readings { get; set; }
    }
}
