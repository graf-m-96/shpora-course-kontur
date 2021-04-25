using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class Program
    {
        private const string Path = "../../pictures/";

        public static void Main()
        {
            var center = new Point(500, 500);
            {
                var cloudLayouter = new CircularCloudLayouter(center);
                for (var i = 0; i < 32; i++)
                    cloudLayouter.PutNextRectangle(new Size(150, 150));
                var drawer = new Drawer(center, new Size(2 * center.X, 2 * center.Y),
                    cloudLayouter.GetAllRectangles().ToArray());
                drawer.Bitmap.Save($"{Path}_tag_cloud1.png");
            }

            {
                var cloudLayouter = new CircularCloudLayouter(center);
                for (var i = 0; i < 30; i++)
                {
                    cloudLayouter.PutNextRectangle(new Size(70, 90));
                    cloudLayouter.PutNextRectangle(new Size(100, 80));
                }
                var drawer = new Drawer(center, new Size(2 * center.X, 2 * center.Y),
                    cloudLayouter.GetAllRectangles().ToArray());
                drawer.Bitmap.Save($"{Path}_tag_cloud2.png");
            }

            {
                var cloudLayouter = new CircularCloudLayouter(center);
                for (var i = 0; i < 10; i++)
                {
                    cloudLayouter.PutNextRectangle(new Size(60, 130));
                    cloudLayouter.PutNextRectangle(new Size(100, 80));
                    cloudLayouter.PutNextRectangle(new Size(150, 150));
                }
                var drawer = new Drawer(center, new Size(2 * center.X, 2 * center.Y),
                    cloudLayouter.GetAllRectangles().ToArray());
                drawer.Bitmap.Save($"{Path}_tag_cloud3.png");
            }
        }
    }
}