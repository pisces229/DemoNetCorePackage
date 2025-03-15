using DemoFluentValidation.Extensions;
using DemoFluentValidation.Requests;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DemoFluentValidation.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        private readonly ILogger<CustomerValidator> _logger;
        private readonly DbContext _dbContext;
        public CustomerValidator(ILogger<CustomerValidator> logger,
            DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            RuleFor(req => req.Surname)
                .NotNull().WithName("Surname".ToUpper());
            RuleFor(req => req.Forename)
                //.Cascade(CascadeMode.Stop)
                //.NotNull().WithName("Forename".ToUpper())
                .NotEmpty().WithName("Forename".ToUpper())
                .NotEqual("").WithName("Forename".ToUpper());
            //RuleFor(req => req.Surname)
            //    .NotNull().WithMessage("{PropertyName} NotNull()");
            //RuleFor(req => req.Forename)
            //    .NotNull().WithMessage("{PropertyName} NotNull()")
            //    .NotEqual("foo").WithMessage("{PropertyName} NotEqual('foo')");

            // Must
            //RuleFor(req => req).Must(MustValidate).WithMessage("MustValidate Fail. Argument: {Item}.");
            //RuleFor(req => req.Surname).MustAsync(MustValidateAsync).WithMessage("MustValidateAsync Fail.");

            RuleFor(req => req).CustomerValide().WithName("Surname".ToUpper());
        }

        //private bool MustValidate(CustomerRequest typeObject, ValidationContext<CustomerRequest> context)
        //{
        //    Console.WriteLine("MustValidate...");
        //    _dbContext.Run();
        //    return false;
        //}

        private bool MustValidate(CustomerRequest typeObject, CustomerRequest typeProperty, ValidationContext<CustomerRequest> context)
        {
            _logger.LogInformation("MustValidate...");
            _dbContext.Run();
            _logger.LogInformation("typeObject.GetHashCode(): {0}", typeObject.GetHashCode());
            _logger.LogInformation("typeProperty.GetHashCode(): {0}", typeProperty.GetHashCode());
            context.MessageFormatter.AppendArgument("Item", "item...");
            return false;
        }

        private async Task<bool> MustValidateAsync(string value, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MustValidateAsync...");
            await _dbContext.RunAsync();
            return false;
        }

    }
}
