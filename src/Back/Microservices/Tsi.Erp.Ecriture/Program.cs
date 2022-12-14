using Tsi.Erp.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.BuildMicroserviceDependencies<Program>();

var app = builder.Build();

app.CreateMicroservicePipeline();

app.Run();