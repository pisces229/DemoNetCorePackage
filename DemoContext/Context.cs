public class Context
{
    private class LogContextData
    {
        public string? TraceId { get; set; }
    }
    private static void Shuffle<T>(IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Shared.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    #region AsyncLocal

    private static readonly AsyncLocal<LogContextData> AsyncLocalData = new();
    private static LogContextData AsyncLocalCurrent
    {
        get
        {
            AsyncLocalData.Value ??= new LogContextData();
            return AsyncLocalData.Value;
        }
        set => AsyncLocalData.Value = value;
    }
    public static void AsyncLocalCreateTraceId() => AsyncLocalCurrent.TraceId ??= Guid.NewGuid().ToString("N");
    public static string AsyncLocalTraceId => AsyncLocalData.Value?.TraceId!;
    public static void AsyncLocalClearTraceId() => AsyncLocalCurrent.TraceId = null;
    public static Task AsyncLocalMain()
    {
        var count = 3;
        var tasks = new List<Task>();
        // Diff Trace Id.
        //Console.WriteLine("Diff Trace Id.");
        //for (var i = 0; i < count; ++i)
        //{
        //    tasks.Add(Task.Run(() => AsyncLocalRun($"Task-{i}")));
        //}
        //Shuffle(tasks);
        //Task.WaitAll([.. tasks]);

        // Same / Diff Trace Id.
        Console.WriteLine("Same / Diff Trace Id.");
        for (var i = 0; i < count; ++i)
        {
            // Same Trace Id.
            //AsyncLocalCreateTraceId();
            //tasks.Add(AsyncLocalRun($"Task-{i}"));

            // Diff Trace Id.
            tasks.Add(AsyncLocalRun($"Task-{i}"));
        }
        Shuffle(tasks);
        Task.WaitAll([.. tasks]);
        return Task.CompletedTask;
    }
    public static async Task AsyncLocalRun(object message)
    {
        AsyncLocalCreateTraceId();
        Console.WriteLine($"{message} : {Thread.CurrentThread.ManagedThreadId} : {AsyncLocalTraceId}");
        await Task.Delay(1);
        AsyncLocalCreateTraceId();
        Console.WriteLine($"{message} : {Thread.CurrentThread.ManagedThreadId} : {AsyncLocalTraceId}");
        //ThreadLocalClearTraceId();
    }
    #endregion

    #region ThreadLocal

    private static readonly ThreadLocal<LogContextData> ThreadLocalData = new();
    private static LogContextData ThreadLocalCurrent
    {
        get
        {
            ThreadLocalData.Value ??= new LogContextData();
            return ThreadLocalData.Value;
        }
        set => ThreadLocalData.Value = value;
    }
    public static void ThreadLocalCreateTraceId() => ThreadLocalCurrent.TraceId ??= Guid.NewGuid().ToString("N");
    public static string ThreadLocalTraceId => ThreadLocalData.Value?.TraceId!;
    public static void ThreadLocalClearTraceId() => AsyncLocalCurrent.TraceId = null;
    public static void ThreadLocalMain()
    {
        var count = 3;
        Console.WriteLine("Diff Trace Id.");
        var threads = new List<Thread>();
        var threadParams = new List<string>();
        for (var i = 0; i < count; ++i)
        {
            threads.Add(new Thread(new ParameterizedThreadStart(ThreadLocalRun!)));
            threadParams.Add($"Thread-{i}");
        }
        Shuffle(threads);
        Shuffle(threadParams);
        for (var i = 0; i < count; ++i)
        {
            threads[i].Start(threadParams[i]);
        }

        //Console.WriteLine("Same Trace Id.");
        //for (var i = 0; i < count; ++i)
        //{
        //    ThreadLocalRun($"Thread-{i}");
        //}
    }
    public static void ThreadLocalRun(object message)
    {
        ThreadLocalCreateTraceId();
        Console.WriteLine($"{message} : {Thread.CurrentThread.ManagedThreadId} : {ThreadLocalTraceId}");
        //ThreadLocalClearTraceId();
    }

    #endregion
}