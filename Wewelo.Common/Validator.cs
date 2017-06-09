using System;
using System.Collections.Generic;

namespace Wewelo.Common
{
    public static class Validator
    {
        public static void NonNull(string field, Object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException($"Field \"{field}\" is required.");
            }
        }
        public static void NonEmpty(string field, String obj)
        {
            NonNull(field, obj);
            if (String.IsNullOrWhiteSpace(obj))
            {
                throw new ArgumentException($"Field \"{field}\" is can not be empty.");
            }
        }
        public static void NonEmpty<T>(string field, IEnumerable<T> obj)
        {
            NonNull(field, obj);
            foreach (var o in obj)
            {
                // There where an item
                return;
            }
            throw new ArgumentException($"Field \"{field}\" is can not be empty.");
        }
    }
}
