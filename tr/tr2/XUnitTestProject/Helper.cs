using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Collections;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XUnitTestProject
{
    static class Helper
    {
        public static bool IsOrderedBy<T, TProperty>(this List<T> list, Expression<Func<T, TProperty>> propertyExpression) where TProperty : IComparable<TProperty>
        {
            var member = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)member.Member;
            IComparable<TProperty> previousValue = null;
            for (int i = 0; i < list.Count(); i++)
            {
                var currentValue = (TProperty)propertyInfo.GetValue(list[i], null);
                if (previousValue == null)
                {
                    previousValue = currentValue;
                    continue;
                }

                if (previousValue.CompareTo(currentValue) > 0) return false;
                previousValue = currentValue;

            }

            return true;
        }

        public static void DeepEquals(object expected, object actual)
        {
            var result = JToken.DeepEquals(JToken.FromObject(actual), JToken.FromObject(expected));
            Assert.True(result);
        }
    }
}
