using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
   : ICommandHandler<RegisterUserCommand, Guid>
{
   public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
   {
      var user = User.Create(request.Email, request.FirstName, request.LastName);

      userRepository.Insert(user);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return user.Id;
   }
}