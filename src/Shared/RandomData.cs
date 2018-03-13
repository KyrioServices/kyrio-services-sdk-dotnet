using System;

namespace Kyrio.Services.Shared
{
    public static class RandomData
    {
        private static readonly System.Random _random = new System.Random();
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

        public static int NextInteger(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public static int NextInteger(int minValue, int maxValue)
        {
            if (maxValue - minValue <= 0)
                return minValue;

            return minValue + _random.Next(maxValue - minValue);
        }

        public static T Pick<T>(T[] values)
        {
            if (values == null || values.Length == 0)
                return default(T);

            return values[NextInteger(values.Length)];
        }

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

        public static bool NextBoolean()
        {
            return _random.Next(100) < 50;
        }

        public static Provider NextProvider()
        {
            return Pick<Provider>(PROVIDERS);
        }

        public static KyrioException NextError()
        {
            return Chance(1, 2)
                ? new KyrioException(ErrorCode.UNKNOWN, 500, "Test error")
                : new KyrioException(ErrorCode.TIMEOUT, 504, "Test timeout");
        }
    }
}
