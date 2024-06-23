using AwesomeShop.Services.Customers.Application;
using AwesomeShop.Services.Customers.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using AwesomeShop.Services.Customers.Application.Commands.Handlers;
using AwesomeShop.Services.Customers.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddConsulConfig(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(opt =>{
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddSubscribers();
builder.Services.AddMongo();
builder.Services.AddRabbitMq();
builder.Services.AddRepositories();
builder.Services.AddMappers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddCustomerHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCustomerById).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title= "AwesomeShop.Services.Customers.API", Version = "v1" }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AwesomeShop.Services.Customers.API v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

//app.UseConsul();

app.MapControllers();

app.Run();
