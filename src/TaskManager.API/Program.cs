using TaskManager.API.Extensions;
using TaskManager.API.Middleware;
using TaskManager.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddAllServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddOpenApi();
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "TaskManager API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
