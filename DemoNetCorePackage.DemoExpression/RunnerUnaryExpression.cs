using System.Linq.Expressions;
using System.Reflection;

namespace DemoNetCorePackage.DemoExpression
{
    internal class RunnerUnaryExpression
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

            Expression<Func<bool>> expressionFunc = () => !"1".Equals(model.Value);
            //Expression<Func<bool>> expressionFunc = () => !"1".StartsWith(model.Value);
            //Expression<Func<bool>> expressionFunc = () => !"1".EndsWith(model.Value);
            //Expression<Func<bool>> expressionFunc = () => !"1".Contains(model.Value);

            Console.WriteLine($"expressionFunc.NodeType[{expressionFunc.NodeType}]");

            Console.WriteLine($"expressionFunc.Body.NodeType[{expressionFunc.Body.NodeType}]");
            var expressionBody = (UnaryExpression)expressionFunc.Body;

            Console.WriteLine($"expressionFunc.NodeType[{expressionBody.Operand.NodeType}]");
            var expressionBodyOperand = (MethodCallExpression)expressionBody.Operand;

            Console.WriteLine($"expression[{expressionBodyOperand.Method.Name}]");
            {
                Console.WriteLine($"expressionBody!.Object!.NodeType[{expressionBodyOperand!.Object!.NodeType}]");
                var constantExpression = (ConstantExpression)expressionBodyOperand!.Object!;

                Console.WriteLine($"constantExpression.Value[{constantExpression.Value}]");
            }
            {
                var argumentsMemberExpression = (MemberExpression)expressionBodyOperand.Arguments.First();
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
