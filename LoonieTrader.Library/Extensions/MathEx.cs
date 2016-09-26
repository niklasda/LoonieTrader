using System;

namespace LoonieTrader.Library.Extensions
{
    public static class MathEx
    {
        public static long IntPow(long num, long power)
        {
            if (num < 0 || power < 0)
            {
                throw new ArgumentOutOfRangeException(num < 0 ? nameof(num) : nameof(power), "Negative numbers are not supported");
            }

            long result = 1;
            for (long i = 0; i < power; i++)
            {
                result *= num;
            }
            return result;
        }
    }
}