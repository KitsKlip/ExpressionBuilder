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
        [TestCase(FilterType.Equals, "entity => entity.StringField.Equals(\"String Field Value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.Contains, "entity => entity.StringField.Contains(\"String Field Value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.EndsWith, "entity => entity.StringField.EndsWith(\"String Field Value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.StartsWith, "entity => entity.StringField.StartsWith(\"String Field Value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.NotEqual, "entity => (entity.StringField != \"String Field Value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]

        /*DirectComparisonWithNullCheck*/
        [TestCase(FilterType.Equals, "entity => ((entity.StringField != null) AndAlso entity.StringField.Equals(\"String Field Value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.Contains, "entity => ((entity.StringField != null) AndAlso entity.StringField.Contains(\"String Field Value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.EndsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.EndsWith(\"String Field Value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.StartsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.StartsWith(\"String Field Value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
        [TestCase(FilterType.NotEqual, "entity => ((entity.StringField == null) Or (entity.StringField != \"String Field Value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparisonWithNullCheck)]
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
        [TestCase(FilterType.Equals, "entity => entity.StringField.ToLower().Equals(\"string field value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.Contains, "entity => entity.StringField.ToLower().Contains(\"string field value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.EndsWith, "entity => entity.StringField.ToLower().EndsWith(\"string field value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.StartsWith, "entity => entity.StringField.ToLower().StartsWith(\"string field value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        [TestCase(FilterType.NotEqual, "entity => (entity.StringField.ToLower() != \"string field value\")", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLower)]
        /*ToLowerWithNullCheck*/
        [TestCase(FilterType.Equals, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().Equals(\"string field value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.Contains, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().Contains(\"string field value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.EndsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().EndsWith(\"string field value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.StartsWith, "entity => ((entity.StringField != null) AndAlso entity.StringField.ToLower().StartsWith(\"string field value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        [TestCase(FilterType.NotEqual, "entity => ((entity.StringField == null) Or (entity.StringField.ToLower() != \"string field value\"))", FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.ToLowerWithNullCheck)]
        public void StringTests<T>(
            FilterType filterType,
            string expectedExpressions,
            string propertyName,
            T propertyValue,
            StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison)
            => RunTest(filterType, propertyName, propertyValue, expectedExpressions, stringExpressionsOptions);

        [TestCase(FilterType.Equals, "entity => entity.SingleFirstLevelChild.StringField.Equals(\"String Field Value\")",
            FakeObject.SingleFirstLevelChildName, FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue)]
        [TestCase(FilterType.NotEqual, "entity => (entity.SingleFirstLevelChild.StringField != \"String Field Value\")",
            FakeObject.SingleFirstLevelChildName, FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue)]

        [TestCase(FilterType.Equals, "entity => entity.SingleFirstLevelChild.StringField.Equals(\"String Field Value\")", FakeObject.SingleFirstLevelChildName, FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.Contains, "entity => entity.SingleFirstLevelChild.StringField.Contains(\"String Field Value\")", FakeObject.SingleFirstLevelChildName, FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.EndsWith, "entity => entity.SingleFirstLevelChild.StringField.EndsWith(\"String Field Value\")", FakeObject.SingleFirstLevelChildName, FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.StartsWith, "entity => entity.SingleFirstLevelChild.StringField.StartsWith(\"String Field Value\")", FakeObject.SingleFirstLevelChildName, FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.NotEqual, "entity => (entity.SingleFirstLevelChild.StringField != \"String Field Value\")", FakeObject.SingleFirstLevelChildName, FakeObject.StringPropertyName, FakeObject.StringTimeDefaultValue, StringExpressionsOptions.DirectComparison)]

        public void ChildrenStringFieldTest<T>(
            FilterType filterType,
            string expectedExpressions,
            string childPropertyName,
            string propertyName,
            T propertyValue,
            StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison,
            string propertySeparator = ".")
        {
            var propertyNameWithChild = $"{childPropertyName}{propertySeparator}{propertyName}";
             RunTest(filterType, propertyNameWithChild, propertyValue, expectedExpressions, stringExpressionsOptions);
        }

        [TestCase(FilterType.NotEqual, "entity => (entity.SingleFirstLevelChild.IntegerField != 73)", FakeObject.SingleFirstLevelChildName, FakeObject.IntPropertyName, FakeObject.IntDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.LongField == 77)", FakeObject.SingleFirstLevelChildName, FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.DoubleField == 76)", FakeObject.SingleFirstLevelChildName, FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.ShortField == 75)", FakeObject.SingleFirstLevelChildName, FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.FloatField == 72)", FakeObject.SingleFirstLevelChildName, FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.BoolField == True)", FakeObject.SingleFirstLevelChildName, FakeObject.BoolPropertyName, FakeObject.BoolDefaultValue)]
        public void ChildrenNonStringFieldTest<T>(
            FilterType filterType,
            string expectedExpressions,
            string childPropertyName,
            string propertyName,
            T propertyValue,
            StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison,
            string propertySeparator = ".")
        {
            var propertyNameWithChild = $"{childPropertyName}{propertySeparator}{propertyName}";
            RunTest(filterType, propertyNameWithChild, propertyValue, expectedExpressions, stringExpressionsOptions);
        }

        [TestCase(FilterType.NotEqual, "entity => (entity.SingleFirstLevelChild.SingleSecondLevelChild.IntegerField != 73)", FakeObject.IntPropertyName, FakeObject.IntDefaultValue, StringExpressionsOptions.DirectComparison)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.SingleSecondLevelChild.LongField == 77)", FakeObject.LongPropertyName, FakeObject.LongDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.SingleSecondLevelChild.DoubleField == 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.SingleSecondLevelChild.ShortField == 75)",  FakeObject.ShortPropertyName, FakeObject.ShortDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.SingleSecondLevelChild.FloatField == 72)",  FakeObject.FloatPropertyName, FakeObject.FloatDefaultValue)]
        [TestCase(FilterType.Equals, "entity => (entity.SingleFirstLevelChild.SingleSecondLevelChild.BoolField == True)",  FakeObject.BoolPropertyName, FakeObject.BoolDefaultValue)]
        public void ChildrenChildrenNonStringFieldTest<T>(
            FilterType filterType,
            string expectedExpressions,
            string propertyName,
            T propertyValue,
            StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison,
            string propertySeparator = ".")
        {
            var propertyNameWithChild = $"{FakeObject.SingleFirstLevelChildName}{propertySeparator}{nameof(FakeChildObject.SingleSecondLevelChild)}{propertySeparator}{propertyName}";
            RunTest(filterType, propertyNameWithChild, propertyValue, expectedExpressions, stringExpressionsOptions);
        }

        //[TestCase(FilterType.Equals, "entity => (entity.ListOfFirstLevelChildren.SingleSecondLevelChild.DoubleField == 76)", FakeObject.DoublePropertyName, FakeObject.DoubleDefaultValue)]
        
        //public void ChildrenCollectionTest<T>(
        //    FilterType filterType,
        //    string expectedExpressions,
        //    string propertyName,
        //    T propertyValue,
        //    StringExpressionsOptions stringExpressionsOptions = StringExpressionsOptions.DirectComparison,
        //    string propertySeparator = ".")
        //{
        //    var propertyNameWithChild = $"{nameof(FakeObject.ListOfFirstLevelChildren)}{propertySeparator}{propertySeparator}{propertyName}";
        //    RunTest(filterType, propertyNameWithChild, propertyValue, expectedExpressions, stringExpressionsOptions);
        //}

        //"entity => ((entity.SingleFirstLevelChild.StringField == null) Or (entity.SingleFirstLevelChild.StringField.ToLower() != \"string field value\"))"
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
}