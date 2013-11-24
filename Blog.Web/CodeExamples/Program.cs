using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    class Program
    {
        public static void Main(string[] args)
        {
            var size = 10000000;
            var target = Enumerable.Range(0, size)
                                    .ToDictionary(a => a, a => Guid.NewGuid());

            var resultList = new List<Guid>();

            var sourceData = Enumerable.Range(size / 2, size);

            var timer = Stopwatch.StartNew();
            // Half will hit the dictionary, half will not
            foreach(var i in sourceData)
            {
                if (target.ContainsKey(i))
                {
                    resultList.Add(target[i]);
                }
            }
            timer.Stop();
            Console.WriteLine("Contains/Index access: {0} milliseconds for {1} total requests",    
                                timer.ElapsedMilliseconds, size);

            resultList.Clear();
            timer.Restart();

            // Half will hit the dictionary, half will not
            foreach (var i in sourceData)
            {
                Guid result;
                if(target.TryGetValue(i, out result))
                {
                    resultList.Add(result);
                }
            }
            timer.Stop();
            Console.WriteLine("TryGet access: {0} milliseconds for {1} total requests",
                                timer.ElapsedMilliseconds, size);

            Console.Read();
        }

        static void x()
        {
            var englishToSpanishWords = new Dictionary<string, string>
            {
                { "ball", "pelota" },
                { "cup", "taza" },
                { "man", "hombre" }
            };

            var ballInSpanish = englishToSpanishWords["woman"];
        }

        static string TryToGetValue_NullCheck(IDictionary<string, string> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return null;
        }

        static string TryToGetValue_Framework(IDictionary<string, string> dictionary, string key)
        {
            string output;
            if (dictionary.TryGetValue(key, out output))
            {
                return output;
            }
            return null;
        }
    }

    public static class Extensions
    {
    public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, 
        TKey key) where TValue : class
    {
        TValue result;
        return source.TryGetValue(key, out result) ? result : null;
    }

    public static TValue? ValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> source, 
        TKey key) where TValue : struct
    {
        TValue result;
        return source.TryGetValue(key, out result) ? result : (TValue?)null;
    }
    }
}
