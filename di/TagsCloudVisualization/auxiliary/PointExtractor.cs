using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.auxiliary
{
    public class PointExtractor
    {
        private Size size;

        public PointExtractor(Size size)
        {
            this.size = size;
        }

        public IEnumerable<Rectangle> CreateRectangles(Point point)
        {
            yield return new Rectangle(point, size);
            yield return new Rectangle(new Point(point.X - size.Width, point.Y), size);
            yield return new Rectangle(new Point(point.X, point.Y - size.Height), size);
            yield return new Rectangle(new Point(point.X - size.Width, point.Y - size.Height), size);
        }
    }
}