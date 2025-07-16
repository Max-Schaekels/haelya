using Haelya.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using Haelya.Application.DTOs.User;
using Haelya.Application.Validators.User;
using Haelya.Application.Interfaces;
using Haelya.Application.Services;
using Haelya.Infrastructure.Logging.Security;
using Haelya.Api.Middlewares;
using Haelya.Domain.Interfaces;
using Haelya.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Haelya.Shared.Settings;
using Haelya.Application.Mappers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//FluentValidation
builder.Services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();
builder.Services.AddScoped<IValidator<RegisterDTO>, RegisterDTOValidator>();

//AutoMapper
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile(new UserMappingProfile());
    //add other line for more profiles
});

//Services 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISecurityLogger, DummySecurityLogger>();
builder.Services.AddScoped<ITokenService, TokenService>();


//DbContext
builder.Services.AddDbContext<HaelyaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Haelya API", Version = "v1" });

    // Définition du schéma JWT Bearer
    options.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Entrez 'Bearer {token}' dans le champ ci-dessous."
    });

    // Application du schéma globalement
    options.AddSecurityRequirement(new()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSettings);


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
            ClockSkew = TimeSpan.Zero // optionnel, réduit le délai de tolérance
        };
    });

//Cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins( "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
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
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
