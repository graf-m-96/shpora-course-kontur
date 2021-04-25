using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.settings;

namespace TagsCloudVisualization.auxiliary
{
    internal class WordSizeDeterminator
    {
        public Dictionary<string, int> WordsCoefficients;
        private readonly ImageSetting imageSetting;
        public readonly Dictionary<string, Font> WordToFont = new Dictionary<string, Font>();

        public WordSizeDeterminator(Dictionary<string, int> wordsCoefficients,
            ImageSetting imageSetting)
        {
            WordsCoefficients = wordsCoefficients;
            this.imageSetting = imageSetting;
            var wordsToFactor = SetFactorForPopularWords();
            foreach (var word in wordsToFactor.Keys)
                WordToFont[word] = new Font(imageSetting.FontFamily,
                    imageSetting.FontSize * wordsToFactor[word]);
        }

        public Size WordToSize(string word)
        {
            var textSize = TextRenderer.MeasureText(word, WordToFont[word]);
            var tagFullSize = new Size(textSize.Width + imageSetting.WordsPadding,
                textSize.Height + imageSetting.WordsPadding);
            return tagFullSize;
        }

        private Dictionary<string, float> SetFactorForPopularWords()
        {
            var wordToFactor = new Dictionary<string, float>();
            float currentFactor = 3;
            const float minFactor = 0.5f;
            const float reductionStepFactor = 0.2f;
            foreach (var pair in WordsCoefficients.OrderByDescending(pair => pair.Value))
            {
                wordToFactor[pair.Key] = currentFactor;
                if (currentFactor > minFactor)
                    currentFactor -= reductionStepFactor;
            }
            return wordToFactor;
        }
    }
}