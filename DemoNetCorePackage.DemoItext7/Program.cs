﻿using DemoNetCorePackage.DemoItext7;

try
{
    var runner = new Runner();
    runner.Run().Wait();
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    Console.WriteLine("Finally");
}