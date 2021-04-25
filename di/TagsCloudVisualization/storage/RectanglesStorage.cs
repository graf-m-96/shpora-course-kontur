using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.auxiliary;

namespace TagsCloudVisualization.storage
{
    internal class RectanglesStorage
    {
        public Point CircleCenter { get; }
        public List<Rectangle> Rectangles { get; }
        public readonly HashSet<Point> Points;

        public RectanglesStorage(Point center)
        {
            CircleCenter = center;
            Rectangles = new List<Rectangle>();
            Points = new HashSet<Point>
            {
                center
            };
        }

        public void AddRectangle(Rectangle rectangle)
        {
            Rectangles.Add(rectangle);
            AddPointsOfRectangle(rectangle);
        }

        private void AddPointsOfRectangle(Rectangle rectangle)
        {
            foreach (var point in rectangle.GetPoints())
                Points.Add(point);
        }

        public bool RectanglesIntersect(Rectangle otherRectangle)
        {
            return Rectangles.Any(thisRectangle => thisRectangle.IntersectsWith(otherRectangle));
        }
    }
}