using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EventModularMonolith.Shared.Infrastructure.Authentication;

internal sealed class JwtBearerConfigureOptions(IConfiguration configuration) : IConfigureNamedOptions<JwtBearerOptions>
{
   private const string ConfigureSectionName = "Authentication";

   public void Configure(JwtBearerOptions options)
   {
      configuration.GetSection(ConfigureSectionName).Bind(options);
   }

   public void Configure(string? name, JwtBearerOptions options)
   {
      Configure(options);
   }
}
