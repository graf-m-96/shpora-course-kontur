using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.calculaterWodsCoefficients;
using TagsCloudVisualization.cloudLayouter;
using TagsCloudVisualization.readers;
using TagsCloudVisualization.settings;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class TagCloudCreator_Should
    {
        private readonly PathSetting pathSetting = new PathSetting("", "check_tag_cloud");

        private readonly ImageSetting imageSetting = new ImageSetting(5, new Size(500, 500), 30,
            ImageFormat.Jpeg, Brushes.BlueViolet, new FontFamily("Comic Sans MS"));

        private CloudWordSetting cloudWordSetting;
        private IFileReader iFileReader;
        private ISetterWordCoefficient iSetterWordCoefficient;
        private ICloudLayouter cloudLayoter;
        private TagCloudCreator tagCloudCreator;
        private IEnumerable<string> words;
        private IEnumerable<string> excludedWords;

        private Dictionary<string, int> wordToCount;

        private char[] separators;

        [SetUp]
        public void SetUp()
        {
            words = new[] {"lala", "lala", "lala", "lylyly", "lylyly", "c#"};
            wordToCount = new Dictionary<string, int>
            {
                {"lala", 3},
                {"lylyly", 2},
                {"c#", 1}
            };
            excludedWords = new string[0];
            separators = new[] {' ', '\n'};
            MakeDefaultSetting();
        }

        [Test]
        public void OnceRun_ReaderOnceReadAll()
        {
            tagCloudCreator.Run();
            iFileReader.Received(1).ReadAll(Arg.Any<char[]>());
        }

        [Test]
        public void OnceRun_OnceGetWordsToCoefficient()
        {
            tagCloudCreator.Run();
            iSetterWordCoefficient.Received(1).GetWordsCoefficient(Arg.Any<IEnumerable<string>>(),
                Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void FewRun_ReaderFewReadAll()
        {
            const int count = 6;
            for (var i = 0; i < count; i++)
                tagCloudCreator.Run();
            iFileReader.Received(count).ReadAll(Arg.Any<char[]>());
        }

        [Test]
        public void FewRun_FewGetWordsToCoefficient()
        {
            const int count = 8;
            for (var i = 0; i < count; i++)
                tagCloudCreator.Run();
            iSetterWordCoefficient.Received(count).GetWordsCoefficient(Arg.Any<IEnumerable<string>>(),
                Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void OnceRun_NoRectagnleAdding_WhenNoWords()
        {
            words = new string[0];
            wordToCount = new Dictionary<string, int>();
            MakeDefaultSetting();
            tagCloudCreator.Run();
            iSetterWordCoefficient.GetWordsCoefficient(words, excludedWords)
                .Returns(wordToCount);
        }

        [Test]
        public void OnceRun_CorrectPutRectangleCount()
        {
            tagCloudCreator.Run();
            cloudLayoter.Received(3).PutNextRectangle(Arg.Any<Size>());
        }

        [Test]
        public void FewRun_CorrectPutRectangleCount()
        {
            const int runCount = 4;
            for (var i = 0; i < runCount; i++)
            {
                tagCloudCreator.Run();
                for (var j = 0; j < wordToCount.Count; j++)
                    cloudLayoter.PutNextRectangle(Arg.Any<Size>());
            }
            cloudLayoter.Received(12).PutNextRectangle(Arg.Any<Size>());
        }

        [Test]
        public void OnceRun_CorrectPutRectangleCount_WithExcludedWords()
        {
            words = new[] {"a", "b", "c", "c", "d", "d", "d"};
            excludedWords = new[] {"c", "d"};
            wordToCount = new Dictionary<string, int>
            {
                {"a", 1},
                {"b", 1}
            };
            MakeDefaultSetting();
            tagCloudCreator.Run();
            cloudLayoter.Received(2).PutNextRectangle(Arg.Any<Size>());
        }

        [Test]
        public void FewRun_CorrectPutRectangleCount_WithExcludedWords()
        {
            words = new[] {"a", "b", "c", "c", "d", "d", "d"};
            excludedWords = new[] {"c", "d"};
            wordToCount = new Dictionary<string, int>
            {
                {"a", 1},
                {"b", 1}
            };
            MakeDefaultSetting();
            const int runCount = 4;
            for (var i = 0; i < runCount; i++)
            {
                tagCloudCreator.Run();
                for (var j = 0; j < wordToCount.Count; j++)
                    cloudLayoter.PutNextRectangle(Arg.Any<Size>());
            }
            cloudLayoter.Received(8).PutNextRectangle(Arg.Any<Size>());
        }

        private void MakeDefaultSetting()
        {
            iFileReader = Substitute.For<IFileReader>();
            iFileReader.ReadAll(separators).Returns(words);
            iSetterWordCoefficient = Substitute.For<ISetterWordCoefficient>();
            iSetterWordCoefficient.GetWordsCoefficient(words, excludedWords)
                .Returns(wordToCount);
            cloudWordSetting = new CloudWordSetting(iFileReader, iSetterWordCoefficient,
                excludedWords, separators);
            cloudLayoter = Substitute.For<ICloudLayouter>();
            tagCloudCreator = Substitute.For<TagCloudCreator>(pathSetting, imageSetting,
                cloudWordSetting, cloudLayoter);
        }
    }
}