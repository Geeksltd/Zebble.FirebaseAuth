namespace Zebble
{
    using System;
    using Olive;

    static class DateTimeExtensions
    {
        public static DateTimeOffset ToUnixOffset(this string value) => DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(value));

        public static bool IsPast(this DateTimeOffset value) => value < LocalTime.Now;
    }
}
