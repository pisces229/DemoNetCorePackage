using MediatR;

namespace DemoMediatR.RequestHandlers
{
    //public class SendRequest : IRequest<Unit>
    public class SendRequest : IRequest<SendResponse>
    {
        public string? Message { get; set; }
    }
}
