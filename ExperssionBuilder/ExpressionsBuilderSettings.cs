namespace Uhuru.ExpressionBuilder
{
    public class ExpressionsBuilderSettings
    {
        public StringExpressionsOptions StringExpressionsOptions { get; set; } = StringExpressionsOptions.ToUpperWithNullCheck;

        public static ExpressionsBuilderSettings Default = new ExpressionsBuilderSettings();

        public static ExpressionsBuilderSettings Create(StringExpressionsOptions stringExpressionsOptions)
            => new ExpressionsBuilderSettings
            {
                StringExpressionsOptions = stringExpressionsOptions
            };
    }
}