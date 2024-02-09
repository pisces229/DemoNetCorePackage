using DemoMediatR;
using DemoMediatR.NotificationHandlers;
using DemoMediatR.RequestHandlers;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var services = new ServiceCollection();

// MediatR services
Console.WriteLine(Assembly.GetExecutingAssembly().FullName);
//services.AddMediatR(Assembly.GetExecutingAssembly());
//services.AddMediatR(cfg =>
//{
//    //...
//}, Assembly.GetExecutingAssembly());
services.AddMediatR(typeof(IMediator));
//typeof(IMediator)
//typeof(INotificationHandler<>)
//typeof(IRequestPreProcessor<>)
//typeof(IRequestPostProcessor<,>)
//typeof(IRequestExceptionHandler<,,>)
//typeof(IRequestExceptionAction<,>)
//services.AddMediatR(cfg =>
//{
//    //...
//    cfg.RequestExceptionActionProcessorStrategy = RequestExceptionActionProcessorStrategy.ApplyForAllExceptions;
//}, typeof(IMediator));

// Priority...
services.AddTransient<INotificationHandler<NotificationRequest>, NotificationRequestHandlerFirst>();
services.AddTransient<INotificationHandler<NotificationRequest>, NotificationRequestHandlerSecond>();
//services.AddTransient<INotificationHandler<NotificationRequest>, NotificationRequestHandlerSecond>();
//services.AddTransient<INotificationHandler<NotificationRequest>, NotificationRequestHandlerFirst>();

// Handler
services.AddTransient<IRequestHandler<SendRequest, SendResponse>, SendRequestHandler>();
// ExceptionHandler, ExceptionAction
services.AddTransient<IRequestExceptionHandler<SendRequest, SendResponse, Exception>, SendExceptionHandler>();
services.AddTransient<IRequestExceptionAction<SendRequest, Exception>, SendExceptionAction>();
// IRequestExceptionHandler
//services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(LoggingExceptionHandler<,,>));
//services.AddTransient<IRequestExceptionHandler<SendRequest, SendResponse, Exception>, 
//    LoggingExceptionHandler<SendRequest, SendResponse, Exception>>();

services.AddScoped<Runner>();

using var serviceProvider = services.BuildServiceProvider();

await serviceProvider.GetRequiredService<Runner>().Run();