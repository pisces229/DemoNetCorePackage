﻿using DemoMediatR.EventBus;
using DemoMediatR.NotificationHandlers;
using DemoMediatR.RequestHandlers;
using DemoMediatR.SimpleEvent;
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
            await RunSimpleEvent();
            //await RunEventBus();
            //await RunNotificationHandler();
            //await RunRequestHandler();
        }
        private Task RunSimpleEvent()
        {
            var customer = new Customer();
            var waiter = new Waiter();
            customer.OrderEvent += Waiter.Action;
            customer.Action();
            return Task.CompletedTask;
        }
        private Task RunEventBus()
        {
            var eventBus = new DemoEventBus();
            eventBus.Subscribe<DemoEvent1>(x => Console.WriteLine("Received Event1: " + x.Message));
            eventBus.Subscribe<DemoEvent1>(x => Console.WriteLine("Received Event1: " + x.Message));
            eventBus.Subscribe<DemoEvent2>(x => Console.WriteLine("Received Event2: " + x.Message));
            eventBus.Publish(new DemoEvent1
            {
                Message = "DemoEvent1 First"
            }); 
            eventBus.Publish(new DemoEvent2
            {
                Message = "DemoEvent2 First"
            });
            eventBus.Publish(new DemoEvent1
            {
                Message = "DemoEvent1 Second"
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
