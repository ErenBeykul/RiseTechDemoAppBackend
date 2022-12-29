using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ReportService.DataAccess;
using ReportService.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar();

builder.Host.ConfigureContainer<ServiceRegistry>(services =>
{
    services.Scan(x =>
    {
        x.TheCallingAssembly();
        x.WithDefaultConventions();
        x.Assembly("ReportService.Worker");
        x.Assembly("ReportService.Service");
        x.Assembly("ReportService.DataAccess");
    });
});

// Add services to the container.

builder.Services.AddDbContext<RiseTechDemoAppReportContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RiseTechDemoAppReportContext"))
);

builder.Services.AddHttpClient("ContactClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ContactServiceBaseAddress").Get<string>());
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(builder.Configuration.GetSection("ContactServiceAuthToken").Get<string>());
});

builder.Services.AddControllers();
builder.Services.AddHostedService<ReportHostedService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();