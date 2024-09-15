using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizerForModerator;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Modules.Users.Application.Organizers.GetAuthTokensForOrganizer;

internal sealed class GetAuthTokensForOrganizerQueryHandler(
   IIdentityProviderService identityProviderService,
   IUserRepository userRepository,
   ISender sender
)
   : IQueryHandler<GetAuthTokensForOrganizerQuery, AuthTokenWithRefresh>
{
   public async Task<Result<AuthTokenWithRefresh>> Handle(GetAuthTokensForOrganizerQuery request, CancellationToken cancellationToken)
   {
      Result<AuthTokenWithRefresh> result =
         await identityProviderService.GetAuthTokens(
            request.Email,
            request.Password,
            cancellationToken);

      if (result.IsFailure)
      {
         return Result.Failure<AuthTokenWithRefresh>(result.Error);
      }

      User user = await userRepository.GetByEmailAsync(request.Email,cancellationToken);

      Result<OrganizerDto> organizer = await sender.Send(new GetOrganizerForModeratorQuery(user.Id.Value), cancellationToken);

      if (organizer.IsFailure)
      {
         return Result.Failure<AuthTokenWithRefresh>(IdentityProviderErrors.InvalidCredentials);
      }

      result = Result.Success(result.Value with { OrganizerId = organizer.Value.Id.ToString() });

      return result.Value;
   }
}
