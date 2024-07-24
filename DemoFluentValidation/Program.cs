using DemoFluentValidation;

{
    var instance = new Customer()
    { 
        Forename = "foo",
    };
    var validator = new CustomerValidator();
    var result = validator.Validate(instance);
    Console.WriteLine(result.IsValid);
    foreach (var failure in result.Errors)
    {
        Console.WriteLine("Property {0} failed validation. Error was: {1}",
            failure.PropertyName, failure.ErrorMessage);
    }
    Console.WriteLine(result.ToString("~"));
}


