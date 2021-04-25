using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    public class RectanglesStorage_Should
    {
        private RectanglesStorage storage;
        private Point[] points;
        private Size size;

        [SetUp]
        public void SetUp()
        {
            storage = new RectanglesStorage(new Point(0, 0));
            points = new[]
            {
                new Point(0, 0), new Point(5, 0), new Point(5, 5), new Point(10, 5),
                new Point(-5, 0), new Point(-5, 5), new Point(-10, 5)
            };
            size = new Size(5, 5);
        }

        [Test]
        public void HaveOnePoint_BeforeAdding()
        {
            storage.Points.Count.Should().Be(1);
        }


        [Test]
        public void HaveOneRectangle_AfterAddingOneRectangle()
        {
            storage.AddRectangle(new Rectangle(points[0], size));
            storage.Rectangles.ShouldAllBeEquivalentTo(new List<Rectangle>
            {
                new Rectangle(points[0], size)
            });
        }

        [Test]
        public void HaveFourPoints_AfterAddingOneRectangle()
        {
            storage.AddRectangle(new Rectangle(points[0], size));
            storage.Points.ShouldAllBeEquivalentTo(new List<Point>
            {
                new Point(0, 0),
                new Point(5, 0),
                new Point(5, 5),
                new Point(0, 5)
            });
        }

        [Test]
        public void HaveFiveRectangles_AfterAddingFiveRectangles()
        {
            for (var i = 0; i < 5; i++)
                storage.AddRectangle(new Rectangle(points[i], size));
            storage.Rectangles.ShouldAllBeEquivalentTo(new List<Rectangle>
            {
                new Rectangle(points[0], size),
                new Rectangle(points[1], size),
                new Rectangle(points[2], size),
                new Rectangle(points[3], size),
                new Rectangle(points[4], size)
            });
        }

        [Test]
        public void RectanglesIntersections_AfterAddingNearRectangle()
        {
            storage.AddRectangle(new Rectangle(points[0], size));
            storage.Rectangles.ShouldAllBeEquivalentTo(new List<Rectangle>
            {
                new Rectangle(points[0], size)
            });
            storage.RectanglesIntersect(new Rectangle(new Point(3, 3), size));
        }

        [Test]
        public void RectanglesNotIntersections_AfterAddingFarRectangle()
        {
            storage.AddRectangle(new Rectangle(points[0], size));
            storage.Rectangles.ShouldAllBeEquivalentTo(new List<Rectangle>
            {
                new Rectangle(points[0], size)
            });
            storage.RectanglesIntersect(new Rectangle(new Point(-10, -10), size));
        }

        [Test]
        public void CountPointsDoesNotContradict_AfterAdding()
        {
            foreach (var point in points)
                storage.AddRectangle(new Rectangle(point, size));
            storage.Points.Count.Should().BeLessOrEqualTo(4 * storage.Rectangles.Count);
        }

        [Test]
        public void SetOfRectanglePointsAndPointsAreEqual_AfterAdding()
        {
            foreach (var point in points)
                storage.AddRectangle(new Rectangle(point, size));
            storage.Rectangles.SelectMany(rectangle => rectangle.GetPoints())
                .Distinct()
                .ShouldAllBeEquivalentTo(storage.Points);
        }
    }
}