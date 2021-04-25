using System;
using System.Globalization;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    [TestFixture]
    public class ObjectPrinter_SetSyntax
    {
        [Test]
        public void DescribeSyntacticPossibilities_WithoutSemantics()
        {
            var printer = ObjectPrinter.For<Person>()
                //1. Исключить из сериализации свойства определенного типа
                .Excluding<Guid>()
                //2.Указать альтернативный способ сериализации для определенного типа
                .Printing<string>().Using(message => message + "\nAM serialization")
                //3. Для числовых типов указать культуру
                .Printing<int>().SetCulture(CultureInfo.CurrentCulture)
                //.Printing<string>().SetCulture(CultureInfo.CurrentCulture) //SetCulture не должен быть виден у <string>
                //4. Настроить сериализацию конкретного свойства
                .Printing(p => p.Name).Using(field => field.ToString() + "\nAM serialization")
                //5. Настроить обрезание строковых свойств (метод должен быть виден только для строковых свойств)
                .Printing<string>().Cut(message => message.Length >= 7 ? message.Substring(0, 7) : message)
                //.Printing<int>().Cut(number => number.ToString()) // Cut не должен быть виден не у <string>
                //6. Исключить из сериализации конкретного свойства
                .Excluding(p => p.Age);
            var person = new Person {Name = "Alex", Age = 20, Height = 2};
            var s1 = printer.PrintToString(person);

            //7. Синтаксический сахар в виде метода расширения, сериализующего по-умолчанию	
            var serializedPerson = person.PrintToString();
            //8. ...с конфигурированием
            var serializedPersonWithConfig = person.PrintToString(s => s.Excluding(p => p.Age));
        }
    }
}