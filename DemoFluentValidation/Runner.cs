using FluentValidation;
using DemoFluentValidation.Validators;
using Microsoft.Extensions.Logging;

namespace DemoFluentValidation
{
    public class Runner(ILogger<Runner> logger,
        IValidator<Customer> validator)
    {
        public async Task Run()
        {
            var instance = new Customer()
            {
                //Surname = "Surname",
                Forename = "foo",
            };
            //var result = _validator.Validate(instance);
            var result = await validator.ValidateAsync(instance);
            logger.LogInformation(result.IsValid.ToString());
            foreach (var failure in result.Errors)
            {
                logger.LogInformation("Property {0} failed validation. Error was: {1}",
                    failure.PropertyName, failure.ErrorMessage);
            }
            logger.LogInformation(result.ToString("~"));
        }
    }
}
