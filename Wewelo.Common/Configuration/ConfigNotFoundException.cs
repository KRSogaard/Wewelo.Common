using System;
using System.Collections.Generic;
using System.Text;

namespace Wewelo.Common.Configuration
{
    public class ConfigNotFoundException : Exception
    {
        public ConfigNotFoundException(String configKey)
            : base($"The config key \"{configKey}\" was not found.")
        {
        }
    }
}
