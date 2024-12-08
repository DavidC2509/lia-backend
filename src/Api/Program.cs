using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lia.Api.Extensions;
using Lia.Infrastructure;
using Lia.Infrastructure.Data;
using Lia.SharedKernel.Exceptions;
using Lia.SharedKernel.Interface;
using LiaBackend.ServiceDefaults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


var appName = "LiaBackendApi";

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.AddSwagger();

builder.Services.AddHttpContextAccessor();

builder.AddNpgsqlDbContext<AppDbContext>("lia-database");

builder.Services.AddTransactionDependencies(builder.Configuration);
builder.AddServiceDefaults();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

builder.Services.AddProblemDetails();
builder.Services.AddControllers();

//JWT 
using var loggerFactory = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Trace).AddConsole());

var secret = builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = ctx => LogAttempt(ctx.Request.Headers, "OnChallenge"),
        OnTokenValidated = ctx => LogAttempt(ctx.Request.Headers, "OnTokenValidated")
    };
});

var app = builder.Build();

app.MapDefaultEndpoints();


app.UseSwagger();
app.UseExceptionHandler("/error");
app.UseSwaggerUI();


app.Map("/error", (IHttpContextAccessor httpContextAccessor, IErrorPropertiesFactory errorPropertiesFactory) =>
{
    Exception? exception = httpContextAccessor.HttpContext?
        .Features.Get<IExceptionHandlerFeature>()?
        .Error;

    IDictionary<string, object?> commonErrorProperties;

    if (exception is ServiceException)
    {
        commonErrorProperties = errorPropertiesFactory.CreateCommonProperties(exception is ServiceException er ? er.Errors : null);
    }
    else
    {
        commonErrorProperties = errorPropertiesFactory.CreateCommonProperties(exception?.Message ?? "Internal Server");
    }

    return exception is ServiceException e
        ? Results.Problem(
            title: e.ErrorMessage,
            statusCode: e.HttpStatus,
            extensions: commonErrorProperties)
        : Results.Problem(
            title: "Interno.",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: commonErrorProperties);
});

app.MapDefaultControllerRoute();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorePolicy");

try
{
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appName);
}
//finally
//{
//    Serilog.Log.CloseAndFlush();
//}

Task LogAttempt(IHeaderDictionary headers, string eventType)
{
    var logger = loggerFactory.CreateLogger<Program>();

    var authorizationHeader = headers.Authorization.FirstOrDefault();

    if (authorizationHeader is null)
        logger.LogInformation($"{eventType}. JWT not present");
    else
    {
        string jwtString = authorizationHeader["Bearer ".Length..];

        var jwt = new JwtSecurityToken(jwtString);

        logger.LogInformation($"{eventType}. Expiration: {jwt.ValidTo.ToLongTimeString()}. System time: {DateTime.UtcNow.ToLongTimeString()}");
    }

    return Task.CompletedTask;
}

