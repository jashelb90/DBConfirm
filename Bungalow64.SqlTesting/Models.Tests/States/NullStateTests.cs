﻿using Models.States;
using Models.TestFrameworks.Abstract;
using NUnit.Framework;
using System;

namespace Models.Tests.States
{
    [TestFixture]
    public class NullStateTests
    {
        private readonly ITestFramework _testFramework = new Frameworks.MSTest2.MSTest2Framework();

        [Test]
        public void NullState_HasValue_Error()
        {
            object value = 123;

            var exception = Assert.Throws<Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException>(() => new NullState()
                .AssertState(_testFramework, value, "CustomMessage"));

            Assert.AreEqual("Assert.AreEqual failed. Expected:< (System.DBNull)>. Actual:<123 (System.Int32)>. CustomMessage", exception.Message);
        }

        [Test]
        public void NullState_HasNullValue_NoError()
        {
            object value = null;

            new NullState()
                .AssertState(_testFramework, value, "CustomMessage");
        }

        [Test]
        public void NullState_HasDBNullValue_NoError()
        {
            object value = DBNull.Value;

            new NullState()
                .AssertState(_testFramework, value, "CustomMessage");
        }
    }
}