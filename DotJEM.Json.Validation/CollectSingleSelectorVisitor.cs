using System;
using System.Linq;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Visitors;

namespace DotJEM.Json.Validation
{
    public class CollectSingleSelectorVisitor : RuleVisitor
    {
        //private bool root = true;

        public string SelectorPath { get; private set; }
        public string Alias { get; private set; }

        public override void Visit(CompositeRule visitee)
        {
            //root = false;
            visitee.Rules.Aggregate(this, (visitor, r) => r.Accept(visitor));
        }

        public override void Visit(NotRule visitee)
        {
            visitee.Rule.Accept(this);
        }

        public override void Visit(FuncRule rule)
        {
            //TODO : Support for using lambdas to select values.
            //if(!root)
            throw new InvalidOperationException("Json Rule Tree had multiple different selectors.");
            //return this;
        }

        public override void Visit(BasicRule visitee)
        {
            if (SelectorPath == null)
            {
                SelectorPath = visitee.Selector.Path;
                Alias = visitee.Alias;
                return;
            }

            if (SelectorPath != visitee.Selector.Path)
                throw new InvalidOperationException("Json Rule Tree had multiple different selectors.");

            if (Alias == null)
            {
                //Note: If Alias is null, we allow the next rule to provide one.
                Alias = visitee.Alias;
                return;
            }

            if (Alias != visitee.Alias)
            {
                //Note: If multiple aliases was found, we defer back to the selector.
                Alias = SelectorPath;
            }
        }
    }
}