﻿using Models.Dates;
using NUnit.Framework;
using System;

namespace Models.Tests.Dates
{
    [TestFixture]
    public class SpecificDateTimeTests
    {
        [Test]
        public void SpecificDateTime_CtorWithDate_DateSet()
        {
            SpecificDateTime date = new SpecificDateTime(DateTime.Parse("01-Mar-2020"));
            Assert.AreEqual(DateTime.Parse("01-Mar-2020"), date.ExpectedDate);
        }

        [Test]
        public void SpecificDateTime_CtorWithDateAsString_DateSet()
        {
            SpecificDateTime date = new SpecificDateTime("01-Mar-2020");
            Assert.AreEqual(DateTime.Parse("01-Mar-2020"), date.ExpectedDate);
        }

        [Test]
        public void SpecificDateTime_Ctor_DefaultPrecisionIs1Second()
        {
            SpecificDateTime date = new SpecificDateTime(DateTime.Parse("01-Mar-2020"));
            Assert.AreEqual(TimeSpan.FromSeconds(1), date.Precision);
        }

        [TestCase("01-Mar-2020", "01-Mar-2020")]
        [TestCase("01-Mar-2020 15:12:31", "01-Mar-2020 15:12:31")]
        public void SpecificDateTime_AssertDate_SameDate_NoError(string expectedDateString, string actualDateString)
        {
            DateTime expectedDate = DateTime.Parse(expectedDateString);
            DateTime actualDate = DateTime.Parse(actualDateString);

            Assert.DoesNotThrow(() =>
                new SpecificDateTime(expectedDate)
                    .AssertDate(actualDate, "DateTime is wrong: {0}"));
        }

        [TestCase("02-Mar-2020", "01-Mar-2020", "Assert.AreEqual failed. Expected:<02/03/2020 00:00:00>. Actual:<01/03/2020 00:00:00>. DateTime is wrong: -86400000 ms")]
        [TestCase("01-Mar-2020", "02-Mar-2020", "Assert.AreEqual failed. Expected:<01/03/2020 00:00:00>. Actual:<02/03/2020 00:00:00>. DateTime is wrong: 86400000 ms")]
        [TestCase("03-Mar-2020", "01-Mar-2020", "Assert.AreEqual failed. Expected:<03/03/2020 00:00:00>. Actual:<01/03/2020 00:00:00>. DateTime is wrong: -172800000 ms")]
        [TestCase("01-Mar-2020", "03-Mar-2020", "Assert.AreEqual failed. Expected:<01/03/2020 00:00:00>. Actual:<03/03/2020 00:00:00>. DateTime is wrong: 172800000 ms")]
        [TestCase("01-Mar-2020 08:12:34", "03-Mar-2020 15:12:31", "Assert.AreEqual failed. Expected:<01/03/2020 08:12:34>. Actual:<03/03/2020 15:12:31>. DateTime is wrong: 197997000 ms")]
        [TestCase("01-Mar-2020 09:12:31", "01-Mar-2020 21:23:54", "Assert.AreEqual failed. Expected:<01/03/2020 09:12:31>. Actual:<01/03/2020 21:23:54>. DateTime is wrong: 43883000 ms")]
        [TestCase("01-Mar-2020 00:00:00", "01-Mar-2020 23:59:59", "Assert.AreEqual failed. Expected:<01/03/2020 00:00:00>. Actual:<01/03/2020 23:59:59>. DateTime is wrong: 86399000 ms")]
        [TestCase("01-Mar-2020 00:00:00", "01-Mar-2020 00:00:01", "Assert.AreEqual failed. Expected:<01/03/2020 00:00:00>. Actual:<01/03/2020 00:00:01>. DateTime is wrong: 1000 ms")]
        [TestCase("01-Mar-2020 00:00:01", "01-Mar-2020 00:00:00", "Assert.AreEqual failed. Expected:<01/03/2020 00:00:01>. Actual:<01/03/2020 00:00:00>. DateTime is wrong: -1000 ms")]
        public void SpecificDateTime_AssertDate_DifferentTimes_DefaultPrecision_Error(string expectedDateString, string actualDateString, string expectedMessage)
        {
            DateTime expectedDate = DateTime.Parse(expectedDateString);
            DateTime actualDate = DateTime.Parse(actualDateString);

            Exception ex = Assert.Throws<Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException>(() =>
                new SpecificDateTime(expectedDate)
                    .AssertDate(actualDate, "DateTime is wrong: {0}"));

            Assert.AreEqual(ex.Message, expectedMessage);
        }
    }
}