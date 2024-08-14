using FluentValidation;
using DemoFluentValidation.Validators;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

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
                Forename = "",
            };
            //var result = _validator.Validate(instance);
            logger.LogInformation("----------");
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
            logger.LogInformation("----------");
            var dictionary = result.ToDictionary();
            foreach (var key in dictionary.Keys)
            {
                var errorMessages = dictionary[key];
                logger.LogInformation("key: '{key}'.", key);
                logger.LogInformation("errorMessage: '{errorMessage}'.", string.Join("|", errorMessages));
            }
            logger.LogInformation("----------");
        }
    }
}
