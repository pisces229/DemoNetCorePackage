using DemoReactive;

try
{
    var runner = new Runner();
    await runner.Run();
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    Console.WriteLine("Finally");
}