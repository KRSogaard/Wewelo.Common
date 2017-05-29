using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Wewelo.Common.Tests
{
    public class CompareTests
    {
        [Fact]
        public void TestNullLists()
        {
            Assert.True(Compare.CompareList((List<string>)null, (List<string>)null));
        }

        [Fact]
        public void TestNullAndChangedists()
        {
            Assert.False(Compare.CompareList((List<string>)null, new List<string>()
            {
                "String1",
                "String2"
            }));
        }

        [Fact]
        public void TestChangedListsAndNull()
        {
            Assert.False(Compare.CompareList(new List<string>()
            {
                "String1",
                "String2"
            }, (List<string>)null));
        }

        [Fact]
        public void TestTwoLists()
        {
            Assert.True(Compare.CompareList(new List<string>()
            {
                "String1",
                "String2"
            }, new List<string>()
            {
                "String1",
                "String2"
            }));
        }

        [Fact]
        public void TestOneItemLists()
        {
            Assert.True(Compare.CompareList(new List<string>()
            {
                "String1"
            }, new List<string>()
            {
                "String1"
            }));
        }

        [Fact]
        public void TestDiffrentOneItemLists()
        {
            Assert.False(Compare.CompareList(new List<string>()
            {
                "String1"
            }, new List<string>()
            {
                "String2"
            }));
        }

        [Fact]
        public void TestChangedTwoLists()
        {
            Assert.False(Compare.CompareList(new List<string>()
            {
                "String1",
                "String2"
            }, new List<string>()
            {
                "String1",
                "String3"
            }));
        }
    }
}
