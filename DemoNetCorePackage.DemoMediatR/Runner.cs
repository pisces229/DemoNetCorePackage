using DemoNetCorePackage.DemoMediatR.NotificationHandlerService;
using DemoNetCorePackage.DemoMediatR.RequestHandlerService;
using MediatR;

namespace DemoNetCorePackage.DemoMediatR
{
    internal class Runner
    {
        private readonly IMediator _mediator;
        public Runner(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Run()
        {
            {
                Console.WriteLine($"-----PublishRequest-----");
                await _mediator.Publish(new PublishRequest { Message = "Run" });
                Console.WriteLine($"-----PublishRequest-----");
            }
            {
                Console.WriteLine($"-----PublishRequest-----");
                await _mediator.Publish(new PublishRequest { Message = "Run" });
                Console.WriteLine($"-----PublishRequest-----");
            }
            {
                Console.WriteLine($"-----OtherRequest-----");
                await _mediator.Publish(new OtherRequest { Message = "Run" });
                Console.WriteLine($"-----OtherRequest-----");
            }
            {
                Console.WriteLine($"-----SendRequest-----");
                var sendResponse = await _mediator.Send(new SendRequest { Message = "Run" });
                Console.WriteLine($"SendResponse.Result:{sendResponse.Result}");
                Console.WriteLine($"-----SendRequest-----");
            }
            {
                Console.WriteLine($"-----SendRequest-----");
                var sendResponse = await _mediator.Send(new SendRequest { Message = "Run" });
                Console.WriteLine($"SendResponse.Result:{sendResponse.Result}");
                Console.WriteLine($"-----SendRequest-----");
            }
        }
    }
}
