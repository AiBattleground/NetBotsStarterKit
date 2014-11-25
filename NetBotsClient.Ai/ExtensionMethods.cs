using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBotsClient.Ai
{
    public static class ExtensionMethods
    {
        private static readonly Random Random = new Random();

        public static IList<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var oldList = source.ToList();
            var newList = new List<T>();
            while (oldList.Any())
            {
                var i = Random.Next(0, oldList.Count - 1);
                newList.Add(oldList[i]);
                oldList.RemoveAt(i);
            }
            return newList;
        }
    }
}
