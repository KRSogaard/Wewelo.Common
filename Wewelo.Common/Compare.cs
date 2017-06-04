using System;
using System.Collections.Generic;
using System.Text;

namespace Wewelo.Common
{
    public static class Compare
    {
        public static bool CompareList<T>(List<T> original, List<T> current)
        {
            if (original == null && current != null ||
                original != null && current == null)
                return false;
            if (original == null && current == null)
                return true;

            if (original.Count != current.Count)
                return false;

            for (int i = 0; i < original.Count; i++)
            {
                if (original[i] == null)
                {
                    if (current[i] != null)
                        return false;
                } else if (!original[i].Equals(current[i]))
                    return false;
            }
            return true;
        }

        public static bool CompareObject(object o1, object o2)
        {
            if (o1 == null && o2 == null)
                return true;
            if (o1 == null || o2 == null)
                return false;

            return o1.Equals(o2);
        }
    }
}
