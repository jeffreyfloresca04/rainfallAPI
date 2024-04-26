using MediatR;
using Rainfall.API.Application.Common;
using Rainfall.API.Application.Station.Models;
using System.Net;
using System.Text.Json;

namespace Rainfall.API.Application.Station.Commands
{
    public class RainfallReadingCommandHandler : IRequestHandler<RainfallReadingCommand, RainfallReadingResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public RainfallReadingCommandHandler(
                IHttpClientFactory httpClientFactory,
                 IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        /// <summary>
        /// Handle the command request to retrieve data
        /// </summary>
        /// <param name="request">contains request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task<RainfallReadingResponse> Handle(RainfallReadingCommand request, CancellationToken cancellationToken)
        {
            var rainfallUrl = _config.GetSection("MicroServiceConfig:RainfallEndpoint").Value;
            var readings = new List<RainfallReading>();
         
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    using var response = await httpClient.GetAsync($"{rainfallUrl}/id/stations/{request.StationId}/readings?_sorted&_limit={request.Count}");

                    if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        throw new CustomException((int)System.Net.HttpStatusCode.InternalServerError, response.ReasonPhrase ?? "Internal Server Error");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new CustomException((int)System.Net.HttpStatusCode.BadRequest, response.ReasonPhrase ?? "Bad Request");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new CustomException((int)System.Net.HttpStatusCode.NotFound, response.ReasonPhrase ?? "Invalid Url");
                    }
                    else
                    {
                        var result = await response.Content.ReadFromJsonAsync<StationReadingModel>();

                        if (result != null && result.Items.Any())
                        {
                            readings = result.Items.Select(x => new RainfallReading
                            {
                                DateMeasured = x.Datetime,
                                AmountMeasured = x.Value
                            }).ToList();
                        }else if(result != null && !result.Items.Any())
                        {
                            throw new CustomException((int)System.Net.HttpStatusCode.NotFound, "Record  not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return new RainfallReadingResponse
            {
                Readings = readings
            };
        }
    }
}
