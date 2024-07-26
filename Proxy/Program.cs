var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "ClientAllowed",
        policy =>
        {
            policy.WithOrigins("https://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("ClientAllowed");

app.MapReverseProxy();

app.Run();