using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Wewelo.Common.Configuration
{
    //creates and handles configuration files, with data about tasks, and permissions.
    public class ApplicationConfigurationManager
    {
        JObject configJSON;
        private Dictionary<String, JToken> map;
        
        public T Get<T>(string path)
        {
            return Get<T>(path, () => { throw new ConfigNotFoundException(path); });
        }

        public T Get<T>(string path, T defaultValue)
        {
            return Get<T>(path, () => defaultValue);
        }

        private T Get<T>(string path, Func<T> notFoundAction)
        {
            if (configJSON == null)
            {
                throw new ConfigNotLoadedException();
            }

            Object token = GetJToken(path);
            if (token == null)
                return notFoundAction();

            try
            {
                if (token is JArray)
                {
                    throw new ConfigArrayException("");
                }
                return ((JToken)token).Value<T>();
            }
            catch (FormatException exp)
            {
                throw new ConfigCastException(path, typeof(T), exp);
            }
        }

        public IList<T> GetArray<T>(string path)
        {
            return GetArray<T>(path, () => { throw new ConfigNotFoundException(path); });
        }

        public IList<T> GetArray<T>(string path, IList<T> defaultValue)
        {
            return GetArray<T>(path, () => defaultValue);
        }

        private IList<T> GetArray<T>(string path, Func<IList<T>> notFoundAction)
        {
            if (configJSON == null)
            {
                throw new ConfigNotLoadedException();
            }

            Object token = GetJToken(path);
            if (token == null)
                return notFoundAction();

            try
            {
                if (!(token is JArray))
                {
                    throw new ConfigNotArrayException(path);
                }
                return ((JArray)token).Values<T>().ToList();
            }
            catch (FormatException exp)
            {
                throw new ConfigCastException(path, typeof(T), exp);
            }
        }

        private Object GetJToken(string path)
        {
            string[] pathSplit = path.Split('.');
            JToken current = configJSON;
            for (int i = 0; i < pathSplit.Length; i++)
            {
                current = current[pathSplit[i]];
                if (current == null)
                {
                    return null;
                }
            }
            return current;
        }

        //main function to be called
        public void Load(string filepath)
        {
            configJSON = JObject.Parse(File.ReadAllText(filepath));
        }
    }
}

