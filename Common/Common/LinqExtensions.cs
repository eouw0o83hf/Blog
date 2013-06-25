using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class LinqExtensions
    {
        #region Null-Friendly

        public static T? FirstOrNull<T>(this IEnumerable<T> items, Func<T, bool> predicate) where T : struct
        {
            return items.Where(a => predicate(a)).FirstOrNull();
        }

        public static T? FirstOrNull<T>(this IEnumerable<T> items) where T : struct
        {
            return items.AsNullable().FirstOrDefault();
        }

        public static IEnumerable<T?> AsNullable<T>(this IEnumerable<T> items) where T : struct
        {
            return items.Cast<T?>();
        }

        public static TResult SelectFirstOrDefault<T, TResult>(this IEnumerable<T> items, Func<T, bool> where, Func<T, TResult> select)
        {
            return items.Where(where).Select(select).FirstOrDefault();
        }

        #endregion

        #region Dictionary

        public static TValue Get<TValue>(this IDictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return (TValue)dictionary[key];
            }
            return default(TValue);
        }

        #endregion
    }
}
