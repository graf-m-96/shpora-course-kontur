using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class PointExtention
    {
        public static double GetDistance(this Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}