﻿using System;
using System.Reflection.Emit;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Selectors;
using DotJEM.Json.Validation.Visitors;

namespace DotJEM.Json.Validation;

public interface IJsonValidatorRuleFactory
{
    IFieldValidatorConfig Then(Rule validator);
    IFieldValidatorConfig Then(FieldSelector selector, CapturedConstraint validator);
    IFieldValidatorConfig Then(FieldSelector selector, string alias, CapturedConstraint validator);
    IFieldValidatorConfig Then(ISelfReferencingRule @ref, CapturedConstraint constraint);

    IForFieldSelector Use<TValidator>() where TValidator : JsonValidator, new();
    IForFieldSelector Use<TValidator>(TValidator instance) where TValidator : JsonValidator;
    IForFieldSelector Use(Type validatorType);
}

public interface IFieldValidatorConfig
{
        
}

public class FieldValidatorConfig : IFieldValidatorConfig
{
    public FieldValidatorConfig(JsonFieldValidator validator)
    {
            
    }
}

public class JsonValidatorRuleFactory : IJsonValidatorRuleFactory
{
    private readonly Rule rule;
    private readonly JsonValidator validator;

    public JsonValidatorRuleFactory(JsonValidator validator, Rule rule)
    {
        this.validator = validator;
        this.rule = rule;
    }

    public IFieldValidatorConfig Then(FieldSelector selector, CapturedConstraint constraint) => Then(new BasicRule(selector, constraint));
    public IFieldValidatorConfig Then(FieldSelector selector, string alias, CapturedConstraint constraint) => Then(new AliasedFieldSelector(alias, selector), constraint);

    public IFieldValidatorConfig Then(ISelfReferencingRule @ref, CapturedConstraint constraint)
    {
        try
        {
            CollectSingleSelectorVisitor visitor = rule.Accept(new CollectSingleSelectorVisitor());
            return Then(visitor.SelectorPath, visitor.Alias, constraint);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("A self referencing rule (It) can only be used when a single field is used in the When clause.", ex);
        }
    }

    public IFieldValidatorConfig Then(Rule rule)
    {
        return validator.AddValidator(new JsonFieldValidator(this.rule, rule));
    }

    public IForFieldSelector Use<TValidator>() where TValidator : JsonValidator, new() => new ForFieldSelector(this, rule, new TValidator());
    public IForFieldSelector Use<TValidator>(TValidator instance) where TValidator : JsonValidator => new ForFieldSelector(this, rule, instance);

    public IForFieldSelector Use(Type validatorType)
    {
        if(!validatorType.IsSubclassOf(typeof(JsonValidator)))
            throw new ArgumentException("The given type must inherit from JsonValidator.");

        return Use((JsonValidator)Activator.CreateInstance(validatorType));
    }
}

public interface IForFieldSelector
{
    void For(FieldSelector selector, string alias = null);
    void For(ISelfReferencingRule selector, string alias = null);

    void ForEachIn(FieldSelector selector, string alias = null);
    void ForEachIn(ISelfReferencingRule selector, string alias = null);
}

public class ForFieldSelector : IForFieldSelector
{
    private readonly Rule rule;
    private readonly JsonValidator validator;
    private readonly JsonValidatorRuleFactory factory;

    public ForFieldSelector(JsonValidatorRuleFactory factory, Rule rule, JsonValidator validator)
    {
        this.factory = factory;
        this.rule = rule;
        this.validator = validator;
    }

    public void For(FieldSelector selector, string alias = null)
    {
        selector = alias != null ? new AliasedFieldSelector(alias, selector) : selector; 
        factory.Then(new EmbededValidatorRule(selector, validator));
    }

    public void For(ISelfReferencingRule selector, string alias = null)
    {
        try
        {
            CollectSingleSelectorVisitor visitor = rule.Accept(new CollectSingleSelectorVisitor());
            For(visitor.SelectorPath, alias);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("A self referencing rule (It) can only be used when a single field is used in the When clause.", ex);
        }
    }

    public void ForEachIn(FieldSelector selector, string alias = null)
    {
        selector = new ForEachInFieldSelector(selector);
        selector = alias != null ? new AliasedFieldSelector(alias, selector) : selector;
        factory.Then(new EmbededValidatorRule(selector, validator));
    }

    public void ForEachIn(ISelfReferencingRule selector, string alias = null)
    {
        try
        {
            //TODO: A hack for now in order to force it as a MultiSelector.
            CollectSingleSelectorVisitor visitor = rule.Accept(new CollectSingleSelectorVisitor());
            ForEachIn(visitor.SelectorPath, alias);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("A self referencing rule (It) can only be used when a single field is used in the When clause.", ex);
        }
    }
}

public interface ISelfReferencingRule
{
}

public class SelfReferencingRule : ISelfReferencingRule
{
}