using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workleap.UserTesting.Api.Security;

namespace Workleap.UserTesting.Api.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class ApiBootstrapper
{
    public static IMvcBuilder ConfigureJsonSerializerOptions(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverterWithAttributeSupport());
        });

        return mvcBuilder;
    }

    public static void ConfigureCorsOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            var settings = configuration.GetSection("Security").GetSection("Cors").Get<CorsPolicySettings>();

            options.AddDefaultPolicy(builder =>
            {
                builder.WithHeaders(settings.AllowedHeaders)
                    .WithMethods(settings.AllowedMethods)
                    .WithOrigins(settings.AllowedOrigins)
                    .AllowCredentials();
            });
        });
    }
}
