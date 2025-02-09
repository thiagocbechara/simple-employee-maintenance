using Microsoft.OpenApi.Models;
using SimpleEmployeeMaintenance.Api.MapperProfiles;
using SimpleEmployeeMaintenance.Domain.Extensions;
using SimpleEmployeeMaintenance.Infra.Database.Extensions;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Simple Employee Maintenance  API",
        Description = "A simple API for Employee Maintenance"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddDomainServices();
builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ApiProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
