using System.Reflection;
#pragma warning disable S4035
#pragma warning disable IDE0074
#pragma warning disable S3011

namespace EventModularMonolith.Shared.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
   private List<PropertyInfo> _properties;

   private List<FieldInfo> _fields;

   public static bool operator ==(ValueObject obj1, ValueObject obj2)
   {
      if (Equals(obj1, null))
      {
         if (Equals(obj2, null))
         {
            return true;
         }

         return false;
      }

      return obj1.Equals(obj2);
   }

   public static bool operator !=(ValueObject obj1, ValueObject obj2) => !(obj1 == obj2);


   public bool Equals(ValueObject? other)
   {
      return Equals(other as object);
   }

   public override bool Equals(object? obj)
   {
      if (obj == null || GetType() != obj.GetType())
      {
         return false;
      }

      return GetProperties().TrueForAll(p => PropertiesAreEqual(obj, p))
             && GetFields().TrueForAll(f => FieldsAreEqual(obj, f));
   }

   public override int GetHashCode()
   {
      unchecked
      {
         int hash = 17;
         foreach (PropertyInfo prop in GetProperties())
         {
            object? value = prop.GetValue(this, null);
            hash = HashValue(hash, value);
         }

         foreach (FieldInfo field in GetFields())
         {
            object? value = field.GetValue(this);
            hash = HashValue(hash, value);
         }

         return hash;
      }
   }

   private bool PropertiesAreEqual(object obj, PropertyInfo p)
   {
      return Equals(p.GetValue(this, null), p.GetValue(obj, null));
   }

   private bool FieldsAreEqual(object obj, FieldInfo f)
   {
      return Equals(f.GetValue(this), f.GetValue(obj));
   }

   private List<PropertyInfo> GetProperties()
   {
#pragma warning disable IDE0074
      if (_properties == null)
#pragma warning restore IDE0074
      {
         _properties = GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToList();

         // Not available in Core
         // !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();
      }

      return _properties;
   }

   private List<FieldInfo> GetFields()
   {
#pragma warning disable IDE0074
      if (_fields == null)
#pragma warning restore IDE0074
      {
         _fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .ToList();
      }

      return _fields;
   }

   private int HashValue(int seed, object? value)
   {
      int currentHash = value?.GetHashCode() ?? 0;

      return seed * 23 + currentHash;
   }
}
