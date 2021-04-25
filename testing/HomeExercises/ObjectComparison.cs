using System;
using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	public class ObjectComparison
	{
		[Test]
		[Description("Проверка текущего царя")]
		[Category("ToRefactor")]
		public void CheckCurrentTsar()
		{
            var actualTsar = TsarRegistry.GetCurrentTsar();

            var expectedTsar = new Person("Ivan IV The Terrible", 54, 170, 70,
                new Person("Vasili III of Russia", 28, 170, 60, null));
            // Данный тест переписан на библиотеку Fluent в тесте CheckCurrentTsarAndHisFather_Fluent
            #region 
            // Перепишите код на использование Fluent Assertions.
            Assert.AreEqual(actualTsar.Name, expectedTsar.Name);
			Assert.AreEqual(actualTsar.Age, expectedTsar.Age);
			Assert.AreEqual(actualTsar.Height, expectedTsar.Height);
			Assert.AreEqual(actualTsar.Weight, expectedTsar.Weight);

			Assert.AreEqual(expectedTsar.Parent.Name, actualTsar.Parent.Name);
			Assert.AreEqual(expectedTsar.Parent.Age, actualTsar.Parent.Age);
			Assert.AreEqual(expectedTsar.Parent.Height, actualTsar.Parent.Height);
			Assert.AreEqual(expectedTsar.Parent.Parent, actualTsar.Parent.Parent);
            #endregion
        }

	    [Test]
	    public void CheckCurrentTsarAndHisFather_Fluent()
	    {
            /*
             * CheckCurrentTsarAndHisFather_Fluent(1) лучше CheckCurrentTsar_WithCustomEquality(2)
             * по следующим критериям:
             * 1) В случае ошибки в тесте (2) вернётся только сообщение о том, что
             * тест не был пройден. А что не сошлось - нет
             * 2) Во (2) тесте используется метод, который принимает 2 объекта одного типа.
             * Так как документацию часто не читают, то есть немалая вероятность ошибиться,
             * передав аргументы не в том порядке
             * 3) (1) написан с помощью библиотеки FluentAssertions, которая описывает тесты
             * с помощью функций, работу которых легко понять, не читая перед этим документацию
             * 
             */
            var actualTsar = TsarRegistry.GetCurrentTsar();
	        var expectedTsar = new Person("Ivan IV The Terrible", 54, 170, 70,
	            new Person("Vasili III of Russia", 28, 170, 60, null));
            const int depthOfRelative = 1;
	        for (var i = 0; i <= depthOfRelative; i++)
	        {
                if (actualTsar == null && expectedTsar == null)
                    break;
	            actualTsar.Should().NotBeNull("pedigrees do not match");
	            expectedTsar.Should().NotBeNull("pedigrees do not match");
                actualTsar.ShouldBeEquivalentTo(expectedTsar, options => options
	                .ExcludingProperties()
	                .Including(fields => fields.Name)
	                .Including(fields => fields.Age)
	                .Including(fields => fields.Height)
	                .Including(fields => fields.Weight)
	            );
	            actualTsar = actualTsar.Parent;
	            expectedTsar = expectedTsar.Parent;
	        }
        }


        [Test]
		[Description("Альтернативное решение. Какие у него недостатки?")]
		public void CheckCurrentTsar_WithCustomEquality()
		{
			var actualTsar = TsarRegistry.GetCurrentTsar();
			var expectedTsar = new Person("Ivan IV The Terrible", 54, 170, 70,
			new Person("Vasili III of Russia", 28, 170, 60, null));

			// Какие недостатки у такого подхода? 
			Assert.True(AreEqual(actualTsar, expectedTsar));

		}

		private bool AreEqual(Person actual, Person expected)
		{
			if (actual == expected) return true;
			if (actual == null || expected == null) return false;
            return
            actual.Name == expected.Name
            && actual.Age == expected.Age
            && actual.Height == expected.Height
            && actual.Weight == expected.Weight
			&& AreEqual(actual.Parent, expected.Parent);
		}
	}


	public class TsarRegistry
	{
		public static Person GetCurrentTsar()
		{
			return new Person(
				"Ivan IV The Terrible", 54, 170, 70,
				new Person("Vasili III of Russia", 28, 170, 60, null));
		}
	}

	public class Person
	{
		public static int IdCounter = 0;
		public int Age, Height, Weight;
		public string Name;
		public Person Parent;
		public int Id;

		public Person(string name, int age, int height, int weight, Person parent)
		{
			Id = IdCounter++;
			Name = name;
			Age = age;
			Height = height;
			Weight = weight;
			Parent = parent;
		}
	}
}
