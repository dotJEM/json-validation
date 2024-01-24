using DotJEM.Json.Validation.Rules;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results;

public class EmbededValidatorResult : Result
{
    public override bool IsValid => Result.IsValid;

    public Result Result { get; }
    public JToken Token { get; set; }
    public EmbededValidatorRule Rule { get; }

    public EmbededValidatorResult(EmbededValidatorRule rule, JToken token, Result result) 
    {
        this.Rule = rule;
        Token = token;
        this.Result = result;
    }

    public override Result Optimize()
    {
        return new EmbededValidatorResult(Rule, Token, Result.Optimize());
    }
}