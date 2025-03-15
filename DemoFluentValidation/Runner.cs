using DemoFluentValidation.Requests;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DemoFluentValidation
{
    public class Runner(ILogger<Runner> _logger,
        IValidator<CustomerRequest> _validator)
    {
        public async Task Run()
        {
            var instance = new CustomerRequest()
            {
                //Surname = "Surname",
                Forename = "",
            };
            //var result = _validator.Validate(instance);
            _logger.LogInformation("----------");
            var result = await _validator.ValidateAsync(instance);
            _logger.LogInformation(result.IsValid.ToString());
            foreach (var failure in result.Errors)
            {
                _logger.LogInformation("PropertyName: '{PropertyName}'.", failure.PropertyName);
                _logger.LogInformation("ErrorMessage: '{ErrorMessage}'.", failure.ErrorMessage);
                _logger.LogInformation("ErrorCode: '{ErrorCode}'.", failure.ErrorCode);
                _logger.LogInformation("Severity: '{Severity}'.", failure.Severity);
            }
            _logger.LogInformation(result.ToString("~"));
            _logger.LogInformation("----------");
            var dictionary = result.ToDictionary();
            foreach (var key in dictionary.Keys)
            {
                var errorMessages = dictionary[key];
                _logger.LogInformation("key: '{key}'.", key);
                _logger.LogInformation("errorMessage: '{errorMessage}'.", string.Join("|", errorMessages));
            }
            _logger.LogInformation("----------");
        }
    }
}
