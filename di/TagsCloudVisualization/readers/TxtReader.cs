using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudVisualization.readers
{
    public class TxtReader : IFileReader
    {
        public string Path { get; }

        public TxtReader(string path)
        {
            Path = path;
        }

        public IEnumerable<string> ReadAll(char[] separators)
        {
            return File.ReadAllText(Path, Encoding.Default).Split(separators)
                .Where(word => !string.IsNullOrWhiteSpace(word));
        }
    }
}