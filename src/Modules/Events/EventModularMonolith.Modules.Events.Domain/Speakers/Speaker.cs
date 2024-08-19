using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Speakers;

public class Speaker : Entity
{
   private Speaker() { }

   public SpeakerId Id { get; set; }
   public string Name { get; private set; }
   public string Description { get; private set; }
   public List<Link> Links { get; private set; } = [];
   public List<Event> Events { get; private set; } = [];

   public static Result<Speaker> Create(string name, string description, List<Link> links)
   {
      var id = Guid.NewGuid();
      var @event = new Speaker
      {
         Id = new SpeakerId(id),
         Name = name,
         Description = description,
         Links = links.Select(l => new Link(l.Url)).ToList()
      };

      return @event;
   }

   public void SetLinks(IEnumerable<Link> links)
   {
      Links = links.ToList();
   }
}

public class Link
{
   private Link()
   {
   }

   public Link(Uri url)
   {
      Url = url;
   }

   public Uri Url { get; set; }
   public SpeakerId SpeakerId { get; set; }
}

public class SpeakerId(Guid value) : TypedIdValueBase(value) { }
