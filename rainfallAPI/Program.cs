using System.Net.NetworkInformation;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Rainfall.API.Application.Common;
using Rainfall.API.Application.Station.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(ReadingByStationIdCommand).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddHttpClient();

//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers(options => {
    // options
})
  .AddJsonOptions(options => {
  });



builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rainfall API",
        Version = "1.0",
        Contact = new OpenApiContact
        {
            Name = "Sorted",
            Url = new Uri("https://wwww.sorted.com")
        },
        Description = "An API which provides rainfall reading data",
    });

    options.AddServer(new OpenApiServer()
    {
        Url = "http://localhost:3000",
        Description = "Rainfall Api"
    });


    // https://stackoverflow.com/questions/62424769/using-swashbuckle-5-x-specify-nullable-true-on-a-generic-t-parameter-reference
    options.UseAllOfToExtendReferenceSchemas();

    foreach (var file in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)), "*.xml"))
    {
        options.IncludeXmlComments(file, true);
    }
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.UseSwagger();


app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rainfall API");
});

app.UseReDoc(options => {
    options.DocumentTitle = "Rainfall API";
    options.SpecUrl = "/swagger/v1/swagger.json";
});


app.Run();
