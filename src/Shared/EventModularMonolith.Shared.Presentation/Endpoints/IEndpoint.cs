using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Shared.Presentation.Endpoints;

public interface IEndpoint
{
   void MapEndpoint(IEndpointRouteBuilder app);
}
