using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Validation
{
    public class ValidationSummary
    {
        /// <summary>
        /// Validation errors
        /// </summary>
        public IList<ValidationError> Errors { get; private set; }

        public ValidationSummary()
        {
            Errors = new List<ValidationError>();
        }

        /// <summary>
        /// Indicates whether the model was valid or not
        /// </summary>
        public bool IsValid { get { return !Errors.Any(); } }

        public class ValidationError
        {
            /// <summary>
            /// The name of the property on the model with an error.  Nested properties will be separated by periods.
            /// A null value implies an error tied to no particular model property.
            /// </summary>
            public string PropertyName { get; set; }

            /// <summary>
            /// Error message
            /// </summary>
            public string Message { get; set; }
        }
    }
}