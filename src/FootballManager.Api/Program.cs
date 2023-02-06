using System.Net;
using FootballManager.Application;
using FootballManager.Application.Common.Interfaces;
using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using FootballManager.Infrastructure.Persistence;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FootballManager.Api;

/// <summary>
/// 
/// </summary>
public static class Program
{
    /// <summary>
    /// Entry point
    /// </summary>
    /// <param name="args"></param>
    public static async Task Main(string[] args)
    {
        var configuration = ConfigApplication();

        try
        {
            var app = ConfigHost(args, configuration);

            ConfigApp(app);

            await app.RunAsync();
            Log.Information("Service shutdown");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static WebApplication ConfigHost(string[] args, IConfigurationRoot configuration)
    {
        Log.Information("Starting web host");
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .ReadFrom.Configuration(ctx.Configuration));

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, configuration.GetValue<int>("HostPort"));
        });

        var services = builder.Services;
        ConfigServices(services);

        builder.Host.UseSerilog();
        var app = builder.Build();
        return app;
    }

    private static void ConfigServices(IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
            options.IncludeXmlComments(filePath);
        });

        services.Config();

        ConfigDatabase(services);

        ConfigHealthCheck(services);
    }

    private static void ConfigDatabase(IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, FootballDbContext>(options =>
            options
                .UseInMemoryDatabase("FootballDbContext")
                .LogTo(Console.WriteLine));
    }

    private static void ConfigApp(WebApplication app)
    {
        CheckDb(app);

        app.UseExceptionHandler($"/error");

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        ConfigHealthCheck(app);
    }

    private static void CheckDb(WebApplication app)
    {
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        var context = serviceScope!.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        if (context is not FootballDbContext db)
            throw new Exception("Error in checking Db");
        
        db.Database.EnsureCreated();
        SeedDb(db);
    }

    private static void SeedDb(IApplicationDbContext dbContext)
    {
        var juventus = Team.Create("Juventus FC", "Serie A", "Italy");
        var ajax = Team.Create("Ajax Amsterdam", "Ajax Amsterdam", "Netherlands");

        dbContext.Teams.Add(juventus);
        dbContext.Teams.Add(ajax);
        
        //Juventus
        var p1 = Player.Create("Wojciech Szczesny", 32, 196, 1, FootType.Right, "Poland");
        var p2 = Player.Create("Mattia Perin", 30, 188, 30, FootType.Right, "Italy");
        var p3 = Player.Create("Carlo Pinsoglio ", 32, 194, 23, FootType.Left, "Italy");
        p1.AddToTeam(juventus.Id, DateTime.Parse("Jul 19, 2017"));
        p2.AddToTeam(juventus.Id, DateTime.Parse("Jul 1, 2018"));
        p3.AddToTeam(juventus.Id, DateTime.Parse("Jul 1, 2014"));
        
        dbContext.Players.AddRange(p1, p2, p3);
        
        //Ajax
        var p10 = Player.Create("Gerónimo Rulli", 30, 189, 12, FootType.Right, "Italy");
        var p11 = Player.Create("Remko Pasveer", 39, 187, 22, FootType.Right, "Netherlands");
        var p12 = Player.Create("Maarten Stekelenburg", 40, 197, 1, FootType.Right, "Netherlands");
        p10.AddToTeam(ajax.Id, DateTime.Parse("Jan 6, 2023"));
        p11.AddToTeam(ajax.Id, DateTime.Parse("Jul 1, 2021"));
        p12.AddToTeam(ajax.Id, DateTime.Parse("Aug 1, 2020"));
        
        dbContext.Players.AddRange(p10, p11, p12);
        dbContext.SaveChangesAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    private static void ConfigHealthCheck(IServiceCollection services)
    {
        services
            .AddHealthChecks();

        services
            .AddHealthChecksUI(settings => settings.SetEvaluationTimeInSeconds(60))
            .AddInMemoryStorage();
    }

    private static void ConfigHealthCheck(WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }

    private static IConfigurationRoot ConfigApplication()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return configuration;
    }
}