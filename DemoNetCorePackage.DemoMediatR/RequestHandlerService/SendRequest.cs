using MediatR;

namespace DemoNetCorePackage.DemoMediatR.RequestHandlerService
{
    public class SendRequest : IRequest<SendResponse>
    {
        public string? Message { get; set; }
    }
}
