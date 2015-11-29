using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Rules.Results;

namespace DotJEM.Json.Validation.Descriptive
{
    public class JsonValidatorDescription : Description
    {
        private readonly JsonValidator validator;
        private readonly List<JsonFieldValidatorDescription> descriptions;

        public JsonValidatorDescription(JsonValidator validator, List<JsonFieldValidatorDescription> descriptions)
        {
            this.validator = validator;
            this.descriptions = descriptions;
        }

        public override string ToString()
        {
            StringWriter writer = new StringWriter();
            descriptions.ForEach(d => writer.WriteLine(d.ToString()));
            return writer.ToString();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            //TODO: (jmd 2015-10-26) Indentation? 
            descriptions.ForEach(d => d.WriteTo(writer));
            return writer;
        }
    }

    public class JsonFieldValidatorDescription : Description
    {
        private readonly IDescription rule;
        private readonly IDescription guard;

        public JsonFieldValidatorDescription(IDescription guard, IDescription rule)
        {
            this.rule = rule;
            this.guard = guard;
        }

        public override string ToString()
        {
            return $"When {guard} then {rule}";
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            writer.Write("When ");
            guard.WriteTo(writer);
            writer.Write(" then ");
            return rule.WriteTo(writer);
        }
    }

    public class AnyJsonRuleDescription : Description
    {
        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return writer;
        }
    }

    public class BasicJsonRuleDescription : Description
    {
        private readonly string alias;
        private readonly string selector;
        private readonly JsonConstraint constraint;

        public BasicJsonRuleDescription(string alias, string selector, JsonConstraint constraint)
        {
            this.alias = alias;
            this.selector = selector;
            this.constraint = constraint;
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return writer.WriteLine($"{alias} should {constraint.Describe()}");
        }
    }

    public class JsonNotRuleDescription : Description
    {
        private readonly Description inner;

        public JsonNotRuleDescription(Description inner)
        {
            this.inner = inner;
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return writer.WriteLine($"not {inner}");
        }
    }

    public class CompositeJsonRuleDescription : Description
    {
        private readonly IEnumerable<Description> list;
        private readonly string @join;

        public CompositeJsonRuleDescription(IEnumerable<Description> list, string join)
        {
            this.list = list;
            this.@join = @join;
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            using (writer.Indent())
            {
                list.Aggregate(false, (joining, description) =>
                {
                    if (joining)
                        writer.Write(join + " ");
                    description.WriteTo(writer);
                    return true;
                });
            }
            return writer;
        }
    }
}