using Application.Interfaces.Repository;
using Application.Mappers;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Aplication.Interfaces.Repository;
using Infrastructure.Security;
using Application.Interfaces.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TurnosYa.Api.Services;
using Application.UseCases.Security;
using Application.Interfaces.UseCases.Appointments;
using Application.UseCases.Appointments;
using Application.Interfaces.UseCases.AvailabilitySlots;
using Application.UseCases.AvailabilitySlots;
using Application.Interfaces.UseCases.Customers;
using Application.UseCases.Customers;
using Application.Interfaces.UseCases.Notifications;
using Application.UseCases.Notifications;
using Application.Interfaces.UseCases.Professionals;
using Application.UseCases.Professionals;
using Application.Interfaces.Users;
using Application.UseCases.User;
using Application.Interfaces.UseCases.Users;
using Application.UseCases.Users;
using Application.Interfaces.Services;
using Microsoft.OpenApi.Models;
using Application.Interfaces.UseCases.ExternalCustomers;
using Application.UseCases.ExternalCustomers;

var builder = WebApplication.CreateBuilder(args);

// ESTO PERMITE USAR JWT EN SWAGGER, NO NECESARIO EN PRODUCCION
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TurnosYa API", Version = "v1" });

    // 🔐 Configurar soporte para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en este formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Configuracion de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL del frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Leer config JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");

// Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Inyeccion de dependencia del hasher
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<JwtService>();




builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataConection")));


builder.Services.AddScoped<ITokenGenerator, JwtService>();

// Inyeccion de dependencias de repositorios
builder.Services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IUserActivityRepository, UserActivityRepository>();
builder.Services.AddScoped<IExternalCustomerRepository, ExternalCustomerRepository>();

// Inyeccion de dependencias de UseCases de Appointments
builder.Services.AddScoped<ICancelExpiredPendingAppointments, CancelExpiredPendingAppointments>();
builder.Services.AddScoped<IConfirmAppointment, ConfirmAppointment>();
builder.Services.AddScoped<ICreateAppointment, CreateAppointment>();
builder.Services.AddScoped<IGetAppointmentById, GetAppointmentById>();
builder.Services.AddScoped<IGetAppointmentsByProfessionalId, GetAppointmentsByProfessionalId>();
builder.Services.AddScoped<IUpdateAppointment, UpdateAppointment>();

// Inyeccion de dependencias de UseCases de AvailabilitySlots
builder.Services.AddScoped<ICreateAvailabilitySlot, CreateAvailabilitySlot>();
builder.Services.AddScoped<IDeleteAvailabilitySlot, DeleteAvailabilitySlot>();
builder.Services.AddScoped<IUpdateAvailabilitySlot, UpdateAvailabilitySlot>();

// Inyeccion de dependencias de UseCases de Customers
builder.Services.AddScoped<ICreateCustomer, CreateCustomer>();
builder.Services.AddScoped<IDeleteCustomer, DeleteCustomer>();
builder.Services.AddScoped<IGetCustomerByEmail, GetCustomerByEmail>();
builder.Services.AddScoped<IGetCustomerById, GetCustomerById>();
builder.Services.AddScoped<IGetCustomerByIdInternal, GetCustomerByIdInternal>();
builder.Services.AddScoped<IUpdateCustomer, UpdateCustomer>();

// Inyeccion de dependencias de UseCases de Notifications
builder.Services.AddScoped<ICreateNotification, CreateNotification>();

// Inyeccion de dependencias de UseCases de Professionals
builder.Services.AddScoped<ICreateProfessional, CreateProfessional>();
builder.Services.AddScoped<IDeleteProfessional, DeleteProfessional>();
builder.Services.AddScoped<IGetProfessionalByEmail, GetProfessionalByEmail>();
builder.Services.AddScoped<IGetProfessionalByEmailInternal, GetProfessionalByEmailInternal>();
builder.Services.AddScoped<IGetProfessionalById, GetProfessionalById>();
builder.Services.AddScoped<IGetProfessionalCustomers, GetProfessionalCustomers>();
builder.Services.AddScoped<IUpdateProfessional, UpdateProfessional>();

// Inyeccion de dependencias de UseCases de Security
builder.Services.AddScoped<ILoginHandler, LoginHandler>();

// Inyeccion de dependencias de UseCases de Users
builder.Services.AddScoped<ICreateUserActivity, CreateUserActivity>();
builder.Services.AddScoped<IGetAllUserActivities, GetAllUserActivities>();
builder.Services.AddScoped<IGetUserByEmail, GetUserByEmail>();
builder.Services.AddScoped<IValidateUserEmail, ValidateUserEmail>();

// Inyeccion de dependencias de UseCases de ExternalCustomers
builder.Services.AddScoped<IGetExternalCustomerByEmail, GetExternalCustomerByEmail>();
builder.Services.AddScoped<IGetExternalCustomerByPhone, GetExternalCustomerByPhone>();
builder.Services.AddScoped<IGetExternalCustomersByProfessionalId, GetExternalCustomersByProfessionalId>();
builder.Services.AddScoped<ICreateExternalCustomer, CreateExternalCustomer>();


builder.Services.AddAutoMapper(typeof(ProfessionalProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AvailabilityProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CustomerProfile).Assembly);
builder.Services.AddAutoMapper(typeof(NotificationProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AppointmentProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UserActivityProfile).Assembly);


var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
