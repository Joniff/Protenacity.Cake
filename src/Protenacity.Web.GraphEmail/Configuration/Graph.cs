using Microsoft.Extensions.Configuration;

namespace Protenacity.Web.GraphEmail.Configuration;

public class Graph
{
    [ConfigurationKeyName("Umbraco:Cms:Global:GraphEmail:ClientId")]
    public string? ClientId { get; init; }

    [ConfigurationKeyName("Umbraco:Cms:Global:GraphEmail:ClientSecret")]
    public string? ClientSecret { get; init; }

    [ConfigurationKeyName("Umbraco:Cms:Global:GraphEmail:TenantId")]
    public string? TenantId { get; init; }

    [ConfigurationKeyName("Umbraco:Cms:Global:GraphEmail:FromEmailUser")]
    public string? FromEmailUser { get; init; }
}
