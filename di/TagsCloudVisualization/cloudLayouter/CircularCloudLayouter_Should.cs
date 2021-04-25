using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.auxiliary;

namespace TagsCloudVisualization.cloudLayouter
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter cloud;

        [SetUp]
        public void SetUp()
        {
            cloud = new CircularCloudLayouter(new Point(5, 5));
        }

        [Test]
        public void HaveZeroRectangles_BeforeAdd()
        {
            cloud.GetAllRectangles().Count().Should().Be(0);
        }

        [Test]
        public void OneRectangleIsBorderWithCenter_AfterAddingOneRectangle()
        {
            cloud.PutNextRectangle(new Size(10, 10));
            cloud.GetAllRectangles().ToArray()[0].Location.Should().Be(new Point(5, 5));
        }

        [Test]
        public void FourRectanglesAreBorderedWithCenter_AfterAdditionFour()
        {
            for (var i = 0; i < 4; i++)
                cloud.PutNextRectangle(new Size(10, 10));
            cloud.GetAllRectangles().SelectMany(rectangle => rectangle.GetPoints())
                .Count(point => point.Equals(cloud.GetCenter()))
                .Should().Be(4);
        }

        [Test]
        public void NCountRectangles_AfterAdditionN()
        {
            for (var i = 0; i < 20; i++)
                cloud.PutNextRectangle(new Size(10, 10));
            cloud.GetAllRectangles().Count().Should().Be(20);
        }

        [Test]
        public void FourRectanglesCrossCenter_AfterAddingMoreThenFour()
        {
            for (var i = 0; i < 10; i++)
                cloud.PutNextRectangle(new Size(10, 10));
            cloud.GetAllRectangles().SelectMany(rectangle => rectangle.GetPoints())
                .Where(point => cloud.GetCenter().Equals(point))
                .ToArray().Length.Should().Be(4);
        }
    }
}