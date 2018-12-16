using System;
using System.Collections.Generic;

namespace Assets.Scripts.Extensions
{
    public static class IListExtensions
    {
        public static T GetRandomElement<T>(this IList<T> collection)
        {
            var random = new Random();

            var index = random.Next(0, collection.Count);

            return collection[index];
        }
    }
}
