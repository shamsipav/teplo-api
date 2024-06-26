using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Models.Validators;
using TeploAPI.Repositories;
using TeploAPI.Services;
using TeploAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs\\log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TeploDBContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("TeploAPIConnection")));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TeploAPI",
        Version = "v1",
        Description = "Master dissertation. Web API documentation",
        Contact = new OpenApiContact()
        {
            Name = "Pavel Shamsimukhametov",
            Email = "shamsipav@gmail.com",
            Url = new Uri("https://t.me/shamsipav")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
            new string[] { }
        }
    });
    
    // Включаем XML-комментарии
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Репозитории
builder.Services.AddScoped<IRepository<User>, MainRepository<User>>();
builder.Services.AddScoped<IRepository<FurnaceBaseParam>, MainRepository<FurnaceBaseParam>>();
builder.Services.AddScoped<IRepository<Furnace>, MainRepository<Furnace>>();
builder.Services.AddScoped<IRepository<CokeCunsumptionReference>, MainRepository<CokeCunsumptionReference>>();
builder.Services.AddScoped<IRepository<FurnaceCapacityReference>, MainRepository<FurnaceCapacityReference>>();
builder.Services.AddScoped<IRepository<Material>, MainRepository<Material>>();

// Сервисы
builder.Services.AddScoped<IFurnaceService, FurnaceService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IProjectPeriodService, ProjectPeriodService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReferenceCoefficientsService, ReferenceService>();
builder.Services.AddScoped<IFurnaceWorkParamsService, FurnaceWorkParamsService>();
builder.Services.AddScoped<IBasePeriodService, BasePeriodService>();
builder.Services.AddScoped<ICalculateService, CalculateService>();

// Валидаторы
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Furnace>, FurnaceValidator>();
builder.Services.AddScoped<IValidator<Material>, MaterialValidator>();

// Lowercase routing -> /api/Auth... => /api/auth..
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

var origins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();

app.UseCors(builder => builder
    .WithOrigins(origins)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Starting application with {environment} mode\n");

app.Run();