using Clinic.API.Infrastructures;
using Clinic.Context;
using Microsoft.EntityFrameworkCore;
using Clinic.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x =>
{
    x.Filters.Add<ClinicExceptionFilter>();
})
    .AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContextFactory<ClinicContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddDependences();
builder.Services.AddMapper();
builder.Services.GetSwaggerDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
