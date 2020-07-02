using System.Linq.Expressions;

namespace Uhuru.ExpressionBuilder
{
    internal class ParameterVisitor : ExpressionVisitor
    {
        public Expression Parameter
        {
            get;
            private set;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            this.Parameter = node;
            return node;
        }
    }
}