using System;
using System.Collections.Generic;
using System.Text;

namespace Wewelo.Common.Configuration
{
    public class ConfigNotArrayException : Exception
    {
        public ConfigNotArrayException(String configKey)
            : base($"The config key \"{configKey}\" was not an array, please use Get.")
        {
        }
    }
}
