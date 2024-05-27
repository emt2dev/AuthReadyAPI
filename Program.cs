using AuthReadyAPI.Configurations;
using AuthReadyAPI.DataLayer;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Repositories;
using AuthReadyAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json;
using Stripe;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.SeederConfigurations;
using Microsoft.Extensions.Configuration.UserSecrets;
using AuthReadyAPI.DataLayer.Services;
var builder = WebApplication.CreateBuilder(args);

/*
 * Everything below was added as part of AuthReady
 * Read the Instructions portion of the readMe.md
 * 
 */

// DATABASE, MSSQL
var CONNECTION_STRING = builder.Configuration.GetConnectionString("MySql_ConnectionString"); // replace with your own connection string
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

builder.Services.AddDbContext<AuthDbContext>(DbOptions =>
{
    DbOptions.UseMySql(CONNECTION_STRING, serverVersion);
});

// asp.net controller
builder.Services.AddControllers();

// api versioning, swagger interface with auth req'd
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Auth Ready API",
        Version = "v1",
    });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"Jwt Authorization header using bearer scheme.
                        Enter 'Bearer' [space] and then token.
                        Example: 'Bearer 12345'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "0auth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        adding => adding.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

// Add Serilog
builder.Host.UseSerilog((builderContext, loggerConfig) =>
    loggerConfig.WriteTo.Console()
    .ReadFrom
    .Configuration(builderContext.Configuration));

/* 
 * 
 * Repositories
 * 
 */

/* IDemo, ICache */
builder.Services.AddScoped<IDemoRepository, DemoRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();

/* Adds AutoMapper Config */
builder.Services.AddAutoMapper(typeof(MapperConfig));

/* Adds Identity Core */
builder.Services.AddIdentityCore<APIUserClass>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<APIUserClass>>("AuthReadyAPI")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

/* IAuthManager */
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

/* Authentication plus JWT Bearer */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
    };
}
);

/* additional API Versioning */
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new MediaTypeApiVersionReader("ver")
        );
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

/* Health Checks, 
 * includes AspNetCore.HealthChecks.SqlServer
 * check to see if EFCore<DbContext> can connect via Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore
 */

builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("Custom Health Check",
    failureStatus: HealthStatus.Degraded,
    tags: new[] { "Custom" }
    )
    .AddSqlServer(CONNECTION_STRING, tags: new[] { "database" })
    .AddDbContextCheck<AuthDbContext>(tags: new[] { "database" });

var app = builder.Build();
// seed users and roles
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<APIUserClass>>();
    var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    await APIUserSeeder.Seed(context, userManager);
    // works
}

    app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

/*
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
    {
        Public = true,
        MaxAge = TimeSpan.FromSeconds(1) // refresh the cache every this many seconds
    };

    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] = new string[] { "Accept-Encoding" };

    await next();
});
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


/* Health Checks */
/* Health Checks */
app.MapHealthChecks("/healthcheck");

app.MapHealthChecks("/healthcheckCustom", new HealthCheckOptions
{
    Predicate = HealthCheck => HealthCheck.Tags.Contains("custom"),
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    },
    ResponseWriter = WriteResponse
});

/* Health Check for DB */
app.MapHealthChecks("/healthcheckdatabase", new HealthCheckOptions
{
    Predicate = HealthCheck => HealthCheck.Tags.Contains("database"),
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    },
    ResponseWriter = WriteResponse
});


/* Health Check Log */
static Task WriteResponse(HttpContext context, HealthReport HR)
{
    context.Response.ContentType = "application/json; charset=utf-8";

    var options = new JsonWriterOptions { Indented = true };

    using var memoryStream = new MemoryStream();
    using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
    {
        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", HR.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var entry in HR.Entries)
        {
            jsonWriter.WriteStartObject(entry.Key);
            jsonWriter.WriteString("status", entry.Value.Status.ToString());

            if (entry.Value.Description != null) jsonWriter.WriteString("description", entry.Value.Description.ToString());

            jsonWriter.WriteStartObject("data");

            foreach (var item in entry.Value.Data)
            {
                jsonWriter.WritePropertyName(item.Key);
                JsonSerializer.Serialize(jsonWriter, item.Value, item.Value?.GetType() ?? typeof(object));
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
    }

    return context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
