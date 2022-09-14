using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using FluentValidation.AspNetCore;

using Core.Models;
using Core.Helpers;
using Core.Interfaces;

using Core.CQRS;

using Data.Context;
using Data.Repositories;

namespace API.Extensions;

/// <summary>
/// <see cref="Services"/> class colletions
/// </summary>
public static class Services
{
    /// <summary>
    /// Main services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="Configuration"></param>
    public static void AddAllCustomServices(this IServiceCollection services, IConfiguration Configuration)
    {
        // some const to set services latter
        const string CoreName = "Core";

        services.AddSigletonConfiguration(Configuration);

        // then, package services
        services.AddAutoMapper(mapper => mapper.AddMaps(CoreName));

        // local services
        services.AddDI(Configuration);
        services.AddDatabases(Configuration);
        services.AddCustomSwagger();
        services.AddValidations();
    }

    /// <summary>
    /// Set all configuration as singleton service
    /// </summary>
    /// <param name="services"></param>
    /// <param name="Configuration"></param>
    public static void AddSigletonConfiguration(this IServiceCollection services, IConfiguration Configuration)
    {
        const string JwtSettings = "JwtSettings";
        const string EncryptSettings = "EncryptSettings";

        // Add Jwt Settings
        var jwtSettings = new JwtSettings();
        Configuration.Bind(JwtSettings, jwtSettings);
        services.AddSingleton(jwtSettings);

        // Add encrypt settins
        var encryptSetting = new EncryptSettings();
        Configuration.Bind(EncryptSettings, encryptSetting);
        services.AddSingleton(encryptSetting);

        services.AddJWT(Configuration, jwtSettings);
    }

    /// <summary>
    /// Validations
    /// </summary>
    /// <param name="services"></param>
    private static void AddValidations(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        })
        .ConfigureApiBehaviorOptions(configuration =>
        {
            configuration.SuppressModelStateInvalidFilter = true;
        })
        .AddFluentValidation(configuration =>
        {
            configuration.ImplicitlyValidateChildProperties = false;
            configuration.RegisterValidatorsFromAssembly(AppDomain.CurrentDomain.Load("Core"));
        });
    }

    /// <summary>
    /// DI services
    /// </summary>
    /// <param name="services"></param>
    private static void AddDI(this IServiceCollection services, IConfiguration configuration)
    {
        #region API & Core services

        services.AddScoped<IUserCommand, UserCommand>();
        services.AddScoped<IUserQueries, UserQueries>();

        // single handlers
        services.AddScoped<IAuthCommand, AuthCommand>();

        #endregion

        // general services
        services.AddScoped<IEncryptDecrypt, EncryptDecrypt>();
        services.AddScoped<IJwtRepository, JwtRepository>();
        services.AddScoped<IServicesBase, ServicesBase>();
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
    }

    /// <summary>
    /// JWT configuration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="Configuration"></param>
    /// <param name="jwtSettings"></param>
    private static void AddJWT(this IServiceCollection services, IConfiguration Configuration, JwtSettings jwtSettings)
    {
        services.AddHttpContextAccessor()
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidAudience = jwtSettings.ValidAudience,
                        RequireExpirationTime = jwtSettings.RequireExpirationTime,
                        ValidateLifetime = jwtSettings.RequireExpirationTime,
                        ClockSkew = TimeSpan.FromDays(1),
                    };
                });
    }

    /// <summary>
    /// Add database services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="Configuration"></param>
    private static void AddDatabases(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<MainDbContext>(
                (provider, options) =>
                    {
                        options.UseSqlServer(Configuration.GetConnectionString("MainDB"));

                        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                        options.UseLoggerFactory(loggerFactory);
                    });
    }

    /// <summary>
    /// Swagger global config
    /// </summary>
    /// <param name="services"></param>
    private static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                     },
                    new string[] {}
                }
            });
#if DEBUG
            var filePath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
            options.IncludeXmlComments(filePath);
#endif
        });
    }
}
