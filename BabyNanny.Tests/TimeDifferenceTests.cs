using System;
using BabyNanny.Helpers;
using Xunit;

namespace BabyNanny.Tests
{
    public class TimeDifferenceTests
    {
        [Fact]
        public void Seconds_AreFormattedCorrectly()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = start.AddSeconds(45);
            var result = TimeUtils.TimeDifference(start, end);
            Assert.Equal("45s", result);
        }

        [Fact]
        public void Minutes_AreFormattedCorrectly()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = start.AddMinutes(2).AddSeconds(30);
            var result = TimeUtils.TimeDifference(start, end);
            Assert.Equal("2m 30s", result);
        }

        [Fact]
        public void Months_AreFormattedCorrectly()
        {
            var start = new DateTime(2023, 1, 15);
            var end = new DateTime(2024, 3, 16);
            var result = TimeUtils.TimeDifference(start, end);
            Assert.Equal("14M 6d", result);
        }

        [Fact]
        public void Hours_AreFormattedCorrectly()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = start.AddHours(1).AddMinutes(15);
            var result = TimeUtils.TimeDifference(start, end);
            Assert.Equal("1h 15m", result);
        }

        [Fact]
        public void Days_AreFormattedCorrectly()
        {
            var start = new DateTime(2024, 1, 1, 8, 0, 0);
            var end = start.AddDays(2).AddHours(3);
            var result = TimeUtils.TimeDifference(start, end);
            Assert.Equal("2d 3h", result);
        }

        [Fact]
        public void NullStart_ReturnsInvalidMessage()
        {
            var result = TimeUtils.TimeDifference(null, DateTime.Now);
            Assert.Equal("Invalid start time", result);
        }

        [Fact]
        public void MixedUnits_AreFormattedInOrder()
        {
            var start = new DateTime(2024, 1, 1, 0, 0, 0);
            var end = start
                .AddMonths(1)
                .AddDays(2)
                .AddHours(4)
                .AddMinutes(5)
                .AddSeconds(6);
            var result = TimeUtils.TimeDifference(start, end);
            Assert.Equal("1M 3d 4h 5m 6s", result);
        }
    }
}
