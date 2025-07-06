using DemoAutoMapper;

try
{
    Console.WriteLine("try...");
    var runner = new Runner();
    runner.Run().Wait();
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    Console.WriteLine("finally...");
}