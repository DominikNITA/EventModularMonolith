using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;

namespace EventModularMonolith.Modules.Events.Infrastructure.Categories;

internal sealed class CategoryRepository(EventsDbContext context) : Repository<Category>(context), ICategoryRepository
{

}
