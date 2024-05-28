using GL.ApplicationServices.Implementations;
using GL.ApplicationServices.Interfaces;
using GL.Data.Contexts;
using GL.Data.Entities;
using GL.Repositories.Implementations;
using GL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .Build();

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

try
{
    Log.Logger.Information("Application is starting!");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<GameLibraryDbContext>(options => options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("GL.WebAPI")));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Game Library",
            Description = "RESTful API for managing a game library",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            },
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    // Add serilog
    builder.Services.AddSerilog();

    // Authentication
    string tokenKey = configuration["Jwt:Key"] ?? "Not working token key";
    string issuer = configuration["Jwt:Issuer"] ?? "GL.WebAPI";
    string audience = configuration["Jwt:Audience"] ?? "GL.WebAPI";

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
        };
    });
    //string tokenKey = configuration["Authentication:TokenKey"] ?? "Not working token key";
    //builder.Services.AddAuthentication(x =>
    //{
    //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //}).AddJwtBearer(x =>
    //{
    //    x.RequireHttpsMetadata = false;
    //    x.SaveToken = true;
    //    x.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuerSigningKey = true,
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)),
    //        ValidateIssuer = false,
    //        ValidateAudience = false,
    //    };
    //});

    // Start SERVICE DI
    builder.Services.AddScoped<DbContext, GameLibraryDbContext>();
    builder.Services.AddScoped<IGamesRepository, GamesRepository>();
    builder.Services.AddScoped<IGenresRepository, GenresRepository>();
    builder.Services.AddScoped<IRatingsRepository, RatingsRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IGamesManagementService, GamesManagementService>();
    builder.Services.AddScoped<IGenresManagementServic�, GenresManagementService>();
    builder.Services.AddScoped<IRatingsManagementService, RatingsManagementService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    builder.Services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(tokenKey));
    // End SERVICE DI

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception!");
}
finally
{
    await Log.CloseAndFlushAsync();
}
