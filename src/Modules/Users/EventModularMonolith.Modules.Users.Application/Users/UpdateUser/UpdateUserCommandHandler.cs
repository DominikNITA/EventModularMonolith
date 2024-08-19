using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
   : ICommandHandler<UpdateUserCommand>
{
   public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
   {
      User? user = await userRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);

      if (user is null)
      {
         return Result.Failure(UserErrors.NotFound(request.UserId));
      }

      user.Update(request.FirstName, request.LastName);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
   }
}
