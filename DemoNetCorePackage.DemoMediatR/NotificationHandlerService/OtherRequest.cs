using MediatR;

namespace DemoNetCorePackage.DemoMediatR.NotificationHandlerService
{
    public class OtherRequest : INotification
    {
        public string? Message { get; set; }
    }
}
