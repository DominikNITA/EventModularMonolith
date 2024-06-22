namespace EventModularMonolith.Api.Extensions;

internal static class ConfigurationExtensions
{
   internal static void AddModuleConfigurations(this IConfigurationBuilder configurationBuilder, string[] modules)
   {
      foreach (string module in modules)
      {
         configurationBuilder.AddJsonFile($"modules.{module}.json", false, true);
         configurationBuilder.AddJsonFile($"modules.{module}.Development.json", false, true);
      }
   }
}
