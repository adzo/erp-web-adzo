

using Tsi.Erp.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.BuildMicroserviceDependencies<ApplicationContext>(builder.Configuration);

var app = builder.Build();

app.CreateMicroservicePipeline(); 

app.Run();