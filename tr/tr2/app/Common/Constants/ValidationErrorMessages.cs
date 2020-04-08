using System;
using System.Collections.Generic;
using System.Text;

namespace app.Common.Constants
{
    public class ValidationErrorMessages
    {
        public static readonly string RequiredMessage = "{PropertyName} Is Mandatory";

        public static readonly string WrongSize = "Wrong Size";
        public static readonly string WrongFormat = "Wrong Format";
        public static readonly string AlreadyExists = "Already Exists";
        public static readonly string LesserThan = "{PropertyName} Must Be Less Than {ComparisonProperty}";
        public static readonly string GreaterThan = "{PropertyName} Must Be Less Than {ComparisonProperty}";

        public static readonly string DateShouldLessThanAWeek = "{PropertyName} should not be older than a week";
        public static readonly string DateBeNewerThanCurrent = "{PropertyName} should not be older than a week";

        public static readonly string MustBeDevidableBy10AndGreaterThan0 = "Must be dividable by 10 and greater than 0";
        public static readonly string UnitsInStockShouldBePositiveAndGreaterOrEqualUnitsOnOrder = "Units in stock should be positive and greater (Or Equal) than units on order";
    }
}
