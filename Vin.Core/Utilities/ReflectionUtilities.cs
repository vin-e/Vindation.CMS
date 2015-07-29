using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Vin.Core.Utilities
{
    public sealed class ReflectionUtilities
    {
        private ReflectionUtilities() { }

        /// <summary>
        /// Determine if generic type is inherited from type to check
        /// </summary>
        /// <param name="generic">Type to evaluate against</param>
        /// <param name="toCheck">Request type to check</param>
        /// <returns>boolean</returns>
        public static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        /// <summary>
        /// Determine if a property exists in an object
        /// </summary>
        /// <param name="propertyName">Name of the property </param>
        /// <param name="srcObject">the object to inspect</param>
        /// <returns>true if the property exists, false otherwise</returns>
        /// <exception cref="ArgumentNullException">if srcObject is null</exception>
        /// <exception cref="ArgumentException">if propertName is empty or null </exception>
        public static bool Exists(string propertyName, object srcObject)
        {
            if (srcObject == null)
                throw new System.ArgumentNullException("srcObject");

            if ((propertyName == null) || (propertyName == String.Empty) || (propertyName.Length == 0))
                throw new System.ArgumentException("Property name cannot be empty or null.");

            PropertyInfo propInfoSrcObj = srcObject.GetType().GetProperty(propertyName);

            return (propInfoSrcObj != null);
        }

        /// <summary>
        /// Determine if a property exists in an object
        /// </summary>
        /// <param name="propertyName">Name of the property </param>
        /// <param name="srcObject">the object to inspect</param>
        /// <param name="ignoreCase">ignore case sensitivity</param>
        /// <returns>true if the property exists, false otherwise</returns>
        /// <exception cref="ArgumentNullException">if srcObject is null</exception>
        /// <exception cref="ArgumentException">if propertName is empty or null </exception>
        public static bool Exists(string propertyName, object srcObject, bool ignoreCase)
        {
            if (!ignoreCase)
                return Exists(propertyName, srcObject);

            if (srcObject == null)
                throw new System.ArgumentNullException("srcObject");

            if ((propertyName == null) || (propertyName == String.Empty) || (propertyName.Length == 0))
                throw new System.ArgumentException("Property name cannot be empty or null.");


            PropertyInfo[] propertyInfos = srcObject.GetType().GetProperties();

            propertyName = propertyName.ToLower();
            foreach (PropertyInfo propInfo in propertyInfos)
            {
                if (propInfo.Name.ToLower().Equals(propertyName))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = TypeDescriptor.GetConverter(destinationType);
                TypeConverter sourceConverter = TypeDescriptor.GetConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }
    }
}
