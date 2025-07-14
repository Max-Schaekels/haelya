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
builder.Services.AddScoped<ISecurityLogger, DummySecurityLogger>();

//DbContext
builder.Services.AddDbContext<HaelyaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
