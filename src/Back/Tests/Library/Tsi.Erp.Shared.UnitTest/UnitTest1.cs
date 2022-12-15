using FluentAssertions;

namespace Tsi.Erp.Shared.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            1.Should().Be(1);
            //1.Should().Be(2);
        }
    }
}