using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rainfall.API.Application.Common;
using Rainfall.API.Application.Station.Commands;
using System.Net.Mime;

namespace Rainfall.API.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RainfallController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <param name="stationId">The id of the reading station</param>
        /// <param name="count">The number of readings to return</param>
        /// <returns></returns>
        [HttpGet("/rainfall/id/{stationId}/readings", Name ="station-reading")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadingByStationIdCommandResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ReadingByStationIdCommandResponse))]
        public async Task<IActionResult> StationReading([FromRoute] string stationId, [FromQuery] int count = 10)
        {
            try
            {
                var response = await _mediator.Send(new ReadingByStationIdCommand()
                {
                    StationId = stationId,
                    Count = count
                });

                if (response != null && response.Success && response.readings.Any())
                    return Ok(response);
                if (response != null && response.Success && !response.readings.Any())
                    return NotFound();

                return BadRequest();
            }
            catch(ValidationException vex)
            {
                return BadRequest(vex.Errors);
            }
            catch(CustomException ex)
            {
                switch (ex.code)
                {
                    case StatusCodes.Status500InternalServerError:
                        return StatusCode(StatusCodes.Status500InternalServerError, ex);

                    case StatusCodes.Status400BadRequest:
                        return StatusCode(StatusCodes.Status400BadRequest, ex);

                    case StatusCodes.Status404NotFound:
                        return StatusCode(StatusCodes.Status404NotFound, ex);
                }
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
