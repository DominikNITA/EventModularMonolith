namespace EventModularMonolith.Shared.Domain;

public abstract class Entity
{
   private readonly List<IDomainEvent> _domainEvents = [];
   public Guid Id { get; protected set; }

   protected Entity()
   {
   }

   public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();

   public void ClearDomainEvents()
   {
      _domainEvents.Clear();
   }

   protected void Raise(IDomainEvent domainEvent)
   {
      _domainEvents.Add(domainEvent);
   }
}
