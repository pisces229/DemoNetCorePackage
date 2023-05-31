using System.Linq.Expressions;
using System.Reflection;

namespace DemoExpression
{
    internal class RunnerMethodCallExpression
    {
        class ValueModel
        { 
            public string? Value { get; set; }
        }
        public void Run()
        {
            var model = new ValueModel()
            {
                Value = "1" 
            };

            Expression<Func<bool>> expressionFunc = () => "1".Equals(model.Value);
            //Expression<Func<bool>> expressionFunc = () => "1".StartsWith(model.Value);
            //Expression<Func<bool>> expressionFunc = () => "1".EndsWith(model.Value);
            //Expression<Func<bool>> expressionFunc = () => !"1".Contains(model.Value);

            Console.WriteLine($"expressionFunc.NodeType[{expressionFunc.NodeType}]");

            Console.WriteLine($"expressionFunc.NodeType[{expressionFunc.Body.NodeType}]");
            var expressionBody = (MethodCallExpression)expressionFunc.Body;

            Console.WriteLine($"expression[{expressionBody.Method.Name}]");
            {
                Console.WriteLine($"expressionBody!.Object!.NodeType[{expressionBody!.Object!.NodeType}]");
                var constantExpression = (ConstantExpression)expressionBody!.Object!;

                Console.WriteLine($"constantExpression.Value[{constantExpression.Value}]");
            }
            {
                var argumentsMemberExpression = (MemberExpression)expressionBody.Arguments.First();
                Console.WriteLine($"argumentsMemberExpression.NodeType[{argumentsMemberExpression.NodeType}]");
                var propertyInfo = (PropertyInfo)argumentsMemberExpression.Member;

                Console.WriteLine($"argumentsMemberExpression.Expression.NodeType[{argumentsMemberExpression!.Expression!.NodeType}]");
                var memberExpression = (MemberExpression)argumentsMemberExpression!.Expression!;

                var fieldInfo = (FieldInfo)memberExpression.Member;

                Console.WriteLine($"memberExpression.Expression.NodeType[{memberExpression!.Expression!.NodeType}]");
                var constantExpression = (ConstantExpression)memberExpression.Expression;

                var constantExpressionValue = constantExpression.Value!;
                Console.WriteLine(constantExpressionValue.GetType());
                var fieldInfoValue = (ValueModel)fieldInfo.GetValue(constantExpressionValue)!;
                Console.WriteLine(fieldInfoValue.GetType());
                var propertyInfoValue = (string)propertyInfo.GetValue(fieldInfoValue, null)!;
                Console.WriteLine(propertyInfoValue);
            }
        }
    }
}
