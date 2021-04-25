using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectPrinting
{
    public class ObjectPrinter
    {
        public static PrintingConfig<T> For<T>()
        {
            return new PrintingConfig<T>();
        }

        public static string PrintToString<TOwner>(PrintingConfig<TOwner> printingConfig, TOwner obj)
        {
            return PrintToString(printingConfig, obj, 0);
        }

        private static string PrintToString<TOwner>(PrintingConfig<TOwner> printingConfig,
            object obj, int nestingLevel)
        {
            if (obj == null)
                return "null" + Environment.NewLine;
            if (printingConfig.ValueTypes.Contains(obj.GetType()))
                return obj + Environment.NewLine;
            var identation = new string('\t', nestingLevel + 1);
            var sb = new StringBuilder();
            var type = obj.GetType();
            sb.AppendLine(type.Name);
            foreach (var propertyInfo in GetSerializableProperties(type, printingConfig))
            {
                sb.Append(identation + propertyInfo.Name + " = ");
                if (printingConfig.SerializeProperty.ContainsKey(propertyInfo))
                {
                    sb.Append(printingConfig.SerializeProperty[propertyInfo](propertyInfo.GetValue(obj)));
                    sb.Append(Environment.NewLine);
                }
                else if (printingConfig.SerializeTypes.ContainsKey(propertyInfo.PropertyType))
                {
                    sb.Append(printingConfig
                        .SerializeTypes[propertyInfo.PropertyType](propertyInfo.GetValue(obj)));
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    sb.Append(PrintToString(printingConfig, propertyInfo.GetValue(obj), nestingLevel + 1));
                }
            }
            return sb.ToString();
        }

        private static IEnumerable<PropertyInfo> GetSerializableProperties<TOwner>(Type type,
            PrintingConfig<TOwner> printingConfig)
        {
            return type.GetProperties().Where(propertyInfo =>
                !(printingConfig.ExcludedTypes.Contains(propertyInfo.PropertyType) ||
                  printingConfig.ExcludedProperties.Contains(propertyInfo)));
        }
    }
}