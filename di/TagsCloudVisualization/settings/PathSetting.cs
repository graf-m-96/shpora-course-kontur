namespace TagsCloudVisualization.settings
{
    public class PathSetting
    {
        public readonly string PicturesDirectory;
        public readonly string ImageName;

        public PathSetting(string picturesDirectory, string imageName)
        {
            PicturesDirectory = picturesDirectory;
            ImageName = imageName;
        }
    }
}