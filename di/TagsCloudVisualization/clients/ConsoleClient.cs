using System;
using System.Drawing;
using System.Linq;
using Castle.Core.Internal;

namespace TagsCloudVisualization.clients
{
    internal class ConsoleClient : IClient
    {
        private readonly Options options;

        public ConsoleClient()
        {
            options = new Options();
        }

        public Options GetOptions()
        {
            GetPathToText();
            GetWordsSeparators();
            GetBoringWords();
            GetFontSize();
            GetImageSize();
            GetImageType();
            GetFontFamily();
            GetFontColor();
            return options;
        }

        private void GetWordsSeparators()
        {
            var separatorsForPrint = string.Join(" ",
                options.WordsSeparators.Select(chr => chr.ToString()));
            Console.WriteLine("Введите символы-разделители в тексте, не включая\n" +
                              "перевод строки и пробел\n" +
                              $"По умолчанию {separatorsForPrint}");
            var message = Console.ReadLine();
            var mandatorySeparators = new[] {'\n', ' '};
            if (!message.IsNullOrEmpty())
                options.WordsSeparators = message.Split().Select(char.Parse)
                    .Concat(mandatorySeparators).ToArray();
            Console.WriteLine();
        }

        private void GetPathToText()
        {
            Console.WriteLine("Введите путь к файлу, хранящему слова для облака тегов, записанные" +
                              "разделённые переводом строки\n" +
                              $"По умолчанию {options.PathToText}");
            var message = Console.ReadLine();
            if (!message.IsNullOrEmpty())
                options.PathToText = message;
            Console.WriteLine();
        }

        private void GetBoringWords()
        {
            Console.WriteLine("Введите \"скучные слова\" через запятую\n" +
                              "По умолчанию \"скучных слов нет\"");
            var message = Console.ReadLine();
            if (!message.IsNullOrEmpty())
                options.BoringWords = message.Split();
            Console.WriteLine();
        }

        private void GetFontSize()
        {
            Console.WriteLine("Введите размер шрифта\n" +
                              $"По умолчанию {options.FontSize}");
            try
            {
                var message = Console.ReadLine();
                if (!message.IsNullOrEmpty())
                    options.FontSize = float.Parse(message);
            }
            catch (FormatException error)
            {
                throw new FormatException("Размер шрифта - это число типа float", error);
            }
            Console.WriteLine();
        }

        private void GetImageSize()
        {
            Console.WriteLine("Введите размер изображения\n" +
                              $"По умолчанию width = {options.ImageSize.Width}, " +
                              $"height = {options.ImageSize.Height}");
            try
            {
                var message = Console.ReadLine();
                if (!message.IsNullOrEmpty())
                {
                    var array = message.Split();
                    options.ImageSize = new Size(int.Parse(array[0]), int.Parse(array[1]));
                }
            }
            catch (FormatException error)
            {
                throw new FormatException("Размер изображения - это 2 целых числа записанные через пробел",
                    error);
            }
        }

        private void GetImageType()
        {
            Console.WriteLine("Введите формат сохраняемого изображения:\n" +
                              $"{string.Join("\n", options.PossibleImageTypes.Keys)}\n" +
                              $"По умолчанию {options.ImageType}");
            var message = Console.ReadLine();
            if (!message.IsNullOrEmpty())
                options.ImageType = options.PossibleImageTypes[message];
            Console.WriteLine();
        }

        private void GetFontFamily()
        {
            Console.WriteLine("Введите формат сохраняемого изображения:\n" +
                              $"{string.Join("\n", options.PossibleFontFamilies.Keys)}\n" +
                              $"По умолчанию {options.FamilyOfFont.Name}");
            var message = Console.ReadLine();
            if (!message.IsNullOrEmpty())
                options.FamilyOfFont = options.PossibleFontFamilies[message];
            Console.WriteLine();
        }

        private void GetFontColor()
        {
            var defaultColor =
                options.PossibleFontColors.Keys
                    .First(key => options.PossibleFontColors[key] == options.FontColor);
            Console.WriteLine("Введите цвет шрифта:\n" +
                              $"{string.Join("\n", options.PossibleFontColors.Keys)}\n" +
                              $"По умолчанию {defaultColor}");
            var message = Console.ReadLine();
            if (!message.IsNullOrEmpty())
                options.FontColor = options.PossibleFontColors[message];
            Console.WriteLine();
        }
    }
}