using Hl7.App.Security;
using Hl7.App.Services;
using Hl7.App.Utilities;
using Hl7.DAL;
using Hl7.DAL.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
const string CONNECTION_STRING_NAME = "Hl7"; 
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISuiEncoder, SuiEncoder>();
builder.Services.AddSingleton<IMdmDecoder, MdmDecoder>();
builder.Services.AddSingleton<IFileLogger, FileLogger>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddDbContext<Hl7DbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString(CONNECTION_STRING_NAME)));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(!app.Environment.IsDevelopment())
    app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
