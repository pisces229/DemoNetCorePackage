using DemoFluentValidation;
using DemoFluentValidation.Requests;
using DemoFluentValidation.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostbuilder = Host.CreateDefaultBuilder(args);

hostbuilder.ConfigureServices((hostContext, services) =>
{
    services.AddHostedService<HostedService>();

    //ValidatorOptions.Global.Severity = Severity.Info;
    // Dependency Injection
    services.AddScoped<IValidator<CustomerRequest>, CustomerValidator>();
    // Automatic registration
    // Load using a type reference rather than the generic.
    //services.AddValidatorsFromAssemblyContaining<CustomerValidator>(ServiceLifetime.Scoped);
    // Load an assembly reference rather than using a marker type.
    //services.AddValidatorsFromAssembly(Assembly.Load("AssemblyLoad"));
    // Filtering results
    //services.AddValidatorsFromAssemblyContaining<CustomerValidator>(ServiceLifetime.Scoped,
    //    filter => filter.ValidatorType != typeof(CustomerValidator));

    services.AddScoped<DbContext>();
    services.AddScoped<Runner>();
});

var host = hostbuilder.Build();

host.Run();
