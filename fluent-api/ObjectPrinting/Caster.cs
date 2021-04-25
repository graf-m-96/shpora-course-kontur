using System;

namespace ObjectPrinting
{
    public class Caster
    {
        public static Func<object, string> CastToObjectSerializer<TPropType>(
            Func<TPropType, string> alternativeSerializer)
        {
            return obj => alternativeSerializer((TPropType) obj);
        }
    }
}