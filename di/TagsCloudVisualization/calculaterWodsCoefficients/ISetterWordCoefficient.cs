using System.Collections.Generic;

namespace TagsCloudVisualization.calculaterWodsCoefficients
{
    public interface ISetterWordCoefficient
    {
        Dictionary<string, int> GetWordsCoefficient(
            IEnumerable<string> words, IEnumerable<string> excludedWords);
    }
}