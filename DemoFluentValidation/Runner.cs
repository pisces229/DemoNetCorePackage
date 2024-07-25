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
                logger.LogInformation("PropertyName: '{PropertyName}'.", failure.PropertyName);
                logger.LogInformation("ErrorMessage: '{ErrorMessage}'.", failure.ErrorMessage);
                logger.LogInformation("ErrorCode: '{ErrorCode}'.", failure.ErrorCode);
                logger.LogInformation("Severity: '{Severity}'.", failure.Severity);
            }
            logger.LogInformation(result.ToString("~"));
        }
    }
}
