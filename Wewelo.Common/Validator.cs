using System;

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
    }
}
