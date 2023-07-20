// System.TypeExtensions

using System.Reflection;

namespace System
{
  public static class TypeExtensions
  {
        public static bool IsTypeOf(this object target, Type type)
        {
            return type.IsInstanceOfType(target);
        }

        public static bool IsTypeOf(this object target, object referenceObject)
        {
            return TypeExtensions.IsTypeOf(target, referenceObject.GetType());
        }
    }
}
