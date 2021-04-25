using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.clients
{
    public class Options
    {
        public string PathToText = "../../text/words.txt";
        public char[] WordsSeparators = {'\n', ' ', '.', ',', '!', '?', ':', ';', '-'};

        public string[] BoringWords = new string[0];
        public float FontSize = 25;
        public Size ImageSize = new Size(1000, 1000);

        public readonly Dictionary<string, ImageFormat> PossibleImageTypes =
            new Dictionary<string, ImageFormat>
            {
                {"png", ImageFormat.Png},
                {"jpeg", ImageFormat.Jpeg},
                {"bmp", ImageFormat.Bmp}
            };

        public ImageFormat ImageType = ImageFormat.Png;

        public readonly Dictionary<string, FontFamily> PossibleFontFamilies =
            new Dictionary<string, FontFamily>
            {
                {"Arial", new FontFamily("Arial")},
                {"Times New Roman", new FontFamily("Times New Roman")},
                {"Comic Sans MS", new FontFamily("Comic Sans MS")}
            };

        public FontFamily FamilyOfFont = new FontFamily("Comic Sans MS");

        public readonly Dictionary<string, Brush> PossibleFontColors =
            new Dictionary<string, Brush>
            {
                {"black", Brushes.Black},
                {"blue", Brushes.Blue},
                {"blueViolet", Brushes.BlueViolet}
            };

        public Brush FontColor = Brushes.Black;
    }
}