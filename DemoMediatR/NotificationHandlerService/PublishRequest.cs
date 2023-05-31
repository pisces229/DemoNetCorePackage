using MediatR;

namespace DemoMediatR.NotificationHandlerService
{
    public class PublishRequest : INotification
    {
        public string? Message { get; set; }
    }
}
