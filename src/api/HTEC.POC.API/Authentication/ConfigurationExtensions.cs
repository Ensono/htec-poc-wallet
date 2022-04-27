using Microsoft.Extensions.Configuration;

namespace HTEC.POC.API.Authentication;

public static class ConfigurationExtensions
{
    public static IConfigurationSection GetJwtBearerAuthenticationConfigurationSection(this IConfiguration configuration) =>
        configuration.GetSection("JwtBearerAuthentication");
}
