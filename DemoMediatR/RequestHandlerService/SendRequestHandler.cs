using MediatR;

namespace DemoMediatR.RequestHandlerService
{
    public class SendRequestHandler : IRequestHandler<SendRequest, SendResponse>
    {
        public SendRequestHandler()
        {
        }

        public async Task<SendResponse> Handle(SendRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"SendRequestHandler GetHashCode:{ GetHashCode() }");
            Console.WriteLine($"SendRequestHandler:{ request.Message }");
            return await Task.FromResult(new SendResponse
            {
                Result = "Finish"
            });
        }
    }
}
