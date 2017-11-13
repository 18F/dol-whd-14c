using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business.Extensions
{
    public static class GetPropertyValueExtensions
    {
        /// <summary>
        ///     Get a property value from this object, if it exists.  Property
        ///     names can be nested in a dot-delimited list; e.g.:
        ///     myObject.GetPropValue("child.grandchild.leaf");
        /// </summary>
        /// <param name="obj">
        ///     The object to get a property from
        /// </param>
        /// <param name="name">
        ///     The name of the property to get.  The
        ///     name can be nested in a dot-delimited list; e.g.,
        ///     "child.grandchild.leaf"
        /// </param>
        /// <returns>
        ///     The property value if it exists, or null.
        /// </returns>
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        /// <summary>
        ///     Get a property value from this object, if it exists.  Property
        ///     names can be nested in a dot-delimited list; e.g.:
        ///     myObject.GetPropValue("child.grandchild.leaf");
        /// </summary>
        /// <typeparam name="T">
        ///     The type of value to get and return.
        /// </typeparam>
        /// <param name="obj">
        ///     The object to get a property from
        /// </param>
        /// <param name="name">
        ///     The name of the property to get.  The
        ///     name can be nested in a dot-delimited list; e.g.,
        ///     "child.grandchild.leaf"
        /// </param>
        /// <returns>
        ///     The property value if it exists, or null.
        /// </returns>
        /// <exception cref="System.InvalidCastException">
        ///     Thrown if the property value cannot be cast to the
        ///     requested type.
        /// </exception>
        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        /// <summary>
        ///     Get a property value from this object, if it exists.  Property
        ///     names can be nested in a dot-delimited list; e.g.:
        ///     myObject.GetPropValue("child.grandchild.leaf");
        /// </summary>
        /// <param name="obj">
        ///     The object to get a property from
        /// </param>
        /// <param name="name">
        ///     The name of the property to get.  The
        ///     name can be nested in a dot-delimited list; e.g.,
        ///     "child.grandchild.leaf"
        /// </param>
        /// <returns>
        ///     The property value as a string if it exists, or
        ///     the default string value.
        /// </returns>
        /// <exception cref="System.InvalidCastException">
        ///     Thrown if the property value cannot be cast to a string.
        /// </exception>
        public static string GetStringPropValue(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(string); }

            // throws InvalidCastException if types are incompatible
            return retval.ToString();
        }
    }
}
