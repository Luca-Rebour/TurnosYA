using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.UseCases;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Aplication.Interfaces.Repository;
using Infrastructure.Security;
using Application.Interfaces.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyeccion de dependencia del hasher
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataConection")));

// Inyeccion de dependencias de repositorios
builder.Services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Inyeccion de dependencias de servicios
builder.Services.AddScoped<IProfessionalService, ProfessionalService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAvailabilitySlotService, AvailabilitySlotService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddAutoMapper(typeof(ProfessionalProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
