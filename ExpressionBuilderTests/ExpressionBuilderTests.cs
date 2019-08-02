using NUnit.Framework;
using Uhuru.ExpressionBuilder;

namespace ExpressionBuilderTests
{
    public class ExpressionBuilderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var expression = ExpressionBuilder.CreateFilterExpression<>()
            Assert.Pass();
        }
    }
}