using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Selenium.Api.Interfaces;
using Selenium.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// var remoteDriver = new RemoteWebDriver(new Uri("http://localhost:4444"), new ChromeOptions());
var remoteDriver = new ChromeDriver();
builder.Services.AddSingleton<IWebDriver>(remoteDriver);

builder.Services.AddSingleton<IGifService, GifService>();
builder.Services.AddSingleton<IBrowserScreenshotService, BrowserScreenshotService>();

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
