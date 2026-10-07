using System.Text;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pomodoro.Application.Common.Behaviors;
using Pomodoro.Infrastructure.Security;
using Pomodoro.WebApi.Middleware;
using Asp.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Pomodoro.WebApi;

public static class DependencyInjection
{
    public const string MultiClientPolicy = "AllowAngularClient";

    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new HtmlSanitizerJsonConverter());
            });
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc();
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException(
                                   "PostgreSQL connection string 'DefaultConnection' not found.");
        services.AddHealthChecks()
            .AddNpgSql(
                    connectionString: connectionString,
                    name: "PostgreSQL-Container",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] {"database", "ready"}
                );
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails(options => 
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                }
            );
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        services.AddCors(options => 
                options.AddPolicy(MultiClientPolicy, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                })
            );
        var secretKey = configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT secret key is missing.");
        var issuer = configuration["JwtSettings:Issuer"];
        var audience = configuration["JwtSettings:Audience"];
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddAuthorization();
        return services;
    }
}