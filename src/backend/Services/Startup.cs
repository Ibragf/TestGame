using System.IO;
using System.Reflection;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.CServices;
using Services.Settings;

[assembly:FunctionsStartup(typeof(Services.Startup))]
namespace Services;

public sealed class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();

        var connectionString = context.Configuration.GetConnectionString("Postgres");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IStatisticsService, StatisticsService>();
        
        builder.Services.AddSingleton<IJwtGenerator, JwtGenerator>();
        
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddRandomizer();

        builder.Services.Configure<JwtSettings>(context.Configuration.GetSection(JwtSettings.SectionName));

        builder.AddJwtBearerAuthentication(context.Configuration);
        builder.AddAuthorization();
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);

        var context = builder.GetContext();

        builder.ConfigurationBuilder
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"))
            .AddEnvironmentVariables();
    }
}