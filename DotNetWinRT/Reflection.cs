using System;
using System.Reflection;

namespace Jitsukawa.Extensions.Reflection
{
    public static class Reflection
    {
        /// <summary>
        /// Realiza uma cópia rasa do objeto.
        /// </summary>
        public static T Clone<T>(this T obj)
        {
            Type type = typeof(T);
            T newObj = Activator.CreateInstance<T>();

            foreach (PropertyInfo pi in type.GetRuntimeProperties())
                if (pi.CanWrite && !pi.PropertyType.Name.Contains("EntitySet"))
                    pi.SetValue(newObj, type.GetRuntimeProperty(pi.Name).GetValue(obj, null), null);

            return newObj;
        }

        /// <summary>
        /// Realiza uma cópia do objeto com suas referências a outros objetos.
        /// </summary>
        public static T DeepClone<T>(this T obj)
        {
            Type type = typeof(T);
            T newObj = Activator.CreateInstance<T>();

            foreach (PropertyInfo pi in type.GetRuntimeProperties())
                if (pi.CanWrite)
                    pi.SetValue(newObj, type.GetRuntimeProperty(pi.Name).GetValue(obj, null), null);

            return newObj;
        }
    }
}
