using app.Operations.ProductOrders.Commands.EditOrderDetail;
using AutoMapper;
using domain.Entities;
using NUnitTestProject;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using MediatR;
using FluentValidation;
using System.Linq.Expressions;
using System.Collections.Specialized;

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
        public IDictionary<Expression<Func<T, object>>, ValidationErrorTypes> ExpectedValidationErrorTypes { get; }

        public GenericValidationTester(T command, IValidator<T> validator, IDictionary<Expression<Func<T, object>>, ValidationErrorTypes> expressions)
        {
            Command = command;
            Validator = validator;
            ExpectedValidationErrorTypes = expressions;
        }

        public ValidationResult ValidationResultOk()
        {
            var actual = Validator.Validate(Command).Errors
                .ToDictionary(x => x.PropertyName, x => (ValidationErrorTypes)Enum.Parse(typeof(ValidationErrorTypes), x.ErrorCode));

            foreach (var item in ExpectedValidationErrorTypes)
            {
                string memberName = string.Empty;
                if (item.Key.Body is MemberExpression)
                {
                    memberName = ((MemberExpression)item.Key.Body).Member.Name;
                }
            }

            Func<KeyValuePair<Expression<Func<T, object>>, ValidationErrorTypes>, string> GetMemberNameFromExpression = x =>
            {
                MemberExpression body = x.Key.Body as MemberExpression;
                if (body == null)
                {
                    UnaryExpression ubody = (UnaryExpression)x.Key.Body;
                    body = ubody.Operand as MemberExpression;
                }
                return body.Member.Name;
            };
            
            var expected = ExpectedValidationErrorTypes.ToDictionary(x => GetMemberNameFromExpression(x), x => x.Value);

            return new ValidationResult(expected, actual);
        }
    }

    public class ValidationResult
    {
        public ValidationResult(Dictionary<string, ValidationErrorTypes> expected, Dictionary<string, ValidationErrorTypes> actual)
        {
            Expected = expected;
            Actual = actual;
        }

        public Dictionary<string, ValidationErrorTypes> Actual { get; set; }
        public Dictionary<string, ValidationErrorTypes> Expected { get; set; }

    }

    public enum ValidationErrorTypes
    {
        AsyncPredicateValidator
        , LengthValidator
        , NotEmptyValidator
        , PredicateValidator
        , RegularExpressionValidator
    }
}
