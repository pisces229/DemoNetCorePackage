using MediatR;

namespace DemoMediatR.NotificationHandlerService
{
    public class OtherRequest : INotification
    {
        public string? Message { get; set; }
    }
}
