using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wewelo.Common.Configuration;
using Xunit;

namespace Wewelo.Common.Tests.ConfigurationManagement
{
    public class ConfigurationTests
    {
        private string testFile =
            @"ConfigurationManagement/Files/exampleconfig.json";

        [Fact]
        public void TestConfigFileCanLoad()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);
        }

        [Fact]
        public void TestFetchFirstLevelStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal("Kasper", appConfig.Get<string>("Name"));
        }

        [Fact]
        public void TestFetchDeepLevelStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal("http://www.wewelo.com", appConfig.Get<string>("Tasks.SQSQueueUrl"));
        }

        [Fact]
        public void TestFetchSuperDeepLevelStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal("Value", appConfig.Get<string>("This.Is.A.Super.Deep"));
        }

        [Fact]
        public void TestFetchDoubleKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal(true, appConfig.Get<bool>("UseBlah"));
        }

        [Fact]
        public void TestFetchSuperDeepDoubleKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal(false, appConfig.Get<bool>("This.Is.A.Boolean"));
        }

        [Fact]
        public void TestIntegerKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal(12, appConfig.Get<int>("Tasks.Threads"));
        }

        [Fact]
        public void TestGetIntegerAsStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal("12", appConfig.Get<string>("Tasks.Threads"));
        }

        [Fact]
        public void TestGetBooleanAsStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal("false", appConfig.Get<string>("This.Is.A.Boolean").ToLower());
        }

        [Fact]
        public void TestNotExistsKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            var exp = Record.Exception(() => appConfig.Get<string>("This.Key.Do.Not.Exist"));
            Assert.NotNull(exp);
            Assert.Equal(typeof(ConfigNotFoundException), exp.GetType());
        }

        [Fact]
        public void TestStringArray()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            var array = appConfig.GetArray<string>("StringArray");
            Assert.Equal(2, array.Count);
            Assert.Equal("String1", array[0]);
            Assert.Equal("String2", array[1]);
        }

        [Fact]
        public void TestDeepStringArray()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            var array = appConfig.GetArray<string>("This.Is.A.Super.StringArray");
            Assert.Equal(2, array.Count);
            Assert.Equal("String1", array[0]);
            Assert.Equal("String2", array[1]);
        }

        [Fact]
        public void TestBoolArray()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            var array = appConfig.GetArray<bool>("BoolArray");
            Assert.Equal(3, array.Count);
            Assert.Equal(true, array[0]);
            Assert.Equal(false, array[1]);
            Assert.Equal(true, array[2]);
        }

        [Fact]
        public void TestIntArray()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            var array = appConfig.GetArray<int>("IntArray");
            Assert.Equal(5, array.Count);
            Assert.Equal(5, array[0]);
            Assert.Equal(4, array[1]);
            Assert.Equal(3, array[2]);
            Assert.Equal(2, array[3]);
            Assert.Equal(1, array[4]);
        }

        [Fact]
        public void TestNotExistsKeyWithDefault()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            Assert.Equal("This is a default value", appConfig.Get<string>("This.Key.Do.Not.Exist", "This is a default value"));
        }

        [Fact]
        public void TestCastStringToInteger()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            var exp = Record.Exception(() => appConfig.Get<int>("Tasks.SQSQueueUrl"));
            Assert.NotNull(exp);
            Assert.Equal(typeof(ConfigCastException), exp.GetType());
        }

        [Fact]
        public void TestCastStringToBoolean()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.Load(testFile);

            var exp = Record.Exception(() => appConfig.Get<bool>("Tasks.SQSQueueUrl"));
            Assert.NotNull(exp);
            Assert.Equal(typeof(ConfigCastException), exp.GetType());
        }

        [Fact]
        public void TestConfigNotLoaded()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();

            var exp = Record.Exception(() => appConfig.Get<bool>("Tasks.SQSQueueUrl"));
            Assert.NotNull(exp);
            Assert.Equal(typeof(ConfigNotLoadedException), exp.GetType());
        }
    }
}
