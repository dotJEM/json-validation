[![Build status](https://ci.appveyor.com/api/projects/status/at67620962onli32/branch/master?svg=true)](https://ci.appveyor.com/project/jeme/json-validation/branch/master)

# json-validation

While JsonSchema largely allows us to validate Json objects, we had trouble finding heads and tails in how cross validation and conditional validation would work. For the examples we have seen it also seemed dificult to read and write all these rules.

Because of this I desided to write a small framwork for building validators in a fluent way.

The goal would be to:
 - Provide a more straigt forward fluent syntax for writing validation rules.
 - Provide an API that would allow us to describe the validation rules. Output which could be in the formas of:
   - A JsonSchema where possible (Some constraints might not be able to be converted into a meaningfull JsonScheme validation)
   - Simple text
 - Framework should be extensible so custom validations would be easy to write.
 - ...
 
# Example Syntax would be:

```
    public class TestValidator : JsonValidator
    {
        public TestValidator()
        {
            When("test", Has.MinLength(5)).Then("test", Must.Have.MaxLength(200));
            When("other", Has.MinLength(0)).Then("test", Must.Have.MaxLength(25));

            When(Field("test", Has.MinLength(5))).Then(Field("other", Should.Be.Equal("0")));

            When(Field("A", Is.Defined()) | Field("B", Is.Defined()))
                .Then(
                      Field("A", Must.Be.Equal("") | Must.Be.Equal(""))
                    & Field("B", Must.Be.Equal("")));
                    
            //Using other/partial validators for e.g. each items in arrays etc:
            //NOTE: Syntax proposal
            //Use<TestValidator>().For(Field("x"));
            //Use<TestValidator>().For(All("items"));
            
            //NOTE: Syntax proposal
            //When("x", Is.Defined()).Use<TestValidator>().For(It);
            //When("x", Is.Defined()).Use<TestValidator>().For(Field("x"));
            //When("x", Is.Defined()).Use<TestValidator>().For(All("items")); / Each
            
            //NOTE: Syntax proposal
            //When(Any).Then(All("", Should.Have.Length(42)));
            //When(Any).Then(Some("", Should.Have.Length(42)));
            //When(Any).Then(None("", Should.Have.Length(42)));
        }
    }
```
