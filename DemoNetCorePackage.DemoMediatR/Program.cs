using DemoNetCorePackage.DemoMediatR;
using DemoNetCorePackage.DemoMediatR.RequestHandlerService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var services = new ServiceCollection();
// MediatR services
services.AddMediatR(Assembly.GetExecutingAssembly());
// don't add any notification handler service
//services.AddTransient<INotificationHandler<PublishRequest>, PublishRequestHandlerFirst>();
//services.AddTransient<INotificationHandler<PublishRequest>, PublishRequestHandlerSecond>();
// set request handler lifetime
services.AddScoped<IRequestHandler<SendRequest, SendResponse>, SendRequestHandler>();
//services.AddTransient<IRequestHandler<SendRequest, SendResponse>, SendRequestHandler>();
services.AddScoped<Runner>();

var serviceProvider = services.BuildServiceProvider();
try
{
    serviceProvider.GetRequiredService<Runner>().Run().Wait();
}
finally
{
    serviceProvider.Dispose();
}