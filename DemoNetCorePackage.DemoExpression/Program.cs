using DemoNetCorePackage.DemoExpression;

try
{
    //new RunnerBinaryExpression().Run();

    new RunnerStringBinaryExpression().Run();

    //new RunnerMethodCallExpression().Run();

    //new RunnerUnaryExpression().Run();
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    Console.WriteLine("Finally");
}