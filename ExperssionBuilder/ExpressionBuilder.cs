using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Uhuru.ExpressionBuilder
{

    public static class ExpressionBuilder
    {
        private const string ToUpper = "ToUpper";
        private const string ToLower = "ToLower";
        public const string ChildPropertySeparator = ".";

        public static Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity, TPropertyValue>(string propertyName, TPropertyValue propertyValue, FilterType filterType, ExpressionsBuilderSettings expressionsBuilderSettings = null, string childPropertySeparator = ChildPropertySeparator)
        {
            var argParam = Expression.Parameter(typeof(TEntity), "entity");
            var propInfo = propertyName.GetProperty<TEntity>();
            if (propInfo == null)
            {

                if (propertyName.Contains(childPropertySeparator))
                {
                    var fields = propertyName.Split(new[] { childPropertySeparator }, StringSplitOptions.RemoveEmptyEntries);
                    var subexpression = GetNavigationPropertyExpression<TEntity, TPropertyValue>(argParam, propertyValue, filterType, expressionsBuilderSettings, fields);
                    return subexpression;
                }

                return null;
            }

            ValidatePropertyAndValueTypesMatch(propertyName, propertyValue, propInfo);

            var propertyExp = Expression.Property(argParam, propertyName);
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
            var expression = GetStringExpression(propertyValue, filterType, propInfo, propertyExp, filter,
                expressionsBuilderSettings);

            return Expression.Lambda<Func<TEntity, bool>>(expression, argParam);
        }

        private static Expression GetStringExpression<TPropertyValue>(TPropertyValue propertyValue, FilterType filterType, PropertyInfo propInfo, Expression propertyExp, string filter, ExpressionsBuilderSettings expressionsBuilderSettings = null)
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

            var searchValue = propertyValue.AsString();

            if (!string.IsNullOrWhiteSpace(propertyChangeMethod))
            {
                propertyExp = Expression.Call(propertyExp, propertyChangeMethod, null, null);

                switch (propertyChangeMethod)
                {
                    case ToLower:
                        searchValue = searchValue.ToLowerInvariant();
                        break;
                    case ToUpper:
                        searchValue = searchValue.ToUpperInvariant();
                        break;
                }
            }

            var searchCriteria = searchValue.GetConstant();
            var method = propInfo.PropertyType.GetMethod(filter, new[] { propInfo.PropertyType });
            if (method == null)
            {
                if (filterType == FilterType.NotEqual)
                {
                    propertyExp = Expression.NotEqual(propertyExp, searchCriteria);

                    if (includeNullCheck)
                        propertyExp = Expression.Or(nullCheck, propertyExp);

                    return propertyExp;
                }

                throw new ArgumentException($"{filter} could not be found");
            }

            if (includeNullCheck)
            {
                var notNullAndEqual = Expression.AndAlso(notNullCheck, Expression.Call(propertyExp, method, searchCriteria));
                return notNullAndEqual;
            }

            return Expression.Call(propertyExp, method, searchCriteria);
        }

        private static void ValidatePropertyAndValueTypesMatch<TPropertyValue>(string propertyName, TPropertyValue propertyValue, PropertyInfo propInfo)
        {
            var propertyValueType = typeof(TPropertyValue);
            if (propInfo.PropertyType != propertyValueType)
                throw new ArgumentException($"Type of the property [{propertyName} => {propInfo.PropertyType.Name}] and the value supplied ['{propertyValue}' => {propertyValueType.Name}] are not the same");
        }

        //private static PropertyInfo GetProperty<TEntity>(this string propertyName)
        //    =>  typeof(TEntity).GetProperty(propertyName);

        private static PropertyInfo GetProperty<TEntity>(this string propertyName)
         => GetProperty(typeof(TEntity), propertyName);


        private static PropertyInfo GetProperty(this Type type, string propertyName)
            => type.GetProperties().FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

        private static Expression<Func<TEntity, bool>> GetNavigationPropertyExpression<TEntity, TPropertyValue>(Expression parameter, TPropertyValue searchValue, FilterType filterType, ExpressionsBuilderSettings expressionsBuilderSettings = null, params string[] properties)
        {
            Expression<Func<TEntity, bool>> resultExpression = null;
            Expression navigationPropertyPredicate;
            var providedExpression = parameter;
            var filter = filterType.GetDescription();

            if (properties.Count() > 1)
            {
                //build path
                providedExpression = Expression.Property(parameter, properties[0]);
                var isCollection = typeof(IEnumerable).IsAssignableFrom(providedExpression.Type);
                //if it´s a collection we later need to use the predicate in the methodexpressioncall
                Expression childParameter;
                Type childType = null;
                if (isCollection)
                {
                    childType = providedExpression.Type.GetGenericArguments()[0];
                    childParameter = Expression.Parameter(childType, childType.Name);
                }
                else
                {
                    childType = parameter.Type;
                    childParameter = providedExpression;
                }

                //skip current property and get navigation property expression recursivly
                var innerProperties = properties.Skip(1).ToArray();
                if (isCollection)
                {
                    navigationPropertyPredicate = ExpressionBuilder.GetNavigationPropertyExpression<TEntity, TPropertyValue>(childParameter, searchValue, filterType, expressionsBuilderSettings, innerProperties);
                    //build methodexpressioncall
                    var anyMethod = typeof(Enumerable).GetMethods().Single(m => m.Name == "Any" && m.GetParameters().Length == 2);
                    anyMethod = anyMethod.MakeGenericMethod(childType);
                    var exp = navigationPropertyPredicate.ReduceExtensions();
                    navigationPropertyPredicate = Expression.Call(anyMethod, parameter, navigationPropertyPredicate);
                    resultExpression = MakeLambda<TEntity>(providedExpression, navigationPropertyPredicate);
                }
                else
                {
                    resultExpression = ExpressionBuilder.GetNavigationPropertyExpression<TEntity, TPropertyValue>(childParameter, searchValue, filterType, expressionsBuilderSettings, innerProperties);
                }
            }
            else
            {
                //Formerly from ACLAttribute

                var childProperty = providedExpression.Type.GetProperty(properties[0]);
                if (childProperty == null)
                    return null;

                var searchCriteria = searchValue.GetConstant();

                if (childProperty.PropertyType == typeof(string))
                {
                    var propertyExp = Expression.Property(providedExpression, childProperty);
                    var resultParameterVisitor = new ParameterVisitor();
                    resultParameterVisitor.Visit(parameter);
                    var resultParameter = (ParameterExpression)resultParameterVisitor.Parameter;

                    navigationPropertyPredicate = GetStringExpression(searchValue, filterType, childProperty, propertyExp, filter, expressionsBuilderSettings);
                    resultExpression = Expression.Lambda<Func<TEntity, bool>>(navigationPropertyPredicate, resultParameter);

                    //Expression propertyExp = Expression.Property(providedExpression, childProperty);
                    //propertyExp = Expression.Call(propertyExp, "ToUpper", null, null);
                    //var method = childProperty.PropertyType.GetMethod(filterType.GetDescription(), new[] { childProperty.PropertyType });
                    //navigationPropertyPredicate = Expression.Call(propertyExp, method, searchCriteria);

                    //var resultParameterVisitor = new ParameterVisitor();
                    //resultParameterVisitor.Visit(parameter);
                    //ParameterExpression resultParameter = (ParameterExpression)resultParameterVisitor.Parameter;

                    //resultExpression = Expression.Lambda<Func<TEntity, bool>>(navigationPropertyPredicate, resultParameter);
                }
                else
                {
                    var left = Expression.Property(parameter, childProperty);

                    //var searchCriteria = propertyValue.GetConstant();
                    var right = left.GetPropertyExpressionForNonStringProperty(filterType, searchCriteria);
                    //var right = ExpressionBuilder.GetPropertyExpressionForNonStringProperty(childProperty.PropertyType, left, filterType, searchCriteria);
                    navigationPropertyPredicate = right;// Expression.Equal(left, right);

                    var resultParameterVisitor = new ParameterVisitor();
                    resultParameterVisitor.Visit(parameter);
                    ParameterExpression resultParameter = (ParameterExpression)resultParameterVisitor.Parameter;

                    resultExpression = Expression.Lambda<Func<TEntity, bool>>(navigationPropertyPredicate, resultParameter);
                }
            }

            return resultExpression;
        }

        private static Expression<Func<T, bool>> MakeLambda<T>(Expression parameter, Expression predicate)
        {
            var resultParameterVisitor = new ParameterVisitor();
            resultParameterVisitor.Visit(parameter);
            ParameterExpression resultParameter = (ParameterExpression)resultParameterVisitor.Parameter;
            //return Expression.Lambda(predicate, (ParameterExpression)resultParameter);

            return Expression.Lambda<Func<T, bool>>(Expression.Lambda(predicate, resultParameter), resultParameter);
        }

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

    internal class ParameterVisitor : ExpressionVisitor
    {
        public Expression Parameter
        {
            get;
            private set;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            Parameter = node;
            return node;
        }
    }
}