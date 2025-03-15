using FluentValidation;
using FluentValidation.Validators;

namespace DemoFluentValidation.PropertyValidators
{
    public class CustomerPropertyValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "CustomerPropertyValidator";
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            return false;
        }
        protected override string GetDefaultMessageTemplate(string errorCode) => "CustomerPropertyValidator: '{PropertyName}'.";
    }
}
