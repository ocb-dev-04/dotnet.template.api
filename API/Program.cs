using Serilog;

using API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

builder.Services.AddAllCustomServices(builder.Configuration);

builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(Configuration).WriteTo.Console());

builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
