using MediatR;

namespace Rainfall.API.Application.Station.Commands
{
    public class ReadingByStationIdCommand : IRequest<ReadingByStationIdCommandResponse>
    {
        public int Count { get; set; }
        public string? StationId { get; set; }
    }
}
