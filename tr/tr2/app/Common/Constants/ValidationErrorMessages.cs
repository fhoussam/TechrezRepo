namespace app.Common.Constants
{
    public class ValidationErrorMessages
    {
        public const string RequiredMessage = "{PropertyName} Is Mandatory";

        public const string WrongSize = "Wrong Size";
        public const string WrongFormat = "Wrong Format";
        public const string AlreadyExists = "Already Exists";
        public const string LesserThan = "{PropertyName} Must Be Less Than {ComparisonProperty}";
        public const string GreaterThan = "{PropertyName} Must Be Less Than {ComparisonProperty}";
        public const string LengthError = "{PropertyName} length should be between {MinLength} and {MaxLength}";

        public static readonly string QuantityNotSufficient = "{PropertyName} not sufficient";
        public static readonly string MustBeDevidableBy10AndGreaterThan0 = "Must be dividable by 10 and greater than 0";
        public static readonly string UnitsInStockShouldBePositiveAndGreaterOrEqualUnitsOnOrder = "Units in stock should be positive and greater (Or Equal) than units on order";
    }
}
