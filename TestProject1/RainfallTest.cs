using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rainfall.API.Application.Common;
using Rainfall.API.Application.Station.Commands;

namespace TestProject1
{

    public class RainfallTest
    {

        [Fact]
        public async Task ShouldReturnNonEmptyReadingResponse_WhenStationIdExists()
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                                    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RainfallReadingCommand).Assembly))
                                    .AddHttpClient().
                                    AddTransient<IConfiguration>(sp =>
                                    {
                                        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                                        configurationBuilder.AddJsonFile("appsettings.json");
                                        return configurationBuilder.Build();
                                    })
                                    .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var query = new RainfallReadingCommand()
                {
                    StationId = "5000",
                    Count = 10
                };

            var response = await mediator.Send(query);

            Assert.True(response.Readings.Count > 0);;
            Assert.True(response.Readings[0].AmountMeasured == 4.154f);
            Assert.True(response.Readings[0].DateMeasured == "2024-04-25T14:30:00Z");
        }


        [Fact]
        public async Task ShouldReturn404NoReadings_WhenStationIdExists()
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                                    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RainfallReadingCommand).Assembly))
                                    .AddHttpClient().
                                    AddTransient<IConfiguration>(sp =>
                                    {
                                        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                                        configurationBuilder.AddJsonFile("appsettings.json");
                                        return configurationBuilder.Build();
                                    })
                                    .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var query = new RainfallReadingCommand()
            {
                StationId = "500000",
                Count = 10
            };

            var ex = await Assert.ThrowsAsync<CustomException>(() => mediator.Send(query));

            Assert.True(ex.ErrorCode == 404);
        }
    }
}