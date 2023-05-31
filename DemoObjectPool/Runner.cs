using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoObjectPool
{
    internal class Runner
    {
        class DefaultObject
        {
            public int Age { get; set; }
            public string? Name { get; set; }
        }
        private readonly DefaultPooledObjectPolicy<DefaultObject> _defaultPooledObjectPolicy;
        private readonly DefaultObjectPool<DefaultObject> _defaultObjectPool;
        public Runner() 
        {
            _defaultPooledObjectPolicy = new DefaultPooledObjectPolicy<DefaultObject>();
            _defaultObjectPool = new DefaultObjectPool<DefaultObject>(_defaultPooledObjectPolicy, 1);
        }
        public Task Run() 
        {
            for (int i = 0; i < 10; i++)
            {
                var pooledObject1 = _defaultObjectPool.Get();
                Console.WriteLine($"#{pooledObject1.GetHashCode()}");
                var pooledObject2 = _defaultObjectPool.Get();
                Console.WriteLine($"#{pooledObject2.GetHashCode()}");
                _defaultObjectPool.Return(pooledObject1);
                _defaultObjectPool.Return(pooledObject2);
            }
            return Task.CompletedTask;
        }
    }
}
