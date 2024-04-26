using MediatR;

namespace Rainfall.API.Application.Station.Commands
{
    public class RainfallReadingCommand : IRequest<RainfallReadingResponse>
    {
        public int Count { get; set; }
        public string? StationId { get; set; }
    }
}
