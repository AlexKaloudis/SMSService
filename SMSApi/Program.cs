using Microsoft.EntityFrameworkCore;
using SMSApi.Data;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy;
using SMSApi.SMS.Strategy.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<SMSDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));
builder.Services.AddScoped<ISMSRepository,SMSRepository>();
builder.Services.AddScoped<ISMSVendorContext, SMSVendorContext>();
Console.WriteLine("info " + builder.Configuration.GetConnectionString("MSSQLConnection"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
