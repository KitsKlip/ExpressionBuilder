using System;
using System.Collections.Generic;

namespace ExpressionBuilderTests
{
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

        public const string SingleFirstLevelChildName = "SingleFirstLevelChild";

        public const string ListOfFirstLevelChildrenName = "ListOfFirstLevelChildren";

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

        public FakeChildObject SingleFirstLevelChild { get; set; } = new FakeChildObject();

        public IEnumerable<FakeChildObject> ListOfFirstLevelChildren { get; set; } = new List<FakeChildObject>()
            {new FakeChildObject(), new FakeChildObject(), new FakeChildObject()};

    }
}