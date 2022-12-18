using MediatR;

namespace DemoNetCorePackage.DemoMediatR.NotificationHandlerService
{
    public class PublishRequestHandlerSecond : INotificationHandler<PublishRequest>
    {
        public PublishRequestHandlerSecond()
        {
        }
        public Task Handle(PublishRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PublishRequestHandlerSecond GetHashCode: { GetHashCode() } ");
            request.Message += "(Second)";
            Console.WriteLine($"PublishRequestHandlerSecond : { request.Message } ");
            return Task.CompletedTask;
        }
    }
}
