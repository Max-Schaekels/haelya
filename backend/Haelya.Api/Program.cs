using AutoMapper;
using FluentValidation;
using Haelya.Api.Middlewares;
using Haelya.Application.DTOs.Category;
using Haelya.Application.DTOs.Product;
using Haelya.Application.DTOs.User;
using Haelya.Application.Interfaces;
using Haelya.Application.Interfaces.Auth;
using Haelya.Application.Mappers;
using Haelya.Application.Services;
using Haelya.Application.Services.Auth;
using Haelya.Application.Validators.Category;
using Haelya.Application.Validators.Product;
using Haelya.Application.Validators.User;
using Haelya.Domain.Interfaces;
using Haelya.Infrastructure;
using Haelya.Infrastructure.Logging.Security;
using Haelya.Infrastructure.Repositories;
using Haelya.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//FluentValidation

//User
builder.Services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();
builder.Services.AddScoped<IValidator<RegisterDTO>, RegisterDTOValidator>();

//Category
builder.Services.AddScoped<IValidator<CategoryCreateDTO>, CategoryCreateDTOValidator>();
builder.Services.AddScoped<IValidator<CategoryUpdateDTO>, CategoryUpdateDTOValidator>();

//Product
builder.Services.AddScoped<IValidator<ProductCreateDTO>, ProductCreateDTOValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<ProductUpdatePriceDTO>, ProductUpdatePriceDTOValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateMarginDTO>, ProductUpdateMarginDTOValidator>();


//AutoMapper
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile(new UserMappingProfile());
    cfg.AddProfile(new CategoryMappingProfile());
    cfg.AddProfile(new BrandMappingProfile());
    cfg.AddProfile(new ProductMappingProfile());
    cfg.AddProfile(new CommonMappingProfile());

    //add other line for more profiles
});

//Services 

//Brand
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();

//Product 
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Category
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Refresh Token
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

//User
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Dummy logger 
builder.Services.AddScoped<ISecurityLogger, DummySecurityLogger>();

//Token
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
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Cookies["AccessToken"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

//Cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:4000")
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
