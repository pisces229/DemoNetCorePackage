using MediatR;

namespace DemoMediatR.RequestHandlers
{
    //public class SendRequestHandler : IRequestHandler<SendRequest, Unit>
    public class SendRequestHandler : IRequestHandler<SendRequest, SendResponse>
    {
        //public SendRequestHandler()
        //{
        //}
        //public async Task<Unit> Handle(SendRequest request, CancellationToken cancellationToken)
        public Task<SendResponse> Handle(SendRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"SendRequestHandler GetHashCode:{ GetHashCode() }");
            //if (GetHashCode() > 0)
            //{
            //    throw new Exception("SendRequestHandler.Exception");
            //}
            Console.WriteLine($"SendRequestHandler:{ request.Message }");
            //return Task.FromResult(Unit.Value);
            return Task.FromResult(new SendResponse() 
            { 
                Result = "Response",
            });
        }
    }
}
