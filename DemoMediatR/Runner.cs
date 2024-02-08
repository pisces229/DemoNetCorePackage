using DemoMediatR.EventBus;
using DemoMediatR.NotificationHandlers;
using DemoMediatR.RequestHandlers;
using MediatR;

namespace DemoMediatR
{
    internal class Runner
    {
        private readonly IMediator _mediator;
        private readonly ISender _sender;
        private readonly IPublisher _publisher;
        public Runner(IMediator mediator, ISender sender, IPublisher publisher)
        {
            _mediator = mediator;
            _sender = sender;
            _publisher = publisher;
        }
        public async Task Run()
        {
            await RunEventBus();
            //await RunNotificationHandler();
            //await RunRequestHandler();
        }
        private Task RunEventBus()
        {
            var eventBus = new DemoEventBus();
            eventBus.Subscribe<Event1>(x => Console.WriteLine("Received Event1: " + x.Message));
            eventBus.Subscribe<Event1>(x => Console.WriteLine("Received Event1: " + x.Message));
            eventBus.Subscribe<Event2>(x => Console.WriteLine("Received Event2: " + x.Message));
            eventBus.Publish(new Event1
            {
                Message = "Hello, world!"
            });
            return Task.CompletedTask;
        }
        private async Task RunNotificationHandler()
        {
            {
                Console.WriteLine($"-----PublishRequest-----");

                await _mediator.Publish(new NotificationRequest { Message = "Run" });
                //await _publisher.Publish(new PublishRequest { Message = "Run" });

                Console.WriteLine($"-----PublishRequest-----");
            }
            {
                Console.WriteLine($"-----PublishRequest-----");

                await _mediator.Publish(new NotificationRequest { Message = "Run" });
                //await _publisher.Publish(new PublishRequest { Message = "Run" });

                Console.WriteLine($"-----PublishRequest-----");
            }
            {
                Console.WriteLine($"-----OtherRequest-----");

                await _mediator.Publish(new NoNotificationRequest { Message = "Run" });
                //await _publisher.Publish(new PublishRequest { Message = "Run" });

                Console.WriteLine($"-----OtherRequest-----");
            }
        }
        private async Task RunRequestHandler()
        {
            {
                Console.WriteLine($"-----SendRequest-----");

                var sendResponse = await _mediator.Send(new SendRequest { Message = "Run" });
                //var sendResponse = await _sender.Send(new SendRequest { Message = "Run" });

                Console.WriteLine($"SendResponse.Result:{sendResponse.Result}");
                Console.WriteLine($"-----SendRequest-----");
            }
            {
                Console.WriteLine($"-----SendRequest-----");

                var sendResponse = await _mediator.Send(new SendRequest { Message = "Run" });
                //var sendResponse = await _sender.Send(new SendRequest { Message = "Run" });

                Console.WriteLine($"SendResponse.Result:{sendResponse.Result}");
                Console.WriteLine($"-----SendRequest-----");
            }
        }
    }
}
