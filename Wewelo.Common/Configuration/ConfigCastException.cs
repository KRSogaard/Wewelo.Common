using System;

namespace Wewelo.Common.Configuration
{
    public class ConfigCastException : Exception
    {
        public ConfigCastException(string key, Type type)
            : this(key, type, null)
        {
            
        }

        public ConfigCastException(string key, Type type, FormatException exp) 
            : base($"This config pay \"{key}\" was found, but it could not be cast to \"{type.Name}\".", exp)
        {
        }
    }
}
