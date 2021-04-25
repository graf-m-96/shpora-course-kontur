using System.Drawing;

namespace TagsCloudVisualization
{
    public class Drawer
    {
        public Bitmap Bitmap { get; }

        public Drawer(Point center, Size size, params Rectangle[] rectangles)
        {
            Bitmap = new Bitmap(size.Width, size.Height);
            using (var graphics = Graphics.FromImage(Bitmap))
            {
                graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), size));
                var pen = new Pen(Color.Red);
                graphics.DrawRectangles(pen, rectangles);
                // выделяем центр
                pen.Width = 5;
                graphics.DrawEllipse(pen, new Rectangle(new Point(center.X - 3, center.Y - 3), new Size(6, 6)));
            }
        }
    }
}