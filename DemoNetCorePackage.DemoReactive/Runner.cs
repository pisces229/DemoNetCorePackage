using System.Reactive.Linq;

namespace DemoNetCorePackage.DemoReactive
{
    internal class Runner
    {
        public Runner() 
        {            
        }
        public Task Run() 
        {
            //{
            //    Observable
            //        .Interval(TimeSpan.FromSeconds(1))
            //        // .Do(
            //        //    value => { if (value == 5) throw new Exception(); }
            //        //)
            //        .Retry(3)
            //        .Subscribe(
            //            value => Console.WriteLine(value),
            //            exception => Console.WriteLine(exception),
            //            () => Console.WriteLine("Completed")
            //        );
            //}

            {
                Observable
                    .Concat(Observable.Return(1), Observable.Return(2), Observable.Return(3), Observable.Return(4))
                    //.Do(
                    //    value => { if (value == 3) throw new Exception(); }
                    //)
                    .Retry(3)
                    .Subscribe(
                        value => Console.WriteLine(value),
                        exception => Console.WriteLine(exception),
                        () => Console.WriteLine("Completed")
                    );
            }

            //{
            //    var subject = new Subject<int>();
            //    subject.Subscribe(
            //        value => Console.WriteLine(value),
            //        exception => Console.WriteLine(exception),
            //        () => Console.WriteLine("Completed")
            //    );
            //    subject.OnNext(1);
            //    subject.OnNext(2);
            //    subject.OnNext(3);
            //}
            return Task.CompletedTask;
        }
    }
}
