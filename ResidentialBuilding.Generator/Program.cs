using Generator.Generator;
using Generator.Service;
using ResidentialBuilding.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisDistributedCache("residential-building-cache");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalDev", policy =>
    {
        policy
            .AllowAnyOrigin()
            .WithHeaders("Content-Type")
            .WithMethods("GET");
    });
});

builder.Services.AddSingleton<ResidentialBuildingGenerator>();
builder.Services.AddSingleton<IResidentialBuildingService,  ResidentialBuildingService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowLocalDev");

app.MapControllers();

app.Run();