using System;

namespace ExpressionBuilderTests
{
    public class FakeChildChildObject
    {
        public Guid GuidField { get; set; } = Guid.Empty;

        public long LongField { get; set; } = FakeObject.LongDefaultValue;

        public double DoubleField { get; set; } = FakeObject.DoubleDefaultValue;

        public int IntegerField { get; set; } = FakeObject.IntDefaultValue;

        public short ShortField { get; set; } = FakeObject.ShortDefaultValue;

        public string StringField { get; set; } = FakeObject.StringTimeDefaultValue;

        public bool BoolField { get; set; } = FakeObject.BoolDefaultValue;

        public decimal DecimalField { get; set; } = FakeObject.DecimalDefaultValue;

        public float FloatField { get; set; } = FakeObject.FloatDefaultValue;

        public DateTime DateTimeField { get; set; } = DateTime.MinValue.ToUniversalTime();
    }
}