using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Wewelo.Common.Configuration
{
    //creates and handles configuration files, with data about tasks, and permissions.
    public class ApplicationConfigurationManager
    {
        private Dictionary<String, JToken> map;
        
        public T Get<T>(string path)
        {
            if (map == null)
            {
                throw new ConfigNotLoadedException();
            }

            string cleanKey = CleanKey(path);
            if (!map.ContainsKey(cleanKey))
            {
                throw new ConfigNotFoundException(path);
            }

            try
            {
                return map[cleanKey].Value<T>();
            }
            catch (FormatException exp)
            {
                throw new ConfigCastException(path, typeof(T), exp);
            }
        }

        public T Get<T>(string path, T defaultValue)
        {
            if (map == null)
            {
                throw new ConfigNotLoadedException();
            }

            string cleanKey = CleanKey(path);
            if (!map.ContainsKey(cleanKey))
            {
                return defaultValue;
            }
            
            try
            {
                return map[cleanKey].Value<T>();
            }
            catch (FormatException exp)
            {
                throw new ConfigCastException(path, typeof(T), exp);
            }
        }

        //main function to be called
        public void CreateConfigDictionary(string filepath)
        {
            ReadFromJsonFile(filepath);
        }

        //read the file
        public void ReadFromJsonFile(string filepath)
        {
            JToken o = JObject.Parse(File.ReadAllText(filepath));
            Dictionary<String, JToken> map = new Dictionary<string, JToken>();
            processConfig(null, map, o);
            this.map = map;
        }

        private void processConfig(string path, Dictionary<String, JToken> map, JToken token)
        {
            if (!token.HasValues)
            {
                map.Add(path, token);
                return;
            }
            
            foreach (var t in token.Children<JObject>())
            {
                processConfig(path, map, t);
            }
            foreach (var t in token.Children<JProperty>())
            {
                String newPath = path == null ? t.Name : path + "." + t.Name;
                processConfig(newPath, map, t);
            }
            foreach (var c in token.Children())
            {
                switch (c.Type)
                {
                    case JTokenType.Integer:
                    case JTokenType.Float:
                    case JTokenType.String:
                    case JTokenType.Boolean:
                    case JTokenType.Null:
                    case JTokenType.Undefined:
                        map.Add(CleanKey(path), c);
                        break;
                }
                Console.WriteLine(c.Type);
            }
            Console.WriteLine(token);
        }

        //return the correct configuration settings key 
        public static string CleanKey(string input)
        {
            return input?.Trim().ToLower();
        }
    }
    
}

