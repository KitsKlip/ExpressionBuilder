using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Uhuru.ExpressionBuilder
{

    public static class ExpressionBuilder
    {
        private const string ToUpper = "ToUpper";
        private const string ToLower = "ToLower";

        public static Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity, TPropertyValue>(string propertyName, TPropertyValue propertyValue, FilterType filterType, ExpressionsBuilderSettings expressionsBuilderSettings = null)
        {
            var argParam = Expression.Parameter(typeof(TEntity), "entity");
            var propInfo = propertyName.GetProperty<TEntity>();

            ValidatePropertyAndValueTypesMatch(propertyName, propertyValue, propInfo);

            Expression propertyExp = Expression.Property(argParam, propertyName);
            var filter = filterType.GetDescription();

            var searchCriteria = propertyValue.GetConstant();

            if (propInfo.PropertyType == typeof(string))
            {
                return GetStringExpression<TEntity, TPropertyValue>(propertyValue, filterType, argParam, propInfo, propertyExp, filter, expressionsBuilderSettings);
            }

            var propertyExpression = propertyExp.GetPropertyExpressionForNonStringProperty(filterType, searchCriteria);
            return Expression.Lambda<Func<TEntity, bool>>(propertyExpression, argParam);
        }

        private static Expression<Func<TEntity, bool>> GetStringExpression<TEntity, TPropertyValue>(TPropertyValue propertyValue, FilterType filterType, ParameterExpression argParam, PropertyInfo propInfo, Expression propertyExp, string filter, ExpressionsBuilderSettings expressionsBuilderSettings = null)
        {
            var includeNullCheck = false;
            var propertyChangeMethod = string.Empty;
            
            if (expressionsBuilderSettings != null)
            {
                switch (expressionsBuilderSettings.StringExpressionsOptions)
                {
                    case StringExpressionsOptions.ToUpper:
                        propertyChangeMethod = ToUpper;
                        break;
                    case StringExpressionsOptions.ToUpperWithNullCheck:
                        propertyChangeMethod = ToUpper;
                        includeNullCheck = true;
                        break;
                    case StringExpressionsOptions.ToLower:
                        propertyChangeMethod = ToLower;
                        break;
                    case StringExpressionsOptions.ToLowerWithNullCheck:
                        propertyChangeMethod = ToLower;
                        includeNullCheck = true;
                        break;
                    case StringExpressionsOptions.DirectComparison:
                        includeNullCheck = false;
                        propertyChangeMethod = string.Empty;
                        break;
                    case StringExpressionsOptions.DirectComparisonWithNullCheck:
                        includeNullCheck = true;
                        propertyChangeMethod = string.Empty;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var notNullCheck = Expression.NotEqual(propertyExp, Expression.Constant(null, typeof(object)));
            var nullCheck = Expression.Equal(propertyExp, Expression.Constant(null, typeof(object)));

            if (!string.IsNullOrWhiteSpace(propertyChangeMethod))
            {
                propertyExp = Expression.Call(propertyExp, propertyChangeMethod, null, null);
            }
            
            var searchCriteria = propertyValue.AsString().ToUpper().GetConstant();

            var method = propInfo.PropertyType.GetMethod(filter, new[] { propInfo.PropertyType });
            if (method == null)
            {
                if (filterType == FilterType.NotEqual)
                {
                    propertyExp = Expression.NotEqual(propertyExp, searchCriteria);
                    
                    if (includeNullCheck)
                        propertyExp = Expression.Or(nullCheck, propertyExp);

                    return Expression.Lambda<Func<TEntity, bool>>(propertyExp, argParam);
                }
                 
                throw new ArgumentException($"{filter} could not be found");
            }

            if (includeNullCheck)
            {
                var notNullAndEqual = Expression.AndAlso(notNullCheck, Expression.Call(propertyExp, method, searchCriteria));
                return Expression.Lambda<Func<TEntity, bool>>(notNullAndEqual, argParam);
            }

            return Expression.Lambda<Func<TEntity, bool>>(Expression.Call(propertyExp, method, searchCriteria), argParam);
        }

        private static void ValidatePropertyAndValueTypesMatch<TPropertyValue>(string propertyName, TPropertyValue propertyValue, PropertyInfo propInfo)
        {
            var propertyValueType = typeof(TPropertyValue);
            if (propInfo.PropertyType != propertyValueType)
                throw new ArgumentException($"Type of the property [{propertyName} => {propInfo.PropertyType.Name}] and the value supplied ['{propertyValue}' => {propertyValueType.Name}] are not the same");
        }

        private static PropertyInfo GetProperty<TEntity>(this string propertyName)
            => typeof(TEntity).GetProperty(propertyName);

        private static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }

        private static string AsString<TPropertyValue>(this TPropertyValue searchValue)
            => (searchValue == null ? string.Empty : searchValue.ToString());

        private static ConstantExpression GetConstant<TPropertyValue>(this TPropertyValue searchValue)
        {
            try
            {
                return Expression.Constant(searchValue, typeof(TPropertyValue));
            }
            catch
            {
                var propertyType = typeof(TPropertyValue);
                if (typeof(TPropertyValue).IsEnum)
                {
                    var enumTypeValue = Convert.ChangeType(searchValue, Enum.GetUnderlyingType(propertyType));
                    var enumSearchValue = Enum.ToObject(propertyType, enumTypeValue);
                    return Expression.Constant(enumSearchValue, propertyType);
                }

                throw;
            }
        }

        private static BinaryExpression GetPropertyExpressionForNonStringProperty(this Expression propertyExp, FilterType filterType, Expression searchCriteria)
        {
            switch (filterType)
            {
                case FilterType.Equals:
                    return Expression.Equal(propertyExp, searchCriteria);
                case FilterType.GreaterThan:
                    return Expression.GreaterThan(propertyExp, searchCriteria);
                case FilterType.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(propertyExp, searchCriteria);
                case FilterType.LessThan:
                    return Expression.LessThan(propertyExp, searchCriteria);
                case FilterType.LessThanOrEqual:
                    return Expression.LessThanOrEqual(propertyExp, searchCriteria);
                case FilterType.NotEqual:
                    return Expression.NotEqual(propertyExp, searchCriteria);
                case FilterType.Contains:
                case FilterType.EndsWith:
                case FilterType.StartsWith:
                    return Expression.Equal(propertyExp, searchCriteria);
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterType), filterType, null);
            }
        }
    }
}
