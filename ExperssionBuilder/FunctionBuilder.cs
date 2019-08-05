using System;

namespace Uhuru.ExpressionBuilder
{
    public static class FunctionBuilder
    {
        public static Func<TEntity, bool> CreateFilterFunc<TEntity, TPropertyValue>(this TPropertyValue propertyValue, string propertyName, FilterType filterType)
            => ExpressionBuilder.CreateFilterExpression<TEntity, TPropertyValue>(propertyName, propertyValue, filterType).Compile();

        public static Func<TEntity, bool> CreateFilterByIdFunc<TEntity, TPropertyValue>(this TPropertyValue propertyValue, string propertyName = "Id", FilterType filterType = FilterType.Equals)
            => CreateFilterFunc<TEntity, TPropertyValue>(propertyValue, propertyName, filterType);
    }
}
