using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class RectangleExtention
    {
        public static Point[] GetPoints(this Rectangle rectangle)
        {
            return new[]
            {
                new Point(rectangle.Left, rectangle.Top),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Right, rectangle.Bottom),
                new Point(rectangle.Left, rectangle.Bottom)
            };
        }

        public static double MinDistance(this Rectangle rectangle, Point point)
        {
            return rectangle.GetPoints().Select(rectanglePoint => rectanglePoint.GetDistance(point)).Min();
        }
    }
}