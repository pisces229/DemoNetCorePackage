using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoFluentValidation
{
    public class HostedService(ILogger<HostedService> logger,
        IHostApplicationLifetime applicationLifetime,
        Runner runner) : IHostedService
    {
        private int? _exitCode;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("HostedService Start:[{time}]", DateTimeOffset.Now);
                await runner.Run();
                _exitCode = 1;
            }
            catch (Exception e)
            {
                logger.LogError(e, "HostedService Exception!");
                _exitCode = 1;
            }
            finally
            {
                applicationLifetime.StopApplication();
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("HostedService Stop:[{time}]", DateTimeOffset.Now);
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}
