namespace DotJEM.Json.Validation.Factories
{
    public interface IConstraintFactory
    {
    }
    public interface IHaveConstraintFactory : IConstraintFactory
    {
    }

    public interface IBeConstraintFactory : IConstraintFactory
    {
    }

    public interface IGuardConstraintFactory : IBeConstraintFactory, IHaveConstraintFactory
    {
    }

    public class ConstraintFactory : IGuardConstraintFactory
    {
        
    }

    public interface IValidatorConstraintFactory
    {
        IBeConstraintFactory Be { get; }
        IHaveConstraintFactory Have { get; }
    }
    

    public class ValidatorConstraintFactory : IValidatorConstraintFactory
    {
        public IBeConstraintFactory Be { get; } = new ConstraintFactory();
        public IHaveConstraintFactory Have { get; } = new ConstraintFactory();
    }

    //public static class GuardConstraintFactoryCommonExtensions
    //{
    //    public static JsonConstraint Defined(this IGuardConstraintFactory self)
    //    {
    //        return new IsDefinedJsonConstraint();
    //    }
    //}

}