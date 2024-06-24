using System.Reflection;

namespace EventModularMonolith.Modules.Events.Presentation;

public static class AssemblyReference
{
   public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
