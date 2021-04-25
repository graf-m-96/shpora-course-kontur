using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.cloudLayouter
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        Point GetCenter();
        IEnumerable<Rectangle> GetAllRectangles();
    }
}