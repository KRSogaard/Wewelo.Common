using System;
using System.Collections.Generic;
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
            appConfig.ReadFromJsonFile(testFile);
        }

        [Fact]
        public void TestFetchFirstLevelStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal("Kasper", appConfig.Get<string>("Name"));
        }

        [Fact]
        public void TestFetchDeepLevelStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal("http://www.wewelo.com", appConfig.Get<string>("Tasks.SQSQueueUrl"));
        }

        [Fact]
        public void TestFetchSuperDeepLevelStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal("Value", appConfig.Get<string>("This.is.A.Super.Deep"));
        }

        [Fact]
        public void TestFetchDoubleKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal(true, appConfig.Get<bool>("UseBlah"));
        }

        [Fact]
        public void TestFetchSuperDeepDoubleKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal(false, appConfig.Get<bool>("This.is.A.Boolean"));
        }

        [Fact]
        public void TestIntegerKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal(12, appConfig.Get<int>("Tasks.Threads"));
        }

        [Fact]
        public void TestGetIntegerAsStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal("12", appConfig.Get<string>("Tasks.Threads"));
        }

        [Fact]
        public void TestGetBooleanAsStringKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal("false", appConfig.Get<string>("This.is.A.Boolean").ToLower());
        }

        [Fact]
        public void TestNotExistsKey()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            var exp = Record.Exception(() => appConfig.Get<string>("This.Key.Do.Not.Exist"));
            Assert.NotNull(exp);
            Assert.Equal(typeof(ConfigNotFoundException), exp.GetType());
        }

        [Fact]
        public void TestNotExistsKeyWithDefault()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            Assert.Equal("This is a default value", appConfig.Get<string>("This.Key.Do.Not.Exist", "This is a default value"));
        }

        [Fact]
        public void TestCastStringToInteger()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

            var exp = Record.Exception(() => appConfig.Get<int>("Tasks.SQSQueueUrl"));
            Assert.NotNull(exp);
            Assert.Equal(typeof(ConfigCastException), exp.GetType());
        }

        [Fact]
        public void TestCastStringToBoolean()
        {
            ApplicationConfigurationManager appConfig = new ApplicationConfigurationManager();
            appConfig.ReadFromJsonFile(testFile);

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
