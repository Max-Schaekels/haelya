using Haelya.Infrastructure;
using Haelya.Shared.Mappers;
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
builder.Services.AddScoped<ITokenService, TokenService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSettings);
var secret = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ClockSkew = TimeSpan.Zero // optionnel, réduit le délai de tolérance
        };
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
