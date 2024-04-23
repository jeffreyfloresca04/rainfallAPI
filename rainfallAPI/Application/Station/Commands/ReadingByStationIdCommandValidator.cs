using FluentValidation;

namespace Rainfall.API.Application.Station.Commands
{
    public class ReadingByStationIdCommandValidator : AbstractValidator<ReadingByStationIdCommand>
    {
        public ReadingByStationIdCommandValidator()
        {
            RuleFor(v => v.StationId)
                 .NotNull();

            RuleFor(v => v.Count)
                .InclusiveBetween(1, 100);
        }
    }
}
