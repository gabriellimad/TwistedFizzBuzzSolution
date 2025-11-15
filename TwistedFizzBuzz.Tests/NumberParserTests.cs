namespace TwistedFizzBuzz.Tests
{
    public class NumberParserTests
    {
        [Trait("Category", "Unit")]
        [Fact]
        public void FromRange_Should_Return_Ascending_Range_When_Start_Less_Than_End()
        {
            long start = 1;
            long end = 5;

            var result = NumberParser.FromRange(start, end);

            Assert.Equal(new long[] { 1, 2, 3, 4, 5 }, result);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void FromRange_Should_Return_Descending_Range_When_Start_Greater_Than_End()
        {
            long start = -2;
            long end = -5;

            var result = NumberParser.FromRange(start, end);

            Assert.Equal(new long[] { -2, -3, -4, -5 }, result);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void FromRange_Should_Return_Single_Value_When_Start_Equals_End()
        {
            long start = 7;
            long end = 7;

            var result = NumberParser.FromRange(start, end);

            Assert.Equal(new long[] { 7 }, result);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void FromList_Should_Return_Exact_Same_List()
        {
            var input = new long[] { -5, 6, 300, 12, 15 };

            var result = NumberParser.FromList(input);

            Assert.Equal(input, result);
        }
    }
}
