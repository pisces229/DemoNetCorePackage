using DemoDocumentFormatOpenXml.Runners;

try
{
    var runner = new PieChartCloneRunner();
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    Console.WriteLine("Finally");
}