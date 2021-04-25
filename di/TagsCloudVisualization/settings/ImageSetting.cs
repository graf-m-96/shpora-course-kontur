using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.settings
{
    public class ImageSetting
    {
        public readonly int WordsPadding;
        public readonly Size ImageSize;
        public float FontSize;
        public ImageFormat ImageFormat;
        public Brush FontColor;
        public FontFamily FontFamily;

        public ImageSetting(int wordsPadding, Size imageSize, float fontSize, ImageFormat imageFormat,
            Brush fontColor, FontFamily fontFamily)
        {
            WordsPadding = wordsPadding;
            ImageSize = imageSize;
            FontSize = fontSize;
            ImageFormat = imageFormat;
            FontColor = fontColor;
            FontFamily = fontFamily;
        }
    }
}