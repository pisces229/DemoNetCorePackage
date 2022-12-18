using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DemoNetCorePackage.DemoExpression
{
    internal class RunnerExpression
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
            var stringBuilder = new StringBuilder();
            var hashtable = new Hashtable();
            First<int>(stringBuilder, hashtable, "Text", (e) => e == 1);
            First<string>(stringBuilder, hashtable, "Text", (e) => e == "1");
            First<char>(stringBuilder, hashtable, "Text", (e) => e > '1');
        }

        public void First<T>(StringBuilder stringBuilder, Hashtable hashtable, string name,
            Expression<Func<T, bool>> expression)
        { 
        
        }
        public void Second<T>(StringBuilder stringBuilder, Hashtable hashtable, string name,
            Expression<Func<T, bool>> expression)
        {

        }
    }
}
