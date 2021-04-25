using System;
using System.Reflection;

namespace ObjectPrinting
{
    public class PropertyPrintingConfig<TOwner, TPropType> : IPropertyPrintingConfig<TOwner, TPropType>
    {
        private readonly PrintingConfig<TOwner> printingConfig;
        private readonly PropertyInfo propertyInfo;

        public PropertyPrintingConfig(PrintingConfig<TOwner> printingConfig)
        {
            this.printingConfig = printingConfig;
        }

        public PropertyPrintingConfig(PrintingConfig<TOwner> config, PropertyInfo propertyInfo) : this(config)
        {
            this.propertyInfo = propertyInfo;
        }

        PrintingConfig<TOwner> IPropertyPrintingConfig<TOwner, TPropType>.PrintingConfig => printingConfig;

        public PrintingConfig<TOwner> Using(Func<TPropType, string> alternativeSerializer)
        {
            if (propertyInfo != null)
                printingConfig.SerializeProperty[propertyInfo] = Caster.CastToObjectSerializer(alternativeSerializer);
            else
                printingConfig.SerializeTypes[typeof(TPropType)] = Caster.CastToObjectSerializer(alternativeSerializer);
            return printingConfig;
        }
    }
}