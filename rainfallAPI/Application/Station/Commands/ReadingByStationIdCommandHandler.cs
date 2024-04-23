using MediatR;
using Rainfall.API.Application.Common;
using Rainfall.API.Application.Station.Models;
using System.Net;
using System.Text.Json;

namespace Rainfall.API.Application.Station.Commands
{
    public class ReadingByStationIdCommandHandler : IRequestHandler<ReadingByStationIdCommand, ReadingByStationIdCommandResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public ReadingByStationIdCommandHandler(
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
        public async Task<ReadingByStationIdCommandResponse> Handle(ReadingByStationIdCommand request, CancellationToken cancellationToken)
        {
            var rainfallUrl = _config.GetSection("MicroServiceConfig:RainfallEndpoint").Value;
            var readings = new List<StationReadingModel>();
         
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    using var httpResponseMessage = await httpClient.GetAsync($"{rainfallUrl}/id/stations/{request.StationId}/readings?_sorted&_limit={request.Count}");

                    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        throw new CustomException((int)System.Net.HttpStatusCode.InternalServerError);
                    }
                    else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new CustomException((int)System.Net.HttpStatusCode.BadRequest);
                    }
                    else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new CustomException((int)System.Net.HttpStatusCode.NotFound);
                    }
                    else
                    {
                        var result = await httpResponseMessage.Content.ReadFromJsonAsync<StationReading>();

                        if (result != null && result.items.Any())
                        {
                            readings = result.items.Select(x => new StationReadingModel
                            {
                                dateMeasured = x.dateTime,
                                amountMeasured = x.value
                            }).ToList();
                        }else if(result != null && !result.items.Any())
                        {
                            throw new CustomException((int)System.Net.HttpStatusCode.NotFound);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return new ReadingByStationIdCommandResponse
            {
                readings = readings
            };
        }
    }
}
