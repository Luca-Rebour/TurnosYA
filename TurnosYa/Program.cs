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
using Application.Interfaces.UseCases.Clients;
using Application.UseCases.Clients;
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
using Application.Interfaces.UseCases.ExternalClients;
using Application.UseCases.ExternalClients;
using Api.Middlewares;
using Application.UseCases.ExternalAppointments;

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
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IInternalAppointmentRepository, InternalAppointmentRepository>();
builder.Services.AddScoped<IExternalAppointmentRepository, ExternalAppointmentRepository>();
builder.Services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IUserActivityRepository, UserActivityRepository>();
builder.Services.AddScoped<IExternalClientRepository, ExternalClientRepository>();

// Inyeccion de dependencias de UseCases de Appointments
builder.Services.AddScoped<ICancelExpiredPendingInternalAppointments, CancelExpiredPendingInternalAppointments>();
builder.Services.AddScoped<IConfirmInternalAppointment, ConfirmInternalAppointment>();
builder.Services.AddScoped<ICreateInternalAppointment, CreateInternalAppointment>();
builder.Services.AddScoped<IGetInternalAppointmentById, GetInternalAppointmentById>();
builder.Services.AddScoped<IGetInternalAppointmentsByProfessionalId, GetInternalAppointmentsByProfessionalId>();
builder.Services.AddScoped<IUpdateInternalAppointment, UpdateInternalAppointment>();

// Inyeccion de dependencias de UseCases de AvailabilitySlots
builder.Services.AddScoped<ICreateAvailabilitySlot, CreateAvailabilitySlot>();
builder.Services.AddScoped<IDeleteAvailabilitySlot, DeleteAvailabilitySlot>();
builder.Services.AddScoped<IUpdateAvailabilitySlot, UpdateAvailabilitySlot>();
builder.Services.AddScoped<IGetAllAvailabilitySlots, GetAllAvailabilitySlots>();
builder.Services.AddScoped<IGetAllAvailabilitySlotsByProfessionalAndDay, GetAllAvailabilitySlotsByProfessionalAndDay>();


// Inyeccion de dependencias de UseCases de Clients
builder.Services.AddScoped<ICreateClient, CreateClient>();
builder.Services.AddScoped<IDeleteClient, DeleteClient>();
builder.Services.AddScoped<IGetClientByEmail, GetClientByEmail>();
builder.Services.AddScoped<IGetClientById, GetClientById>();
builder.Services.AddScoped<IGetClientByIdInternal, GetClientByIdInternal>();
builder.Services.AddScoped<IUpdateClient, UpdateClient>();

// Inyeccion de dependencias de UseCases de Notifications
builder.Services.AddScoped<ICreateNotification, CreateNotification>();

// Inyeccion de dependencias de UseCases de Professionals
builder.Services.AddScoped<ICreateProfessional, CreateProfessional>();
builder.Services.AddScoped<IDeleteProfessional, DeleteProfessional>();
builder.Services.AddScoped<IGetProfessionalByEmail, GetProfessionalByEmail>();
builder.Services.AddScoped<IGetProfessionalByEmailInternal, GetProfessionalByEmailInternal>();
builder.Services.AddScoped<IGetProfessionalById, GetProfessionalById>();
builder.Services.AddScoped<IGetProfessionalClients, GetProfessionalClients>();
builder.Services.AddScoped<IUpdateProfessional, UpdateProfessional>();
builder.Services.AddScoped<IGetProfessionalSummary, GetProfessionalSummary>();

// Inyeccion de dependencias de UseCases de Security
builder.Services.AddScoped<ILoginHandler, LoginHandler>();

// Inyeccion de dependencias de UseCases de Users
builder.Services.AddScoped<ICreateUserActivity, CreateUserActivity>();
builder.Services.AddScoped<IGetAllUserActivities, GetAllUserActivities>();
builder.Services.AddScoped<IGetUserByEmail, GetUserByEmail>();
builder.Services.AddScoped<IValidateUserEmail, ValidateUserEmail>();

// Inyeccion de dependencias de UseCases de ExternalAppointments
builder.Services.AddScoped<IConfirmAppointment, ConfirmAppointment>();
builder.Services.AddScoped<IGetAppointmentById, GetAppointmentById>();

// Inyeccion de dependencias de UseCases de ExternalClients
builder.Services.AddScoped<IGetExternalClientByEmail, GetExternalClientByEmail>();
builder.Services.AddScoped<IGetExternalClientByPhone, GetExternalClientByPhone>();
builder.Services.AddScoped<IGetExternalClientsByProfessionalId, GetExternalClientsByProfessionalId>();
builder.Services.AddScoped<ICreateExternalClient, CreateExternalClient>();

// Inyeccion de dependencias de UseCases de ExternalAppointments
builder.Services.AddScoped<IGetExternalAppointmentById, GetExternalAppointmentById>();
builder.Services.AddScoped<IGetExternalAppointmentsByProfessionalId, GetExternalAppointmentsByProfessionalId>();
builder.Services.AddScoped<ICancelExpiredPendingExternalAppointments, CancelExpiredPendingExternalAppointments>();
builder.Services.AddScoped<IConfirmExternalAppointment, ConfirmExternalAppointment>();
builder.Services.AddScoped<ICreateExternalAppointment, CreateExternalAppointment>();
builder.Services.AddScoped<IUpdateExternalAppointment, UpdateExternalAppointment>();


builder.Services.AddAutoMapper(typeof(ProfessionalProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AvailabilityProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ClientProfile).Assembly);
builder.Services.AddAutoMapper(typeof(NotificationProfile).Assembly);
builder.Services.AddAutoMapper(typeof(InternalAppointmentProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UserActivityProfile).Assembly);


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

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
