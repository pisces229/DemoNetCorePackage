using FluentValidation;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Validators;
using Microsoft.Extensions.Logging;

namespace DemoFluentValidation.Validators
{
    public class Customer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public decimal Discount { get; set; }
        public string Address { get; set; }
    }
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly ILogger<CustomerValidator> _logger;
        private readonly DbContext _dbContext;
        public CustomerValidator(ILogger<CustomerValidator> logger,
            DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            RuleFor(customer => customer.Surname)
                .NotNull().WithName("Surname".ToUpper());
            RuleFor(customer => customer.Forename)
                .NotNull().WithName("Surname".ToUpper())
                .NotEqual("foo").WithName("Surname".ToUpper());
            //RuleFor(customer => customer.Surname)
            //    .NotNull().WithMessage("{PropertyName} NotNull()");
            //RuleFor(customer => customer.Forename)
            //    .NotNull().WithMessage("{PropertyName} NotNull()")
            //    .NotEqual("foo").WithMessage("{PropertyName} NotEqual('foo')");

            // Must
            //RuleFor(customer => customer).Must(MustValidate).WithMessage("MustValidate Fail. Argument: {Item}.");
            //RuleFor(customer => customer.Surname).MustAsync(MustValidateAsync).WithMessage("MustValidateAsync Fail.");

            //RuleFor(customer => customer).CustomerValide().WithName("Surname".ToUpper());
        }

        //private bool MustValidate(Customer typeObject, ValidationContext<Customer> context)
        //{
        //    Console.WriteLine("MustValidate...");
        //    _dbContext.Run();
        //    return false;
        //}

        private bool MustValidate(Customer typeObject, Customer typeProperty, ValidationContext<Customer> context)
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

    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> CustomerValide<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomerPropertyValidator<T, TProperty>());
        }
    }

    public class CustomerPropertyValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "CustomerPropertyValidator";
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            Console.WriteLine(value);
            return false;
        }
        protected override string GetDefaultMessageTemplate(string errorCode) => "CustomerPropertyValidator: '{PropertyName}'.";
    }
}
