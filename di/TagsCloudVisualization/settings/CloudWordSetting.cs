using System.Collections.Generic;
using TagsCloudVisualization.calculaterWodsCoefficients;
using TagsCloudVisualization.readers;

namespace TagsCloudVisualization.settings
{
    public class CloudWordSetting
    {
        public readonly IFileReader Reader;
        public readonly ISetterWordCoefficient SetterWordCoefficient;
        public readonly IEnumerable<string> ExcludedWords;
        public readonly char[] Separators;

        public CloudWordSetting(IFileReader reader, ISetterWordCoefficient setterWordCoefficient,
            IEnumerable<string> excludedWords, char[] separators)
        {
            Reader = reader;
            SetterWordCoefficient = setterWordCoefficient;
            ExcludedWords = excludedWords;
            Separators = separators;
        }
    }
}