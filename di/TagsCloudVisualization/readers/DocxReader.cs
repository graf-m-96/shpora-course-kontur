using System.Collections.Generic;
using System.Linq;
using Code7248.word_reader;

namespace TagsCloudVisualization.readers
{
    public class DocxReader : IFileReader
    {
        public string Path { get; }

        public DocxReader(string path)
        {
            Path = path;
        }

        public IEnumerable<string> ReadAll(char[] separators)
        {
            var extractor = new TextExtractor(Path);
            return extractor.ExtractText().Split(separators)
                .Where(word => !string.IsNullOrWhiteSpace(word));
        }
    }
}