using Microsoft.Extensions.Logging;

namespace DemoFluentValidation
{
    public class DbContext(ILogger<DbContext> logger)
    {
        public void Run()
        {
            logger.LogInformation("DbContext.Run...");
        }
        public Task RunAsync()
        {
            logger.LogInformation("DbContext.RunAsync...");
            return Task.CompletedTask;
        }
    }
}
