using DemoMediatR;
using DemoMediatR.NotificationHandlers;
using DemoMediatR.RequestHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var services = new ServiceCollection();

// MediatR services
services.AddMediatR(Assembly.GetExecutingAssembly());
//services.AddMediatR(typeof(AddPersonCommand).GetTypeInfo().Assembly);
//services.AddMediatR(cfg =>
//{
//    //cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
//    //cfg.AddBehavior<PingPongBehavior>();
//    //cfg.AddStreamBehavior<PingPongStreamBehavior>();
//    //cfg.AddRequestPreProcessor<PingPreProcessor>();
//    //cfg.AddRequestPostProcessor<PingPongPostProcessor>();
//    //cfg.AddOpenBehavior(typeof(GenericBehavior<,>));
//}, Assembly.GetExecutingAssembly());

// don't add any notification handler service
//services.AddTransient<INotificationHandler<NotificationRequest>, NotificationRequestHandlerFirst>();
//services.AddTransient<INotificationHandler<NotificationRequest>, NotificationRequestHandlerSecond>();

// lifetime
//services.AddTransient<IRequestHandler<SendRequest, SendResponse>, SendRequestHandler>();
//services.AddScoped<IRequestHandler<SendRequest, SendResponse>, SendRequestHandler>();

services.AddScoped<Runner>();

using var serviceProvider = services.BuildServiceProvider();

await serviceProvider.GetRequiredService<Runner>().Run();