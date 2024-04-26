using FluentValidation;

namespace Rainfall.API.Application.Station.Commands
{
    public class RainfallReadingCommandValidator : AbstractValidator<RainfallReadingCommand>
    {
        public RainfallReadingCommandValidator()
        {
            RuleFor(v => v.StationId)
                 .NotNull();

            RuleFor(v => v.Count)
                .InclusiveBetween(1, 100);
        }
    }
}
