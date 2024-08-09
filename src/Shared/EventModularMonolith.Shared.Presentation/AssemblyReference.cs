using System.Reflection;

namespace EventModularMonolith.Shared.Presentation;

public static class AssemblyReference
{
   public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
