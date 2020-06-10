[![Build status](https://ci.appveyor.com/api/projects/status/at67620962onli32/branch/master?svg=true)](https://ci.appveyor.com/project/jeme/json-validation/branch/master)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FdotJEM%2Fjson-validation.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FdotJEM%2Fjson-validation?ref=badge_shield)

# json-validation

While JsonSchema largely allows to validate Json objects, the seemed to be dificult to figure out especially when it comes to cross validation and conditional validation.
For obvious reasons validations in a schema is also more static in nature which could mean that these validation schemas would become obsolete faster than they where generated.

This framwork tries to make it easy to write validators in C# that in a fluent syntax which is easy to understand.

**Highlights:**
 - Provides a straigt forward fluent syntax for writing validation rules.
 - Provides an extensible API which allows developers to add aditional features to the framework fast and easy, such constraints could be very domain specific and even interact with configurable sources.
 - Provides a way to generate meaningfull descriptions of the validations both for human and machine. Hereunder allow for decorating JsonSchemas with the constraints that can be expressed.
 - Provides a way to generate meaningfull descriptions of validation errors for both human and machine.

 
# Example:

Given the user validator below.

```csharp
    public class UserValidator : JsonValidator
    {
        public UserValidator()
        {
            When(Any)
                .Then(Field("id", Is.Required() & Must.Be.Number() & Must.Be.GreaterThan(0))
                    & Field("username", Is.Required() & Must.Be.String() & Must.Have.MinLength(2))
                    & Field("email", Is.Required() & Must.Match(@"^[^@]+@[^@]+\.[^@]+$")));

            When("name", Is.Defined())
                .Then(It, Must.Be.String() & Have.MaxLength(256));

            When(Field("company", Is.Defined()) | Field("address", Is.Defined()))
                .Then("address", Is.Required());

            When("address", Is.Defined() & Is.Object())
                .Use<AddressValidator>()
                .For(It);
            
            When("company", Is.Defined())
                .Then("company.name", Is.Required() & Must.Be.String() & Have.LengthBetween(3, 256));
        }
    }
```

The fluent syntax stays very close to how one would express the rules in natural english which makes the rules easy to read and understand.

If the following JSON is passed though the validator:

```Json
{
  "name": null, "company": { }
}
```

And then later described with an example implementation of a descriptor, the output looks like this:

```
When
    ANY
Then
    (
        id
        (
            is required - actual value was: NULL
            AND
            must be a number (strict: True) - actual value was: NULL
            AND
            must be greather than 0 - actual value was: NULL
        )
        AND
        username
        (
            is required - actual value was: NULL
            AND
            must be a string - actual value was: NULL
            AND
            must have length more than or equal to '2' - actual value was: NULL
        )
        AND
        email
        is required - actual value was: NULL
    )

When
    name
    is defined
Then
    name
    must be a string - actual value was: 

When
    (
        company
        is defined
        OR
        address
        is defined
    )
Then
    address
    is required - actual value was: NULL

When
    company
    is defined
Then
    company.name
    (
        is required - actual value was: NULL
        AND
        must be a string - actual value was: NULL
        AND
        have length from '3' to '256' - actual value was: NULL
    )
```

This is a bit verbose, but shows that the result of the validation can be converted to something that is fairly readable by an average user. 
By using the fluent syntax, much of the information we put into the validator as pure code is preserved and can be used to generate an output.

By implementing custom descriptors, developers can build their own output.


# Extending with new constraints

It is easy to extend the framework with new constrains, an example of this could be an EmailConstraint which validates a string as an email

To do this, first implement the actual constraint:
```
[JsonConstraintDescription("valid email")]
public class ValidEmailConstraint : JsonConstraint
{
    private static readonly Regex pattern = new Regex("emailpattern", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    public override bool Matches(JToken token, IJsonValidationContext context)
        => token?.Type == JTokenType.String && pattern.IsMatch((string)token);
}
```

Then add an extension method that targets the appropirate interface, in this case "must be valid email" sounds right so we targets the "IBeConstraintFactory":
```
public static CapturedConstraint ValidEmail(this IBeConstraintFactory self)
    => self.Capture(new ValidEmailConstraint());
```

It is now possible to use the new constraint as:

```csharp
    public class UserValidator : JsonValidator
    {
        public UserValidator()
        {
            When("email", Is.Defined())
                .Then(It, Must.Be.ValidEmail());
        }
    }
```


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FdotJEM%2Fjson-validation.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FdotJEM%2Fjson-validation?ref=badge_large)