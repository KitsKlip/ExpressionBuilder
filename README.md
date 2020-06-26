ExpressionBuilder
===================

** ExpressionBuilder
A package that helps to create expressions dynamically for unknown type

----------


Features
-------------
* Create Expression on any object at runtime based on property name 
* String comparisons can include
- *DirectComparison* 
- *DirectComparisonWithNullCheck* (NotNull && ...)
- *ToUpper* (value.ToUpper()...)
- *ToUpperWithNullCheck*  (NotNull && value.ToUpper()...)
- *ToLower* (value.ToLower()...)
- *ToLowerWithNullCheck*  (NotNull && value.ToLower()...)


Installing
-------------
Add this **NuGet** dependency to your project: 

```
PM> Install-Package Uhuru.ExpressionBuilder
```
https://www.nuget.org/packages/Uhuru.ExpressionBuilder/

Usage
-------------

##### Usage


```csharp
using Uhuru.ExpressionBuilder;

...

var expression = ExpressionBuilder.CreateFilterExpression<FakeObject, T>(propertyName, propertyValue, filterType, ExpressionsBuilderSettings.Create(stringExpressionsOptions));
```