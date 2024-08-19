#pragma warning disable S4035
#pragma warning disable IDE0041
#pragma warning disable IDE0024
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
namespace EventModularMonolith.Shared.Domain;

public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
   public Guid Value { get; }

   protected TypedIdValueBase()
   {

   }

   protected TypedIdValueBase(Guid value)
   {
      if (value == Guid.Empty)
      {
         throw new InvalidOperationException("Id value cannot be empty!");
      }

      Value = value;
   }

   public override bool Equals(object obj)
   {
      if (ReferenceEquals(null, obj))
      {
         return false;
      }

      return obj is TypedIdValueBase other && Equals(other);
   }

   public override int GetHashCode()
   {
      return Value.GetHashCode();
   }

   public bool Equals(TypedIdValueBase other)
   {
      return Value == other?.Value;
   }

   public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
   {
      if (object.Equals(obj1, null))
      {
         if (object.Equals(obj2, null))
         {
            return true;
         }

         return false;
      }

      return obj1.Equals(obj2);
   }

   public static bool operator !=(TypedIdValueBase x, TypedIdValueBase y) => !(x == y);
}
