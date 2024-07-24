using FluentValidation;

namespace DemoFluentValidation
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
        public CustomerValidator()
        {
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
        }
    }
}
