using System;

namespace ObjectPrinting
{
    internal static class ObjectPrinterExtension
    {
        public static string PrintToString<T>(this PrintingConfig<T> printingConfig, T obj)
        {
            return ObjectPrinter.PrintToString(printingConfig, obj);
        }

        public static string PrintToString<T>(this T obj)
        {
            return ObjectPrinter.PrintToString(ObjectPrinter.For<T>(), obj);
        }

        public static string PrintToString<T>(this T obj, Func<PrintingConfig<T>, PrintingConfig<T>> serializer)
        {
            return serializer(ObjectPrinter.For<T>()).PrintToString(obj);
        }

        public static string PrintToString<T>(this T obj, PrintingConfig<T> defaultSerializer)
        {
            return defaultSerializer.PrintToString(obj);
        }

        public static string PrintToString<T>(this T obj, Func<PrintingConfig<T>, PrintingConfig<T>> serializer,
            PrintingConfig<T> defaultSerializer)
        {
            return serializer(defaultSerializer).PrintToString(obj);
        }
    }
}