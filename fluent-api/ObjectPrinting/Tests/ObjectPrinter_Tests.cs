using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    [TestFixture]
    internal class ObjectPrinter_Tests
    {
        /*
         * ПОРЯДОК НЕ УЧИТЫВАЕТСЯ:
         * GetProperties Метод не возвращает свойства определенного порядка,
         * например алфавитного или в порядке объявления.
         * Код не должен зависеть от порядка, в котором возвращаются свойства,
         * так как этот порядок меняется.
         */

        [SetUp]
        public void SetUp()
        {
            person = new Person
            {
                Name = "Alex",
                Age = 20,
                Height = 2,
                Chief = new Person {Name = "Bob", Age = 45, Height = 1.9}
            };
            defaultPrintingConfig = ObjectPrinter.For<Person>().Excluding(p => p.Chief);
        }

        private Person person;
        private PrintingConfig<Person> defaultPrintingConfig;

        [Test]
        public void DoSomething_WhenSomething()
        {
        }

        [Test]
        public void PrintToString_ChangeSerializePropertyName()
        {
            var expected = $"Person{Environment.NewLine}" +
                           $"\tId = Guid{Environment.NewLine}" +
                           $"\tName = Super Alex{Environment.NewLine}" +
                           $"\tHeight = 2{Environment.NewLine}" +
                           $"\tAge = 20{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Printing(p => p.Name)
                    .Using(name => $"Super {name}"),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_ChangeSerializeString()
        {
            var expected = $"Person{Environment.NewLine}" +
                           $"\tId = Guid{Environment.NewLine}" +
                           $"\tName = Some ALEX{Environment.NewLine}" +
                           $"\tHeight = 2{Environment.NewLine}" +
                           $"\tAge = 20{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Printing<string>()
                    .Using(str => $"Some {str.ToUpper()}")
                    .Excluding(p => p.Chief),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_CombineExcludeAndChangeSerializePropertiesAndTypes()
        {
            person.Height = 1.84;
            var expected = $"Person{Environment.NewLine}" +
                           $"\tName = Super Alex{Environment.NewLine}" +
                           $"\tHeight = 184 in centimeters{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Excluding<Guid>()
                    .Excluding(p => p.Age)
                    .Printing(p => p.Name)
                    .Using(name => $"Super {name}")
                    .Printing<double>()
                    .Using(height => $"{100 * height} in centimeters"),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_Cut()
        {
            person.Name = "Some long long name";
            var expected = $"Person{Environment.NewLine}" +
                           $"\tId = Guid{Environment.NewLine}" +
                           $"\tName = Some long{Environment.NewLine}" +
                           $"\tHeight = 2{Environment.NewLine}" +
                           $"\tAge = 20{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Printing<string>()
                    .Cut(message => message.Length >= 9 ? message.Substring(0, 9) : message),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_ExcludeAllProperty()
        {
            var expected = $"Person{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Excluding(p => p.Id)
                    .Excluding(p => p.Age)
                    .Excluding(p => p.Height)
                    .Excluding(p => p.Name),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_ExcludePropertyId()
        {
            var expected = $"Person{Environment.NewLine}" +
                           $"\tName = Alex{Environment.NewLine}" +
                           $"\tHeight = 2{Environment.NewLine}" +
                           $"\tAge = 20{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Excluding(p => p.Id),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_ExcludeTypeGuid()
        {
            var expected = $"Person{Environment.NewLine}" +
                           $"\tName = Alex{Environment.NewLine}" +
                           $"\tHeight = 2{Environment.NewLine}" +
                           $"\tAge = 20{Environment.NewLine}";

            var actual = person.PrintToString(s => s.Excluding<Guid>(), defaultPrintingConfig);

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_ExludeTypeAll()
        {
            var expected = $"Person{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Excluding<Guid>()
                    .Excluding<int>()
                    .Excluding<double>()
                    .Excluding<string>(),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_SetCultureInvariant()
        {
            person.Height = 1.84;
            var expected = $"Person{Environment.NewLine}" +
                           $"\tId = Guid{Environment.NewLine}" +
                           $"\tName = Alex{Environment.NewLine}" +
                           $"\tHeight = 1.84{Environment.NewLine}" +
                           $"\tAge = 20{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                    .Printing<double>()
                    .SetCulture(CultureInfo.InvariantCulture),
                defaultPrintingConfig
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_WithNestedComplexClass()
        {
            var expected = $"Person{Environment.NewLine}" +
                           $"\tName = Alex{Environment.NewLine}" +
                           $"\tChief = Person{Environment.NewLine}" +
                           $"\t\tName = Bob{Environment.NewLine}" +
                           $"\t\tChief = null{Environment.NewLine}";

            var actual = person.PrintToString(s => s
                .Excluding<Guid>()
                .Excluding<int>()
                .Excluding<double>()
            );

            actual.Should().Be(expected);
        }

        [Test]
        public void PrintToStringPerson_WithoutSetting()
        {
            var expected = $"Person{Environment.NewLine}" +
                           $"\tId = Guid{Environment.NewLine}" +
                           $"\tName = Alex{Environment.NewLine}" +
                           $"\tHeight = 2{Environment.NewLine}" +
                           $"\tAge = 20{Environment.NewLine}";

            var actual = person.PrintToString(defaultPrintingConfig);

            actual.Should().Be(expected);
        }
    }
}