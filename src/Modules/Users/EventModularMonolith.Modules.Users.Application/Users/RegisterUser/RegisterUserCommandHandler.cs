using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
   IIdentityProviderService identityProviderService,
   IUserRepository userRepository,
   IUnitOfWork unitOfWork)
   : ICommandHandler<RegisterUserCommand, Guid>
{
   public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
   {
      Result<string> result =
         await identityProviderService.RegisterUserAsync(
            new(request.Email, request.Password, request.FirstName, request.LastName),
            cancellationToken);

      if (result.IsFailure)
      {
         return Result.Failure<Guid>(result.Error);
      }

      var user = User.Create(request.Email, request.FirstName, request.LastName, result.Value);

      await userRepository.InsertAsync(user, cancellationToken);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return user.Id.Value;
   }
}
