using MediatR;

namespace DemoNetCorePackage.DemoMediatR.NotificationHandlerService
{
    public class PublishRequest : INotification
    {
        public string? Message { get; set; }
    }
}
