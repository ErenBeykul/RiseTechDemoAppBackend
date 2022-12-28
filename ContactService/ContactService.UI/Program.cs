using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ContactService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar();

builder.Host.ConfigureContainer<ServiceRegistry>(services =>
{
    services.Scan(x =>
    {
        x.TheCallingAssembly();
        x.WithDefaultConventions();
        x.Assembly("ContactService.Service");
        x.Assembly("ContactService.DataAccess");
    });
});

// Add services to the container.

builder.Services.AddDbContext<RiseTechDemoAppContactContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RiseTechDemoAppContactContext"))
);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
                  .AllowAnyHeader();
        });
});

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
