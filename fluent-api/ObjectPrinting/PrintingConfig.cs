using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions.Common;

namespace ObjectPrinting
{
    public class PrintingConfig<TOwner>
    {
        public readonly HashSet<PropertyInfo> ExcludedProperties = new HashSet<PropertyInfo>();
        public readonly HashSet<Type> ExcludedTypes = new HashSet<Type>();

        public readonly Dictionary<PropertyInfo, Func<object, string>> SerializeProperty =
            new Dictionary<PropertyInfo, Func<object, string>>();

        public readonly Dictionary<Type, Func<object, string>> SerializeTypes =
            new Dictionary<Type, Func<object, string>>();

        public readonly Type[] ValueTypes =
        {
            typeof(int), typeof(double), typeof(float), typeof(string),
            typeof(DateTime), typeof(TimeSpan)
        };

        public PrintingConfig<TOwner> Excluding<TPropType>()
        {
            ExcludedTypes.Add(typeof(TPropType));
            return this;
        }

        public PrintingConfig<TOwner> Excluding<TPropType>(Expression<Func<TOwner, TPropType>> memberSelector)
        {
            ExcludedProperties.Add(memberSelector.GetPropertyInfo());
            return this;
        }

        public PropertyPrintingConfig<TOwner, TPropType> Printing<TPropType>()
        {
            return new PropertyPrintingConfig<TOwner, TPropType>(this);
        }

        public PropertyPrintingConfig<TOwner, TPropType> Printing<TPropType>(
            Expression<Func<TOwner, TPropType>> propertySelector)
        {
            var propertyInfo = ((MemberExpression) propertySelector.Body).Member as PropertyInfo;
            return new PropertyPrintingConfig<TOwner, TPropType>(this, propertyInfo);
        }
    }
}