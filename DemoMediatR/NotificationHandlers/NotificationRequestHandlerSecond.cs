using MediatR;

namespace DemoMediatR.NotificationHandlers
{
    public class NotificationRequestHandlerSecond : INotificationHandler<NotificationRequest>
    {
        //public NotificationRequestHandlerSecond()
        //{
        //}
        public Task Handle(NotificationRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"NotificationRequestHandlerSecond GetHashCode: { GetHashCode() } ");
            request.Message += "(Second)";
            Console.WriteLine($"NotificationRequestHandlerSecond : { request.Message } ");
            return Task.CompletedTask;
        }
    }
}
