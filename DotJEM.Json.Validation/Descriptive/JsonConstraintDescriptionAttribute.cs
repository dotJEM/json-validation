using System;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Constraints.String.Length;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Descriptive
{
    /// <summary>
    /// <p>
    /// Description Attribute for Json Constraints.
    /// </p>
    /// <p>
    /// The descrition can point to Properties or fields stored or computed in the constraint which will be retreived by reflection.
    /// </p>
    /// <p>
    /// A Constraint description should say exactly what the constraint does and nothing more, this description should fit into e.g. "must have {description}", "should be {description}" etc.
    /// Based on the factory interfaces choosen.
    /// 
    /// Some suitable examples:
    /// <list type="table">
    ///     <listheader>  
    ///         <term>Constraint</term>  
    ///         <description>Description</description>  
    ///         <description>Example Result</description>  
    ///     </listheader>  
    ///     <item>  
    ///         <term><see cref="StringLengthConstraint"/> on <see cref="IHaveConstraintFactory"/></term>  
    ///         <description>length from '{minLength}' to '{maxLength}'.</description>  
    ///         <description>must have length from '5' to '10'.</description>  
    ///     </item>   
    ///     <item>  
    ///         <term><see cref="MatchStringConstraint"/>on <see cref="IValidatorConstraintFactory"/></term>  
    ///         <description>match the expression: '{expression}'.</description>  
    ///         <description>must match the expression: '\d{4}\-\w+'.</description>  
    ///     </item>   
    ///     <item>  
    ///         <term><see cref="StringEqualsConstraint"/>on <see cref="IBeConstraintFactory"/></term>  
    ///         <description>equal to '{value}' ({comparison}).</description>  
    ///         <description>must be equal to '{value}' ({comparison}).</description>  
    ///     </item>  
    /// </list>
    /// </p>
    /// </summary>
    public class JsonConstraintDescriptionAttribute : Attribute
    {
        public string Format { get; private set; }

        public JsonConstraintDescriptionAttribute(string format)
        {
            Format = format;
        }
    }
}