using System.Reflection;
using HomeDashboard.Application.Interfaces;
using HomeDashboard.Application.Services;
using HomeDashboard.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    Console.WriteLine(xmlFilePath);
    options.IncludeXmlComments(xmlFilePath);
});

// Configure DI\
builder.Services.AddHttpClient();
builder.Services.AddScoped<IWasteBinsCollectionDateService, WasteBinsCollectionDateService>();
builder.Services.AddScoped<IWasteBinCollectionDateQueryService, UnleyCouncilOnlineWasteBinCollectionDateQueryService>();
    
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();
