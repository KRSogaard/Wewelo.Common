using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;

namespace Wewelo.Common.ConfigurationManagement
{
    //creates and handles configuration files, with data about tasks, and permissions.
    public class ApplicationConfigurationManager
    {
        public ApplicationConfigurationManager()
        {
            //JObject
        }

        /// <summary>
        /// Get a configuration item from the configuration manager. Navigate sub-areas with . like:  "Tasks.SQSQueueUrl"
        /// </summary>
        public T Get<T>(string path)
        {
            //parse path by .
            string[] parsedPath = path.Split('.');
            int n = parsedPath.Length;
            for (int i = 0; i < n; i++)
            {
                string item = parsedPath[i];
                //if item does not exist
                try
                {
                    //(access/find item)
                }
                catch (Exception e)
                {
                    //throw ConfigNotFoundException($"Could not find the specified configuration item \"{item}\".");
                    return default(T);
                }
                //if we reach the last item in the path:
                if (i == n - 1)
                {
                    try
                    {
                        //parse item to T (if not string?)

                    }
                    catch (Exception e)
                    {
                        //throw ConfigCastException("Expected configuration item type did not match found type. Expected type was: " + typeof(T));
                        return default(T);
                    }
                }
            }

            //return the T (such as SQSQueueUrl)

            return default(T);
        }

        //main function to be called
        public void CreateConfigDictionary(string filepath)
        {
            ReadFromJsonFile(filepath);
        }

        //read the file
        public void ReadFromJsonFile(string filepath)
        {
            string json = @"{
                'Tasks': [ 
                    {
                        'SQSQueueUrl': 'Blah blah blah',
                        'Threads': 12
                    },
                    {
                        'SQSQueueUrl': 'More bla bla bla',
                        'Threads': 404
                    }
                ],
                'UseBlah': true,
                'SIM': {
                    'folder': {
                        'structure': '73f5ddef-8296-48fe-acd6-576b7971298d',
                        'validations': '73f5ddef-8296-48fe-acd6-576b7971298d'
                    }
                }
            }";

            JObject o = JObject.Parse(@"c:\test.json"/*json*/);

            //JObject jsonObject = JObject.Parse(File.ReadAllText(@""+filepath));
            //jsonObject.Children();
            JArray a = (JArray)o["Tasks"];//foreach... next?
            //this below assigns the two inner values, if they have the same name
            IList<Task> person = a.ToObject<IList<Task>>();
            int hold = 0;
        }

        //return the correct configuration settings key 
        public static ConfigurationKeys ParseKey(string input)
        {
            var clean = input.Trim();
            return (ConfigurationKeys)Enum.Parse(typeof(ConfigurationKeys), clean, true);
        }
    }

    public class Task
    {
        public string SQSQueueURL { get; set; }
        public string Threads { get; set; }
    }

    public enum ConfigurationKeys
    {
        Host,
        Port,
        Username,
        Local,
        Remote,
        Passphrase,
        DisableDeletion,
        Privatekey,
        Interval,
        Threads,
        Ignore,
        DelayTime,
        DelayFile,
        FixEOL,
        NotifyBuffer,
        MinResyncMinutes,
        MaxRemoteFetchMinutes,
        SweepIntervalMinutes,
        Unkown
    }
}

