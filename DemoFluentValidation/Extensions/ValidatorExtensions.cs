using DemoFluentValidation.PropertyValidators;
using FluentValidation;

namespace DemoFluentValidation.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> CustomerValide<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomerPropertyValidator<T, TProperty>());
        }
    }
}
