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
            Assert.Equal("14M 1d", result);
        }
    }
}
