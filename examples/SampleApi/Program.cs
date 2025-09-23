using Andy.Guard;
using Andy.Guard.Scanning;
using Andy.Guard.Scanning.InputScanners;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register PromptInjectionScanner as a singleton
builder.Services.AddSingleton<IInputScanner, PromptInjectionScanner>();

// Register OutputScannerRegistry and OutputInjectionScanner
builder.Services.AddSingleton<IOutputScannerRegistry, Andy.Guard.Scanning.OutputScannerRegistry>();
builder.Services.AddSingleton<IOutputScanner, Andy.Guard.Scanning.OutputScanners.OutputInjectionScanner>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
