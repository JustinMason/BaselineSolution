namespace Infrastructure.Validation
{
    public class ValidatedResult<TResult>
    {
        public bool IsValid { get { return ValidationSummary.IsValid; } }
        public ValidationSummary ValidationSummary { get; set; } 
        public TResult Result { get; set; }
    }
}