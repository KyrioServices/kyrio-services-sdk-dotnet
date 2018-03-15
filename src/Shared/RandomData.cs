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
            new Provider { Id = "1002", Name = "Time Warner Cable" },
            new Provider { Id = "1005", Name = "Comcast" },
            new Provider { Id = "1008", Name = "Adelphia" },
            new Provider { Id = "1010", Name = "Cox Communications" },
            new Provider { Id = "1011", Name = "Charter" },
            new Provider { Id = "1012", Name = "Insight Communications" },
            new Provider { Id = "1014", Name = "Mediacom" },
            new Provider { Id = "1015", Name = "Cablevision" },
            new Provider { Id = "1016", Name = "Cable One" },
            new Provider { Id = "1017", Name = "Bright House Networks" },
            new Provider { Id = "1018", Name = "Suddenlink" },
            new Provider { Id = "1024", Name = "Massillon Cable" },
            new Provider { Id = "1027", Name = "Clear Picture, Inc" },
            new Provider { Id = "1099", Name = "LotsACable" },
            new Provider { Id = "1111", Name = "Ridge Cable" },
            new Provider { Id = "1236", Name = "Mythical Cable" },
            new Provider { Id = "1237", Name = "NewMythical Cable" }
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
