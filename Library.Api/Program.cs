using Library.Api.Extensions;
using Library.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.Configure<LibrarySetting>(
    builder.Configuration.GetSection("LibrarySettings")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
