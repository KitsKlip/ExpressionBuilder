using System;
using NUnit.Framework;
using Uhuru.ExpressionBuilder;

namespace ExpressionBuilderTests
{
    public class ExpressionBuilderTests
    {
        [Test]
        [TestCase(FilterType.Equals, "entity => (entity.DecimalField == 74)")]
        [TestCase(FilterType.NotEqual, "entity => (entity.DecimalField != 74)")]
        [TestCase(FilterType.GreaterThan, "entity => (entity.DecimalField > 74)")]
        [TestCase(FilterType.GreaterThanOrEqual, "entity => (entity.DecimalField >= 74)")]
        [TestCase(FilterType.LessThan, "entity => (entity.DecimalField < 74)")]
        [TestCase(FilterType.LessThanOrEqual, "entity => (entity.DecimalField <= 74)")]
        public void Decimal_Expression_Tests(FilterType filterType, string expectedExpressions)
            => RunTest(filterType, FakeObject.DecimalPropertyName, FakeObject.DecimalDefaultValue, expectedExpressions);

        [Test]
        [TestCase(FilterType.Equals, "entity => (entity.GuidField == 5c60f693-bef5-e011-a485-80ee7300c695)")]
        public void Guid_Expression_Tests(FilterType filterType, string expectedExpressions)
            => RunTest(filterType, FakeObject.GuidPropertyName, FakeObject.GuidDefaultValue, expectedExpressions);

        [Test]
        [TestCase(FilterType.Equals, "entity => (entity.DateTimeField == 2019-01-02 03:04:05)")]
        [TestCase(FilterType.NotEqual, "entity => (entity.DateTimeField != 2019-01-02 03:04:05)")]
        [TestCase(FilterType.GreaterThan, "entity => (entity.DateTimeField > 2019-01-02 03:04:05)")]
        [TestCase(FilterType.GreaterThanOrEqual, "entity => (entity.DateTimeField >= 2019-01-02 03:04:05)")]
        [TestCase(FilterType.LessThan, "entity => (entity.DateTimeField < 2019-01-02 03:04:05)")]
        [TestCase(FilterType.LessThanOrEqual, "entity => (entity.DateTimeField <= 2019-01-02 03:04:05)")]
        public void DateTime_Expression_Tests(FilterType filterType, string expectedExpressions)
            => RunTest(filterType, FakeObject.DateTimePropertyName, FakeObject.DateTimeDefaultValue,
                expectedExpressions);

        [Test]
        /*long*/
        [TestCase(FilterType.Equals, "entity => (entity.LongField == 77)", FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        [TestCase(FilterType.NotEqual, "entity => (entity.LongField != 77)", FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        [TestCase(FilterType.GreaterThan, "entity => (entity.LongField > 77)", FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        [TestCase(FilterType.GreaterThanOrEqual, "entity => (entity.LongField >= 77)", FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        [TestCase(FilterType.LessThan, "entity => (entity.LongField < 77)", FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        [TestCase(FilterType.LessThanOrEqual, "entity => (entity.LongField <= 77)", FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        /*double*/
        [TestCase(FilterType.Equals, "entity => (entity.DoubleField == 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        [TestCase(FilterType.NotEqual, "entity => (entity.DoubleField != 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        [TestCase(FilterType.GreaterThan, "entity => (entity.DoubleField > 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        [TestCase(FilterType.GreaterThanOrEqual, "entity => (entity.DoubleField >= 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        [TestCase(FilterType.LessThan, "entity => (entity.DoubleField < 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        [TestCase(FilterType.LessThanOrEqual, "entity => (entity.DoubleField <= 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        /*short*/
        [TestCase(FilterType.Equals, "entity => (entity.ShortField == 75)", FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        [TestCase(FilterType.NotEqual, "entity => (entity.ShortField != 75)", FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        [TestCase(FilterType.GreaterThan, "entity => (entity.ShortField > 75)", FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        [TestCase(FilterType.GreaterThanOrEqual, "entity => (entity.ShortField >= 75)", FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        [TestCase(FilterType.LessThan, "entity => (entity.ShortField < 75)", FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        [TestCase(FilterType.LessThanOrEqual, "entity => (entity.ShortField <= 75)", FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        /*int*/
        [TestCase(FilterType.Equals, "entity => (entity.IntegerField == 73)", FakeObject.IntPropertyName, FakeObject.IntDefaultValue)]
        [TestCase(FilterType.NotEqual, "entity => (entity.IntegerField != 73)", FakeObject.IntPropertyName, FakeObject.IntDefaultValue)]
        [TestCase(FilterType.GreaterThan, "entity => (entity.IntegerField > 73)", FakeObject.IntPropertyName, FakeObject.IntDefaultValue)]
        [TestCase(FilterType.GreaterThanOrEqual, "entity => (entity.IntegerField >= 73)", FakeObject.IntPropertyName, FakeObject.IntDefaultValue)]
        [TestCase(FilterType.LessThan, "entity => (entity.IntegerField < 73)", FakeObject.IntPropertyName, FakeObject.IntDefaultValue)]
        [TestCase(FilterType.LessThanOrEqual, "entity => (entity.IntegerField <= 73)", FakeObject.IntPropertyName, FakeObject.IntDefaultValue)]
        /*float*/
        [TestCase(FilterType.Equals, "entity => (entity.FloatField == 72)", FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
        [TestCase(FilterType.NotEqual, "entity => (entity.FloatField != 72)", FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
        [TestCase(FilterType.GreaterThan, "entity => (entity.FloatField > 72)", FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
        [TestCase(FilterType.GreaterThanOrEqual, "entity => (entity.FloatField >= 72)", FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
        [TestCase(FilterType.LessThan, "entity => (entity.FloatField < 72)", FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
        [TestCase(FilterType.LessThanOrEqual, "entity => (entity.FloatField <= 72)", FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
       /*bool*/
        [TestCase(FilterType.Equals, "entity => (entity.BoolField == True)", FakeObject.BoolPropertyName, FakeObject.BoolDefaultValue)]
        public void Tests<T>(
            FilterType filterType, 
            string expectedExpressions, 
            string propertyName, 
            T propertyValue, 
            StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison
            )
            => RunTest(filterType, propertyName, propertyValue, expectedExpressions, stringExpressionsOptions);


        /*ToUpperWithNullCheck*/
        [TestCase(FilterType.Equals, "entity => entity.StringField.Equals(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.Contains, "entity => entity.StringField.Contains(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.EndsWith, "entity => entity.StringField.EndsWith(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.StartsWith, "entity => entity.StringField.StartsWith(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.NotEqual, "entity => (entity.StringField != \"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]

        /*DirectComparisonWithNullCheck*/
        [TestCase(FilterType.Equals, "entity => ((entity.StringField != null) AndAlso entity.StringField.Equals(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.Contains, "entity => ((entity.StringField != null) AndAlso entity.StringField.Contains(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.EndsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.EndsWith(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.StartsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.StartsWith(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.NotEqual, "entity => ((entity.StringField == null) Or (entity.StringField != \"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        /*ToUpper*/
        [TestCase(FilterType.Equals, "entity => entity.StringField.ToUpper().Equals(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpper)]
        [TestCase(FilterType.Contains, "entity => entity.StringField.ToUpper().Contains(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpper)]
        [TestCase(FilterType.EndsWith, "entity => entity.StringField.ToUpper().EndsWith(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpper)]
        [TestCase(FilterType.StartsWith, "entity => entity.StringField.ToUpper().StartsWith(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpper)]
        [TestCase(FilterType.NotEqual, "entity => (entity.StringField.ToUpper() != \"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpper)]
        /*ToUpperWithNullCheck*/
        [TestCase(FilterType.Equals, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToUpper().Equals(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpperWithNullCheck)]
        [TestCase(FilterType.Contains, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToUpper().Contains(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpperWithNullCheck)]
        [TestCase(FilterType.EndsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToUpper().EndsWith(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpperWithNullCheck)]
        [TestCase(FilterType.StartsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToUpper().StartsWith(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpperWithNullCheck)]
        [TestCase(FilterType.NotEqual, "entity => ((entity.StringField == null) Or (entity.StringField.ToUpper() != \"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToUpperWithNullCheck)]
        /*ToLower*/
        [TestCase(FilterType.Equals, "entity => entity.StringField.ToLower().Equals(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.Contains, "entity => entity.StringField.ToLower().Contains(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.EndsWith, "entity => entity.StringField.ToLower().EndsWith(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.StartsWith, "entity => entity.StringField.ToLower().StartsWith(\"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.NotEqual, "entity => (entity.StringField.ToLower() != \"STRING FIELD VALUE\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        /*ToLowerWithNullCheck*/
        [TestCase(FilterType.Equals, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().Equals(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.Contains, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().Contains(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.EndsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().EndsWith(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.StartsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().StartsWith(\"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.NotEqual, "entity => ((entity.StringField == null) Or (entity.StringField.ToLower() != \"STRING FIELD VALUE\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        public void StringTests<T>(
            FilterType filterType,
            string expectedExpressions,
            string propertyName,
            T propertyValue,
            StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison)
            => RunTest(filterType, propertyName, propertyValue, expectedExpressions, stringExpressionsOptions);

        private static void RunTest<T>(
            FilterType filterType, 
            string propertyName, 
            T propertyValue, 
            string expectedExpression, 
            StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison
            )
        {
            var expression = ExpressionBuilder.CreateFilterExpression<FakeObject, T>(propertyName, propertyValue, filterType, ExpressionsBuilderSettings.Create(stringExpressionsOptions));
            var stringExpression = expression.ToString();
            Assert.AreEqual(expectedExpression, stringExpression);
        }
    }

    internal class FakeObject
    {
        public const string LongPropertyName = "LongField";
        public const long LongDefaultValue = 77;

        public const string DoublePropertyName = "DoubleField";
        public const double DoubleDefaultValue = 76;

        public const string ShortPropertyName = "ShortField";
        public const short ShortDefaultValue = 75;

        public const string DecimalPropertyName = "DecimalField";
        public const decimal DecimalDefaultValue = 74m;

        public const string IntPropertyName = "IntegerField";
        public const int IntDefaultValue = 73;

        public const string FloatPropertyName = "FloatField";
        public const float FloatDefaultValue = 72f;

        public const string StringPropertyName = "StringField";
        public const string StringTimeDefaultValue = "String Field Value";

        public const string BoolPropertyName = "BoolField";
        public const bool BoolDefaultValue = true;

        public const string GuidPropertyName = "GuidField";
        public static readonly Guid GuidDefaultValue = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695");

        public const string DateTimePropertyName = "DateTimeField";
        public static readonly DateTime DateTimeDefaultValue = DateTime.Parse("2019-01-02 03:04:05");

        public Guid GuidField { get; set; } = Guid.Empty;

        public long LongField { get; set; } = LongDefaultValue;

        public double DoubleField { get; set; } = DoubleDefaultValue;

        public int IntegerField { get; set; } = IntDefaultValue;

        public short ShortField { get; set; } = ShortDefaultValue;

        public string StringField { get; set; } = StringTimeDefaultValue;

        public bool BoolField { get; set; } = BoolDefaultValue;

        public decimal DecimalField { get; set; } = DecimalDefaultValue;

        public float FloatField { get; set; } = FloatDefaultValue;

        public DateTime DateTimeField { get; set; } = DateTime.MinValue.ToUniversalTime();
       
    }
}