using System;
using System.Collections.Generic;
using System.Text;

namespace Wewelo.Common.Configuration
{
    public class ConfigArrayException : Exception
    {
        public ConfigArrayException(String configKey)
            : base($"The config key \"{configKey}\" was an array, please use GetArray.")
        {
        }
    }
}
