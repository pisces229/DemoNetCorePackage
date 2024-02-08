using MediatR;

namespace DemoMediatR.NotificationHandlers
{
    public class NotificationRequestHandlerFirst : INotificationHandler<NotificationRequest>
    {
        //public NotificationRequestHandlerFirst()
        //{
        //}
        public Task Handle(NotificationRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"NotificationRequestHandlerFirst GetHashCode: { GetHashCode() } ");
            request.Message += "(First)";
            Console.WriteLine($"NotificationRequestHandlerFirst : { request.Message } ");
            return Task.CompletedTask;
        }
    }
}
