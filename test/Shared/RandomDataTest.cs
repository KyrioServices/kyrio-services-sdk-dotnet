using Xunit;

namespace Kyrio.Services.Shared
{
    public class RandomDataTest
    {
        [Fact]
        public void TestNextInteger()
        {
            var value1 = RandomData.NextInteger(0, 100);
            var value2 = RandomData.NextInteger(100);
            var value3 = RandomData.NextInteger(100);

            Assert.True(value1 != value2 || value2 != value3);
        }

        [Fact]
        public void TestNextBoolean()
        {
            var value1 = RandomData.NextBoolean();
            var value2 = RandomData.NextBoolean();
            var value3 = RandomData.NextBoolean();

            //Assert.True(value1 != value2 || value2 != value3);
        }

        [Fact]
        public void TestChance()
        {
            var value1 = RandomData.Chance(1, 10);
            var value2 = RandomData.Chance(1, 10);
            var value3 = RandomData.Chance(1, 10);

            //Assert.True(value1 != value2 || value2 != value3);
        }

        [Fact]
        public void TestPick()
        {
            var value1 = RandomData.Pick<int>(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            var value2 = RandomData.Pick<int>(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            var value3 = RandomData.Pick<int>(new int[] { 1, 2, 3, 4, 5, 6, 7 });

            Assert.True(value1 != value2 || value2 != value3);
        }

        [Fact]
        public void TestNextProvider()
        {
            var value1 = RandomData.NextProvider();
            var value2 = RandomData.NextProvider();
            var value3 = RandomData.NextProvider();

            Assert.True(value1.Id != value2.Id || value2.Id != value3.Id);
        }
    }
}
