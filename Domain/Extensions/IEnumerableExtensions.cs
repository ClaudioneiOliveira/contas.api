using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace contas.api.Domain.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            if (self == null)
                return;

            foreach (var item in self)
            {
                action(item);
            }
        }


        public static void ForEach<T>(this IEnumerable<T> self, Action<T, int> action)
        {
            if (self == null)
                return;

            int i = 0;
            foreach (var item in self)
                action(item, i++);
        }

        public static void ForEachWhile<T>(this IEnumerable<T> self, Predicate<T> predicate, Action<T> action)
        {
            if (self == null)
                return;

            foreach (var item in self)
            {
                if (!predicate(item))
                    break;

                action(item);
            }
        }

        public static void ForEachWhile<T>(this IEnumerable<T> self, Predicate<T> predicate, Action<T, int> action)
        {
            if (self == null)
                return;

            int i = 0;
            foreach (var item in self)
            {
                if (!predicate(item))
                    break;

                action(item, i++);
            }
        }

        public static string AggregateIntoString<T>(this IEnumerable<T> list)
            where T : class
            => list.AggregateIntoString(null, null);

        public static string AggregateIntoString<T>(this IEnumerable<T> list, string separator)
            where T : class
            => list.AggregateIntoString(null, separator);

        public static string AggregateIntoString<T>(this IEnumerable<T> list, string seed, string separator)
            where T : class
        => list.AggregateIntoString(seed, separator, o => o.ToString());

        public static string AggregateIntoString<T>(this IEnumerable<T> list, string seed, string separator, Func<T, string> stringConvert)
            where T : class
        {
            if (seed == null) seed = string.Empty;

            if (separator == null) separator = Environment.NewLine;

            if (list != null && list.Any())
            {
                if (string.IsNullOrEmpty(seed))
                {
                    seed = stringConvert(list.FirstOrDefault());
                    return list.ToArray()[1..].Aggregate(new StringBuilder(seed), (a, b) => a.Append(separator).Append(stringConvert(b))).ToString();
                }
                else
                    return list.Aggregate(new StringBuilder(seed), (a, b) => a.Append(separator).Append(stringConvert(b))).ToString();
            }
            else
                return seed;
        }
    }
}