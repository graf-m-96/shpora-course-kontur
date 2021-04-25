using System;
using System.Globalization;

namespace ObjectPrinting
{
    internal static class PropertyPrintingConfigExtension
    {
        public static PrintingConfig<TOwner> SetCulture<TOwner>(
            this PropertyPrintingConfig<TOwner, int> config, CultureInfo info)
        {
            ((IPropertyPrintingConfig<TOwner, int>) config).PrintingConfig.SerializeTypes[typeof(int)] =
                number => ((int) number).ToString(info);
            return ((IPropertyPrintingConfig<TOwner, int>) config).PrintingConfig;
        }

        public static PrintingConfig<TOwner> SetCulture<TOwner>(
            this PropertyPrintingConfig<TOwner, double> config, CultureInfo info)
        {
            ((IPropertyPrintingConfig<TOwner, double>) config).PrintingConfig.SerializeTypes[typeof(double)] =
                number => ((double) number).ToString(info);
            return ((IPropertyPrintingConfig<TOwner, double>) config).PrintingConfig;
        }

        public static PrintingConfig<TOwner> SetCulture<TOwner>(
            this PropertyPrintingConfig<TOwner, long> config, CultureInfo info)
        {
            ((IPropertyPrintingConfig<TOwner, long>) config).PrintingConfig.SerializeTypes[typeof(long)] =
                number => ((long) number).ToString(info);
            return ((IPropertyPrintingConfig<TOwner, long>) config).PrintingConfig;
        }

        public static PrintingConfig<TOwner> Cut<TOwner>(
            this PropertyPrintingConfig<TOwner, string> config, Func<string, string> cutter)
        {
            ((IPropertyPrintingConfig<TOwner, string>) config).PrintingConfig.SerializeTypes[typeof(string)] =
                Caster.CastToObjectSerializer(cutter);
            return ((IPropertyPrintingConfig<TOwner, string>) config).PrintingConfig;
        }
    }
}