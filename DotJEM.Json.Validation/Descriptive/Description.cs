namespace DotJEM.Json.Validation.Descriptive
{
    /// <summary>
    /// Represents something that can be described.
    /// </summary>
    public interface IDescribable
    {
        IDescription Describe();
    }

    /// <summary>
    /// Represents a description of a validator, result or constraint.
    /// </summary>
    public interface IDescription
    {
        IDescriptionWriter WriteTo(IDescriptionWriter writer);
    }

    public abstract class Description : IDescription
    {
        public override string ToString()
        {
            return WriteTo(new DescriptionWriter()).ToString();
        }

        public abstract IDescriptionWriter WriteTo(IDescriptionWriter writer);
    }
}