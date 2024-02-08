using MediatR;

namespace DemoMediatR.NotificationHandlers
{
    public class NotificationRequest : INotification
    {
        public string? Message { get; set; }
    }
}
