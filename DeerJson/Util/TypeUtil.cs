using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DeerJson.Util
{
    public class TypeUtil
    {
        public static bool IsNumber(char? a)
        {
            return a >= '0' && a <= '9';
        }

        public static bool IsAutoProperty(PropertyInfo property)
        {
            // auto props must have a setter and a getter;
            if (!property.CanRead || !property.CanWrite) return false;

            var getter = property.GetGetMethod(false);
            var setter = property.GetGetMethod(false);

            if (getter != null && setter != null)
            {
                // is public auto prop?
                if (getter.IsDefined(typeof(CompilerGeneratedAttribute), false)
                    && setter.IsDefined(typeof(CompilerGeneratedAttribute), false))
                    return true;
            }

            // is private auto prop?
            var backingField = GetBackingField(property);
            return backingField != null && backingField.IsDefined(typeof(CompilerGeneratedAttribute), false);
        }

        private static FieldInfo GetBackingField(PropertyInfo property)
        {
            var backingFieldName = $"<{property.Name}>k__BackingField";
            return property.DeclaringType?.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(field => field.Name == backingFieldName);
        }
    }
}