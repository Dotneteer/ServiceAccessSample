using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Dpic.Connect.SharedDefinitions.Exceptions
{
    /// <summary>
    /// This static class provides utilities to convert portable exceptions
    /// </summary>
    public static class PortableExceptionConverter
    {
        private static readonly List<string> s_IntrinsicProperties =
            new List<string> { "Source", "HResult" };

        /// <summary>
        /// Gets a dictionary that represents the properties of the specified portable exception
        /// </summary>
        /// <param name="exceptionBase"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Serialize(PortableExceptionBase exceptionBase)
        {
            var props = new Dictionary<string, object>();
            foreach (var prop in exceptionBase.GetType().GetRuntimeProperties())
            {
                if (prop.CanRead && prop.CanWrite && !s_IntrinsicProperties.Contains(prop.Name))
                {
                    var value = prop.GetValue(exceptionBase);
                    if (value != null) props.Add(prop.Name, value);
                }
            }

            return props;
        }

        /// <summary>
        /// Deserializes a set of properties into a concrete PortableExceptionBase instance
        /// </summary>
        /// <typeparam name="T">Type of concrete PortableExceptionBase</typeparam>
        /// <param name="props">Dictionary of properties</param>
        /// <returns>Deserialized object</returns>
        public static T Deserialize<T>(Dictionary<string, object> props)
            where T : PortableExceptionBase, new()
        {
            var ex = new T();
            foreach (var prop in typeof(T).GetRuntimeProperties())
            {
                if (!prop.CanWrite) continue;
                object propVal;
                if (props.TryGetValue(prop.Name, out propVal))
                {
                    var propJArray = propVal as JArray;
                    var convertedValue = propJArray != null
                        ? propJArray.ToObject(prop.PropertyType)
                        : Convert.ChangeType(propVal, prop.PropertyType);
                    prop.SetValue(ex, convertedValue);
                }
            }
            return ex;
        }

        /// <summary>
        /// Deserializes a set of properties into a concrete PortableExceptionBase instance
        /// </summary>
        /// <param name="props">Dictionary of properties</param>
        /// <param name="destType">Type of concrete PortableExceptionBase</param>
        /// <returns>Deserialized object</returns>
        public static object Deserialize(Dictionary<string, object> props, TypeInfo destType)
        {
            if (!destType.IsSubclassOf(typeof(PortableBusinessOperationExceptionBase)))
            {
                throw new InvalidOperationException(
                    $"The destination type should be inherited from {typeof(PortableBusinessOperationExceptionBase)}");
            }

            var method = typeof(PortableExceptionConverter)
                .GetRuntimeMethod("Deserialize", new[] { typeof(Dictionary<string, object>) });
            var genMethod = method.MakeGenericMethod(destType.AsType());
            var result = genMethod.Invoke(null, new object[] { props });
            return result;
        }
    }
}