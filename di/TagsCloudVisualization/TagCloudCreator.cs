using System.Drawing;
using System.IO;
using System.Linq;
using Castle.Core.Internal;
using TagsCloudVisualization.auxiliary;
using TagsCloudVisualization.cloudLayouter;
using TagsCloudVisualization.settings;

namespace TagsCloudVisualization
{
    public class TagCloudCreator
    {
        private readonly PathSetting pathSetting;
        private readonly ImageSetting imageSetting;
        private readonly CloudWordSetting cloudWordSetting;
        private readonly ICloudLayouter cloudLayoter;

        public TagCloudCreator(PathSetting pathSetting, ImageSetting imageSetting,
            CloudWordSetting cloudWordSetting, ICloudLayouter cloudLayoter)
        {
            this.pathSetting = pathSetting;
            this.imageSetting = imageSetting;
            this.cloudWordSetting = cloudWordSetting;
            this.cloudLayoter = cloudLayoter;
        }

        public void Run()
        {
            var words = cloudWordSetting.Reader.ReadAll(cloudWordSetting.Separators);
            var wordsToCount = cloudWordSetting.SetterWordCoefficient.GetWordsCoefficient(words,
                cloudWordSetting.ExcludedWords);
            var wordSizeDeterminator = new WordSizeDeterminator(wordsToCount, imageSetting);
            var tags = wordsToCount.Keys.Select(word =>
            {
                var tagFullSize = wordSizeDeterminator.WordToSize(word);
                var rectangle = cloudLayoter.PutNextRectangle(tagFullSize);
                return new Tag(word, new Point(rectangle.X, rectangle.Y));
            });
            var drawer = new Drawer(tags, imageSetting.ImageSize, imageSetting.FontColor,
                wordSizeDeterminator.WordToFont);
            var fullImageName = pathSetting.PicturesDirectory.IsNullOrEmpty()
                ? $"{pathSetting.ImageName}.{imageSetting.ImageFormat.ToString().ToLower()}"
                : $"{pathSetting.PicturesDirectory}{Path.DirectorySeparatorChar}" +
                  $"{pathSetting.ImageName}.{imageSetting.ImageFormat.ToString().ToLower()}";
            drawer.Bitmap.Save(fullImageName, imageSetting.ImageFormat);
        }
    }
}