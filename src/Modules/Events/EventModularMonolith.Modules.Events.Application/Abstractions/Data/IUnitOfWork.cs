using System.Data.Common;

namespace EventModularMonolith.Modules.Events.Application.Abstractions.Data;

public interface IUnitOfWork
{
   Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
