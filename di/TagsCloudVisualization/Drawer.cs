using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.auxiliary;

namespace TagsCloudVisualization
{
    public class Drawer
    {
        public Bitmap Bitmap { get; }

        public Drawer(IEnumerable<Tag> tags, Size imageSize, Brush fontColor,
            IDictionary<string, Font> wordToFont)
        {
            Bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            using (var graphics = Graphics.FromImage(Bitmap))
            {
                foreach (var tag in tags)
                    graphics.DrawString(tag.Word, wordToFont[tag.Word], fontColor, tag.Location);
            }
        }
    }
}