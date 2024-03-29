﻿using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using FluentValidation;
using System.Linq.Expressions;

namespace XUnitTestProject
{
    //to enable polymorphisme at TheoryData level, 'out' must be used here
    public interface IValidationTester<out T> where T : IBaseRequest
    {
        ValidationResult ValidationResultOk();
    }

    public class GenericValidationTester<T> : IValidationTester<T> where T : IBaseRequest
    {
        public T Command { get; }
        public IValidator<T> Validator { get; }
        public IDictionary<Expression<Func<T, object>>, ValidationErrorMessage> ExpectedValidationErrorTypes { get; }

        public GenericValidationTester(T command, IValidator<T> validator, IDictionary<Expression<Func<T, object>>, ValidationErrorMessage> expressions = null)
        {
            Command = command;
            Validator = validator;
            ExpectedValidationErrorTypes = expressions;
        }

        private string GetUntransformedErrorMessage(string errorMessage, Dictionary<string, object> placeholders) 
        {
            placeholders.ToList().ForEach(x => {
                if(x.Value is string && x.Value != null && !string.IsNullOrEmpty(x.Value.ToString()) && x.Key != null)
                    errorMessage = errorMessage.Replace(x.Value.ToString(), $"{{{x.Key}}}");
            });

            return errorMessage;
        }

        public ValidationResult ValidationResultOk()
        {
            Dictionary<string, ValidationErrorMessage> expected = null;
            Dictionary<string, ValidationErrorMessage> actual = null;

            var validationResult = Validator.Validate(Command);

            if (validationResult.Errors.Count > 0)
                actual = validationResult.Errors
                    .ToDictionary(x => x.PropertyName, x => new ValidationErrorMessage(
                        GetUntransformedErrorMessage(x.ErrorMessage, x.FormattedMessagePlaceholderValues),
                        (ValidationErrorTypes)Enum.Parse(typeof(ValidationErrorTypes), x.ErrorCode))
                    );

            if (ExpectedValidationErrorTypes != null) 
            {
                foreach (var item in ExpectedValidationErrorTypes)
                {
                    string memberName = string.Empty;
                    if (item.Key.Body is MemberExpression)
                    {
                        memberName = ((MemberExpression)item.Key.Body).Member.Name;
                    }
                }

                Func<KeyValuePair<Expression<Func<T, object>>, ValidationErrorMessage>, string> GetMemberNameFromExpression = x =>
                {
                    MemberExpression body = x.Key.Body as MemberExpression;
                    if (body == null)
                    {
                        UnaryExpression ubody = (UnaryExpression)x.Key.Body;
                        body = ubody.Operand as MemberExpression;
                    }
                    return body.Member.Name;
                };

                expected = ExpectedValidationErrorTypes.ToDictionary(x => GetMemberNameFromExpression(x), x => x.Value);
            }

            return new ValidationResult(expected, actual);
        }
    }

    public class ValidationResult
    {
        public ValidationResult(Dictionary<string, ValidationErrorMessage> expected, Dictionary<string, ValidationErrorMessage> actual)
        {
            Expected = expected;
            Actual = actual;
        }

        public Dictionary<string, ValidationErrorMessage> Actual { get; set; }
        public Dictionary<string, ValidationErrorMessage> Expected { get; set; }

    }

    public enum ValidationErrorTypes
    {
        AsyncPredicateValidator
        , LengthValidator
        , NotEmptyValidator
        , PredicateValidator
        , RegularExpressionValidator
        , GreaterThanValidator
    }

    public class ValidationErrorMessage 
    {
        public ValidationErrorMessage(string errorMessage, ValidationErrorTypes errorType)
        {
            ErrorMessage = errorMessage;
            ErrorType = errorType;
        }

        public readonly string ErrorMessage;
        public readonly ValidationErrorTypes ErrorType;
    }
}
