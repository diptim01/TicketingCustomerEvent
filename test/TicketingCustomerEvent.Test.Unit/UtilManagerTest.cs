using FluentAssertions;
using Xunit;

namespace TicketingCustomerEvent.Unit
{
    public class UtilManagerTest
    {
       [Fact]
        public void GetKey_ShouldReturnUniqueKey_WhenCitiesAreGiven()
        {
            var expected = "aaaaabcdefhiilllmnorsstuux";

            var result = UtilManager.GetKey("Texas  California Hubu ", "   Dallas m");

            result.Should().Be(expected)
                .And.HaveLength(26);
        }
    }
}