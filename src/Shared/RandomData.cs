using System;

namespace Kyrio.Services.Shared
{
    /// <summary>
    /// Random data generator used to simulate server responses.
    /// </summary>
    public static class RandomData
    {
        private static readonly System.Random _random = new System.Random();

        /// <summary>
        /// All currently connected cable providers
        /// </summary>
        public static Provider[] PROVIDERS = new Provider[]
        {
            new Provider { Id = "2000", Name = "Local Test Cable Provider A" },
            new Provider { Id = "2001", Name = "Local Test Cable Provider B" },
            new Provider { Id = "2002", Name = "Local Test Cable Provider C" },
            new Provider { Id = "2003", Name = "Local Test Cable Provider D" },
            new Provider { Id = "2004", Name = "Local Test Cable Provider E" },
            new Provider { Id = "2005", Name = "Local Test Cable Provider F" },
            new Provider { Id = "2006", Name = "Local Test Cable Provider G" },
            new Provider { Id = "2007", Name = "Local Test Cable Provider H" },
            new Provider { Id = "2008", Name = "Local Test Cable Provider I" },
            new Provider { Id = "2009", Name = "Local Test Cable Provider J" },
            new Provider { Id = "2010", Name = "Local Test Cable Provider K" },
            new Provider { Id = "2011", Name = "Local Test Cable Provider L" },
            new Provider { Id = "2012", Name = "Local Test Cable Provider M" },
            new Provider { Id = "2013", Name = "Local Test Cable Provider N" },
            new Provider { Id = "2014", Name = "Local Test Cable Provider O" },
            new Provider { Id = "2015", Name = "Local Test Cable Provider P" },
            new Provider { Id = "2016", Name = "Local Test Cable Provider Q" }
        };

        /// <summary>
        /// Generates random integer less or equal to max value.
        /// </summary>
        /// <param name="maxValue">A maximum for random values</param>
        /// <returns>A random integer value</returns>
        public static int NextInteger(int maxValue)
        {
            return _random.Next(maxValue);
        }

        /// <summary>
        /// Generates random integer within specified range
        /// </summary>
        /// <param name="minValue">A minimum for random values</param>
        /// <param name="maxValue">A maximum for random values</param>
        /// <returns>A random integer value</returns>
        public static int NextInteger(int minValue, int maxValue)
        {
            if (maxValue - minValue <= 0)
                return minValue;

            return minValue + _random.Next(maxValue - minValue);
        }

        /// <summary>
        /// Picks a random element from values array.
        /// </summary>
        /// <typeparam name="T">Type of random value</typeparam>
        /// <param name="values">Array with possible values</param>
        /// <returns>A random value</returns>
        public static T Pick<T>(T[] values)
        {
            if (values == null || values.Length == 0)
                return default(T);

            return values[NextInteger(values.Length)];
        }

        /// <summary>
        /// Determines a random chance from maximum chances.
        /// </summary>
        /// <param name="chances">Number of chances to test</param>
        /// <param name="maxChances">Maximum number of chances</param>
        /// <returns><code>true</code> is chance happend or <code>false</code> otherwise.</returns>
        public static bool Chance(float chances, float maxChances)
        {
            chances = chances >= 0 ? chances : 0;
            maxChances = maxChances >= 0 ? maxChances : 0;
            if (chances == 0 && maxChances == 0)
                return false;

            maxChances = Math.Max(maxChances, chances);
            double start = (maxChances - chances) / 2;
            double end = start + chances;
            double hit = _random.NextDouble() * maxChances;
            return hit >= start && hit <= end;
        }

        /// <summary>
        /// Generates random boolean value
        /// </summary>
        /// <returns>A random boolean value</returns>
        public static bool NextBoolean()
        {
            return _random.Next(100) < 50;
        }

        /// <summary>
        /// Picks a random cable provider from the list of registered providers.
        /// </summary>
        /// <returns>A random cable provider.</returns>
        public static Provider NextProvider()
        {
            return Pick<Provider>(PROVIDERS);
        }

        /// <summary>
        /// Generates a random error returned by Kyrio services.
        /// </summary>
        /// <returns>A random error</returns>
        public static KyrioException NextError()
        {
            return Chance(1, 2)
                ? new KyrioException(ErrorCode.UNKNOWN, 500, "Test error")
                : new KyrioException(ErrorCode.TIMEOUT, 504, "Test timeout");
        }
    }
}
