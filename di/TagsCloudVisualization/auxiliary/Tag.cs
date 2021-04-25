using System.Drawing;

namespace TagsCloudVisualization.auxiliary
{
    public class Tag
    {
        public string Word { get; }
        public Point Location { get; }

        public Tag(string word, Point location)
        {
            Word = word;
            Location = location;
        }
    }
}