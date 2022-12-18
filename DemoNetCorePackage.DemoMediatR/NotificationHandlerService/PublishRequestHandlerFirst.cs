using MediatR;

namespace DemoNetCorePackage.DemoMediatR.NotificationHandlerService
{
    public class PublishRequestHandlerFirst : INotificationHandler<PublishRequest>
    {
        public PublishRequestHandlerFirst()
        {
        }
        public Task Handle(PublishRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PublishRequestHandlerFirst GetHashCode: { GetHashCode() } ");
            request.Message += "(First)";
            Console.WriteLine($"PublishRequestHandlerFirst : { request.Message } ");
            return Task.CompletedTask;
        }
    }
}
