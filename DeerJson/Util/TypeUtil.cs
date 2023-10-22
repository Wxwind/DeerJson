using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DeerJson.Util
{
    public class TypeUtil
    {
        public static bool IsNumber(char? a)
        {
            return a >= '0' && a <= '9';
        }

        public static bool IsAutoProperty(PropertyInfo pi)
        {
            // auto props must have a setter and a getter;
            if (!pi.CanRead || !pi.CanWrite) return false;

            var getter = pi.GetGetMethod(false);
            var setter = pi.GetGetMethod(false);

            if (getter != null && setter != null)
            {
                // is public auto prop?
                if (getter.IsDefined(typeof(CompilerGeneratedAttribute), false)
                    && setter.IsDefined(typeof(CompilerGeneratedAttribute), false))
                    return true;
            }

            // is private auto prop?
            var backingField = GetBackingField(pi);
            return backingField != null && backingField.IsDefined(typeof(CompilerGeneratedAttribute), false);
        }

        public static bool IsAutoPropertyBackingField(FieldInfo fi)
        {
            var name = fi.Name;
            return Regex.IsMatch(name, "^<.*>k__BackingField$");
        }

        private static FieldInfo GetBackingField(PropertyInfo pi)
        {
            var backingFieldName = $"<{pi.Name}>k__BackingField";
            return pi.DeclaringType?.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(field => field.Name == backingFieldName);
        }
    }
}