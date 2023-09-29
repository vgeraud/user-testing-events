using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Workleap.UserTesting.Api.Security;

[ExcludeFromCodeCoverage]
internal sealed class CorsPolicySettings
{
    private static readonly string[] DefaultAllowedHeaders = { "*" };
    private string[] _allowedOrigins;

    public CorsPolicySettings()
    {
        this.AllowedHeaders = DefaultAllowedHeaders;
        this.AllowedMethods = Array.Empty<string>();
        this.AllowedOrigins = Array.Empty<string>();
    }

    public string[] AllowedHeaders { get; set; }

    public string[] AllowedMethods { get; set; }

    public string[] AllowedOrigins
    {
        get => this._allowedOrigins;
        set => this._allowedOrigins = value.Where(x => !x.Equals("null", StringComparison.OrdinalIgnoreCase)).ToArray();
    }
}
