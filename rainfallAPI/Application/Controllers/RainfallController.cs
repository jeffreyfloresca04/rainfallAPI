using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rainfall.API.Application.Common;
using Rainfall.API.Application.Station.Commands;
using Rainfall.API.Application.Station.Models;
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
        /// <returns> Retrieve the latest readings for the specified stationId</returns>
        [HttpGet("/rainfall/id/{stationId}/readings", Name ="get-rainfall")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetStationReading([FromRoute] string stationId, [FromQuery] int count = 10)
        {
            try
            {
                var response = await _mediator.Send(new RainfallReadingCommand()
                {
                    StationId = stationId,
                    Count = count
                });

                if (response != null && response.Readings.Any())
                    return Ok(response);

                return BadRequest();
            }
            catch(ValidationException vex)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = vex.Message,
                    Detail = vex.Errors.Select(x => new ErrorDetail(x.PropertyName, x.ErrorMessage)).ToList()
                });
            }
            catch(CustomException cex)
            {
                
                switch (cex.ErrorCode)
                {
                    case StatusCodes.Status500InternalServerError:
                        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                        {
                            Message = cex.Message
                        });
                    case StatusCodes.Status400BadRequest:
                        return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse
                        {
                            Message = cex.Message
                        });

                    case StatusCodes.Status404NotFound:
                        return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                        {
                            Message = cex.Message
                        });
                }
                return BadRequest(cex);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = ex.Message
                });
            }
        }
    }
}
