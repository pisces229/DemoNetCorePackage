using System.Linq.Expressions;
using System.Reflection;

namespace DemoNetCorePackage.DemoExpression
{
    internal class RunnerBinaryExpression
    {
        class ValueModel
        { 
            public int Value { get; set; }
        }
        public void Run()
        {
            var model = new ValueModel()
            {
                Value = 1
            };

            Expression<Func<bool>> expressionFunc = () => 1 == model.Value;
            //Expression<Func<bool>> expressionFunc = () => 1 != model.Value;
            //Expression<Func<bool>> expressionFunc = () => 1 > model.Value;
            //Expression<Func<bool>> expressionFunc = () => 1 >= model.Value;
            //Expression<Func<bool>> expressionFunc = () => 1 < model.Value;
            //Expression<Func<bool>> expressionFunc = () => 1 <= model.Value;

            Console.WriteLine($"expressionFunc.NodeType[{expressionFunc.NodeType}]");

            Console.WriteLine($"expressionFunc.Body.NodeType[{expressionFunc.Body.NodeType}]");
            var expressionBody = (BinaryExpression)expressionFunc.Body;

            {
                Console.WriteLine($"expressionBody.Left.NodeType[{expressionBody.Left.NodeType}]");
                var constantExpression = (ConstantExpression)expressionBody.Left;

                Console.WriteLine($"expressionFirst.Value[{constantExpression.Value}]");
            }
            {
                Console.WriteLine($"expressionBody.Right.NodeType[{expressionBody.Right.NodeType}]");
                var expression = (MemberExpression)expressionBody.Right;

                var propertyInfo = (PropertyInfo)expression.Member;

                Console.WriteLine($"expressionFirst.Expression.NodeType[{expression!.Expression!.NodeType}]");
                var memberExpression = (MemberExpression)expression.Expression;

                var fieldInfo = (FieldInfo)memberExpression.Member;

                Console.WriteLine($"expressionSecond.Expression.NodeType[{memberExpression!.Expression!.NodeType}]");
                var constantExpression = (ConstantExpression)memberExpression.Expression;

                var constantExpressionValue = constantExpression.Value!;
                Console.WriteLine(constantExpressionValue.GetType());
                var fieldInfoValue = (ValueModel)fieldInfo.GetValue(constantExpressionValue)!;
                Console.WriteLine(fieldInfoValue.GetType());
                var propertyInfoValue = (int)propertyInfo.GetValue(fieldInfoValue, null)!;
                Console.WriteLine(propertyInfoValue);
            }
        }
    }
}
