using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloudVisualization.calculaterWodsCoefficients;
using TagsCloudVisualization.clients;
using TagsCloudVisualization.cloudLayouter;
using TagsCloudVisualization.readers;

namespace TagsCloudVisualization.settings
{
    internal class ContainerCustomizer
    {
        private const string PicturesDirectory = "../../pictures";
        private const string ImageName = "tag_cloud";

        private readonly Dictionary<string, Func<string, IFileReader>> expansionToReader =
            new Dictionary<string, Func<string, IFileReader>>
            {
                {"txt", path => new TxtReader(path)},
                {"doc", path => new DocxReader(path)},
                {"docx", path => new DocxReader(path)}
            };

        public IWindsorContainer GetContainer(Options options)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ImageFormat>().Instance(options.ImageType));
            container.Register(Component.For<Brush>().Instance(options.FontColor));
            container.Register(Component.For<FontFamily>().Instance(options.FamilyOfFont));
            container.Register(Component.For<ImageSetting>().ImplementedBy<ImageSetting>()
                .DependsOn(Dependency.OnValue("WordsPadding", 5))
                .DependsOn(Dependency.OnValue("FontSize", options.FontSize))
                .DependsOn(Dependency.OnValue("ImageSize", options.ImageSize)));
            var expansion = options.PathToText.Split('.').Last();
            container.Register(Component.For<IFileReader>().Instance(
                expansionToReader[expansion](options.PathToText)));
            container.Register(Component.For<ISetterWordCoefficient>()
                .ImplementedBy<QuantitativeWordCoefficient>());
            container.Register(Component.For<char[]>().Instance(options.WordsSeparators));
            container.Register(Component.For<IEnumerable<string>>().Instance(options.BoringWords));
            container.Register(Component.For<CloudWordSetting>().ImplementedBy<CloudWordSetting>());
            container.Register(Component.For<PathSetting>()
                .Instance(new PathSetting(PicturesDirectory, ImageName)));
            var imageSize = options.ImageSize;
            var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
//            container.Register(Component.For<ICloudLayouter>().Instance(new CircularCloudLayouter(center)));
            container.Register(Component.For<ICloudLayouter>().ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue(typeof(Point), center)));
            container.Register(Component.For<TagCloudCreator>().ImplementedBy<TagCloudCreator>());
            return container;
        }
    }
}