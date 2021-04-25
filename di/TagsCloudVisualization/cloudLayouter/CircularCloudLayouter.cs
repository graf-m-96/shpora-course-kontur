using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.auxiliary;
using TagsCloudVisualization.storage;

namespace TagsCloudVisualization.cloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly RectanglesStorage rectangleStorage;

        public CircularCloudLayouter(Point center)
        {
            rectangleStorage = new RectanglesStorage(center);
        }

        public Point GetCenter()
        {
            return rectangleStorage.CircleCenter;
        }

        public IEnumerable<Rectangle> GetAllRectangles()
        {
            return rectangleStorage.Rectangles;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            return FindNearestPlace(rectangleSize);
        }

        public Rectangle FindNearestPlace(Size rectangleSize)
        {
            var distanceOptimizedRectangle = Rectangle.Empty;
            var minDistance = double.PositiveInfinity;
            var pointExtractor = new PointExtractor(rectangleSize);
            foreach (var rectangle in rectangleStorage.Points.SelectMany(
                point => pointExtractor.CreateRectangles(point)))
            {
                if (rectangleStorage.RectanglesIntersect(rectangle) ||
                    rectangle.MinDistance(GetCenter()) >= minDistance)
                    continue;
                distanceOptimizedRectangle = rectangle;
                minDistance = rectangle.MinDistance(GetCenter());
            }
            rectangleStorage.AddRectangle(distanceOptimizedRectangle);
            return distanceOptimizedRectangle;
        }
    }
}