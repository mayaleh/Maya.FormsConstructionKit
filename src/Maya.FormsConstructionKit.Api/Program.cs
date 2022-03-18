using Maya.FormsConstructionKit.Api.CQRS;
using Maya.FormsConstructionKit.Api.Model.Settings;
using Maya.FormsConstructionKit.Api.Model.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Maya.FormsConstructionKit.Api.Library;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
Serilog.ILogger logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

builder.Logging.AddSerilog(logger);

var services = builder.Services;

// Add services to the container.
services.AddSingleton(logger);
// Set configuration
services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>();
services.AddSingleton<IAppSettings>(sp => appSettings);


// MongoDb datasources
services.AddSingleton<AppDataContext>();

// Auth
services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidIssuer = appSettings.OAuthentication.Issuer;
        options.Audience = appSettings.OAuthentication.Audience;
        options.Authority = appSettings.OAuthentication.Issuer;
        //options.TokenValidationParameters.IssuerSigningKeys = AuthHelper.GetJwks(appSettings.OAuthentication);

        options.RequireHttpsMetadata = appSettings.OAuthentication.RequireHttpsMetadata;
        options.TokenValidationParameters.ValidateIssuerSigningKey = appSettings.OAuthentication.ValidateIssuerSigningKey;
        options.TokenValidationParameters.ValidateAudience = appSettings.OAuthentication.ValidateAudience;
        options.TokenValidationParameters.ValidateIssuer = appSettings.OAuthentication.ValidateIssuer;
        options.TokenValidationParameters.RequireExpirationTime = appSettings.OAuthentication.RequireExpirationTime;
        options.TokenValidationParameters.RequireSignedTokens = appSettings.OAuthentication.RequireSignedTokens;
        options.TokenValidationParameters.ValidAudience = appSettings.OAuthentication.Audience;

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = (a) =>
            {
                return Task.CompletedTask;
            },
            OnTokenValidated = (a) =>
            {
                return Task.CompletedTask;
            }
        };
    });

services.AddCQRS();
services.AddCors();
services.AddAppLibraryServices();
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x.WithOrigins(appSettings.AllowedOrigins)
                .AllowAnyMethod()   
                .AllowAnyHeader()
                //.SetIsOriginAllowed(origin => true)
                .AllowCredentials()
                .WithExposedHeaders("*"));
app.Run();
