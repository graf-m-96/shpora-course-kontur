using System.Collections.Generic;

namespace TagsCloudVisualization.readers
{
    public interface IFileReader
    {
        IEnumerable<string> ReadAll(char[] separators);
    }
}