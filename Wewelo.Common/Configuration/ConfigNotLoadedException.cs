using System;
using System.Collections.Generic;
using System.Text;

namespace Wewelo.Common.Configuration
{
    public class ConfigNotLoadedException : Exception
    {
        public ConfigNotLoadedException() : base("The configuration have not been loaded.") { }
    }
}
