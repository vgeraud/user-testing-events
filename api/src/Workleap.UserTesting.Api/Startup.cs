using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Workleap.DomainEventPropagation;
using Workleap.UserTesting.Api.Bootstrapping;
using Workleap.UserTesting.Api.DomainEventHandlers;
using Workleap.UserTesting.Api.DomainEvents;

namespace Workleap.UserTesting.Api;

[ExcludeFromCodeCoverage]
public sealed class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().ConfigureJsonSerializerOptions();

        services.AddAzureAppConfiguration();
        services.AddHealthChecks();
        services.ConfigureCorsOptions(this.Configuration);
        services.AddDataProtection();

        services.AddEventPropagationPublisher();

        services.AddEventPropagationSubscriber()
                .AddDomainEventHandler<SignedUpDomainEvent, SignedUpDomainEventHandler>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        if (!(env.IsEnvironment("DevelopmentDocker") || env.IsEnvironment("Development")))
        {
            app.UseAzureAppConfiguration();
        }


        app.UseRouting()
            .UseCors()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints
                    .MapHealthChecksEndpoint()
                    .MapStartupHealthChecksEndpoint()
                    .MapAuthorizedPingHealthCheckEndpoint("Insert proper AuthenticationScheme here");

                endpoints.MapControllers();

                endpoints.MapEventPropagationEndpoint();
            });

        
    }
}
