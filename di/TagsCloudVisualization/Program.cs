using TagsCloudVisualization.clients;
using TagsCloudVisualization.settings;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var client = new ConsoleClient();
            var options = client.GetOptions();
//            var options = new Options(); // Значения по умолчанию
            var container = new ContainerCustomizer().GetContainer(options);
            var tagCloudCreator = container.Resolve<TagCloudCreator>();
            tagCloudCreator.Run();
        }
    }
}