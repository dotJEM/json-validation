using System;
using System.Linq;
using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation
{
    public class CollectSingleSelectorVisitor : JsonRuleVisitor, IJsonRuleVisitor<CompositeJsonRule>, IJsonRuleVisitor<BasicJsonRule>, IJsonRuleVisitor<NotJsonRule>, IJsonRuleVisitor<FuncJsonRule>
    {
        private bool root = true;
        public string Selector { get; private set; }
        public string Alias { get; private set; }

        public IJsonRuleVisitor Visit(CompositeJsonRule rule)
        {
            root = false;
            return rule.Rules.Aggregate(this, (visitor, r) => r.Accept(visitor));
        }

        public IJsonRuleVisitor Visit(NotJsonRule rule)
        {
            return rule.Rule.Accept(this);
        }

        public IJsonRuleVisitor Visit(FuncJsonRule rule)
        {
            //TODO : Support for using lambdas to select values.
            //if(!root)
            throw new InvalidOperationException("Json Rule Tree had multiple different selectors.");
            //return this;
        }

        public IJsonRuleVisitor Visit(BasicJsonRule rule)
        {
            if (Selector == null)
            {
                Selector = rule.Selector;
                Alias = rule.Alias;
                return this;
            }

            if (Selector != rule.Selector)
                throw new InvalidOperationException("Json Rule Tree had multiple different selectors.");

            if (Alias == null)
            {
                //Note: If Alias is null, we allow the next rule to provide one.
                Alias = rule.Alias;
                return this;
            }

            if (Alias != rule.Alias)
            {
                //Note: If multiple aliases was found, we defer back to the selector.
                Alias = Selector;
            }

            return this;
        }
    }
}