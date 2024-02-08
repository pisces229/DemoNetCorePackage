using MediatR;

namespace DemoMediatR.NotificationHandlers
{
    public class NoNotificationRequest : INotification
    {
        public string? Message { get; set; }
    }
}
