using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Routing;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Web.Mvc.Helpers
{
    /// <summary>
    /// Contains extension methods of IDictionary&lt;string, objectT&gt;.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Merges the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="replaceExisting">if set to <c>true</c> [replace existing].</param>
        public static void Merge(this IDictionary<string, object> instance, string key, object value, bool replaceExisting)
        {
            Guard.Against(instance == null, "instance is null");
            Guard.Against(string.IsNullOrEmpty(key), "key is null");
            Guard.Against(value == null, "value is null");

            if (replaceExisting || !instance.ContainsKey(key))
            {
                instance[key] = value;
            }
        }

        /// <summary>
        /// Appends the in value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="key">The key.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="value">The value.</param>
        public static void AppendInValue(this IDictionary<string, object> instance, string key, string separator, object value)
        {
            Guard.Against(instance == null, "instance is null");
            Guard.Against(string.IsNullOrEmpty(key), "key is empty");
            Guard.Against(separator == null, "separator is null");
            Guard.Against(value == null, "value is null");

            instance[key] = instance.ContainsKey(key) ? instance[key] + separator + value : value.ToString();
        }

        /// <summary>
        /// Toes the attribute string.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string ToAttributeString(this IDictionary<string, object> instance)
        {
            Guard.Against(instance == null, "instance is null");

            StringBuilder attributes = new StringBuilder();

            foreach (KeyValuePair<string, object> attribute in instance)
            {
                attributes.Append(" {0}=\"{1}\"".FormatWith(HttpUtility.HtmlAttributeEncode(attribute.Key), HttpUtility.HtmlAttributeEncode(attribute.Value.ToString())));
            }

            return attributes.ToString();
        }

        /// <summary>
        /// Merges the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="from">From.</param>
        /// <param name="replaceExisting">if set to <c>true</c> [replace existing].</param>
        public static void Merge(this IDictionary<string, object> instance, IDictionary<string, object> from, bool replaceExisting)
        {
            Guard.Against(instance == null, "instance is null");
            Guard.Against(from == null, "from is null");

            foreach (KeyValuePair<string, object> pair in from)
            {
                if (!replaceExisting && instance.ContainsKey(pair.Key))
                {
                    continue; // Try the next
                }

                instance[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// Merges the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="from">From.</param>
        public static void Merge(this IDictionary<string, object> instance, IDictionary<string, object> from)
        {
            Merge(instance, from, true);
        }

        /// <summary>
        /// Merges the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="values">The values.</param>
        /// <param name="replaceExisting">if set to <c>true</c> [replace existing].</param>
        public static void Merge(this IDictionary<string, object> instance, object values, bool replaceExisting)
        {
            Merge(instance, new RouteValueDictionary(values), replaceExisting);
        }

        /// <summary>
        /// Merges the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="values">The values.</param>
        public static void Merge(this IDictionary<string, object> instance, object values)
        {
            Merge(instance, values, true);
        }
    }
}