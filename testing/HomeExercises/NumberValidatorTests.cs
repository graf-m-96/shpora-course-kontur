using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	public class NumberValidatorTests
	{
		[Test]
		public void Test()
		{
            // Данный тест переписан на библиотеку Fluent в тестах, начинающихся с
            // 1)NumberValidator
            // 2)IsValidNumber

            Assert.Throws<ArgumentException>(() => new NumberValidator(-1, 2, true));
			Assert.DoesNotThrow(() => new NumberValidator(1, 0, true));
			Assert.Throws<ArgumentException>(() => new NumberValidator(-1, 2, false));
			Assert.DoesNotThrow(() => new NumberValidator(1, 0, true));

			Assert.IsTrue(new NumberValidator(17, 2, true).IsValidNumber("0.0"));
			Assert.IsTrue(new NumberValidator(17, 2, true).IsValidNumber("0"));
			Assert.IsTrue(new NumberValidator(17, 2, true).IsValidNumber("0.0"));
			Assert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("00.00"));
			Assert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("-0.00"));
			Assert.IsTrue(new NumberValidator(17, 2, true).IsValidNumber("0.0"));
			Assert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("+0.00"));
			Assert.IsTrue(new NumberValidator(4, 2, true).IsValidNumber("+1.23"));
			Assert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("+1.23"));
			Assert.IsFalse(new NumberValidator(17, 2, true).IsValidNumber("0.000"));
			Assert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("-1.23"));
			Assert.IsFalse(new NumberValidator(3, 2, true).IsValidNumber("a.sd"));
		}

	    [Test]
	    public void NumberValidator_ArgumentException_PrecisionNonpositive()
	    {
	        Action create = () => new NumberValidator(-1, 2, true);
	        create.ShouldThrow<ArgumentException>("precision must be a positive number");
	    }

	    [Test]
	    public void NumberValidator_ArgumentException_ScaleNegative()
	    {
	        Action create = () => new NumberValidator(2, -1, false);
	        create.ShouldThrow<ArgumentException>("precision must be a non-negative number");
        }

	    [Test]
	    public void NumberValidator_ArgumentException_ScaleMoreOrEqualPrecison()
	    {
	        Action create = () => new NumberValidator(2, 3, true);
            create.ShouldThrow<ArgumentException>("scale must be less or equal than precision");
        }

	    [Test]
	    public void NumberValidator_NotExceptions_PrecisionPositiveAndScaleNonNegativeAndNotMorePrecision()
	    {
	        Action create = () => new NumberValidator(1, 0, true);
	        create.ShouldNotThrow("not exceptions");
	    }

        [Test]
        public void IsValidNumber_InvalidOnNullOrEmptyValue_CorrectNumberValidator()
        {
            new NumberValidator(17, 2, true).IsValidNumber(String.Empty).Should().Be(false);
        }

	    [Test]
	    public void IsValidNumber_InvalidOnMatch_CorrectNumberValidator()
	    {
	        var noNumber = "itIsNotNumber";
	        new NumberValidator(17, 2, true).IsValidNumber(noNumber).Should().Be(false);
        }

	    [Test]
	    public void IsValidNumber_InvalidnOnLengthMoreThanPrecision_CorrectNumberValidator()
	    {
	        const string number = "+12345.12345";
	        new NumberValidator(9, 6, true).IsValidNumber(number).Should().Be(false);
        }

	    [Test]
	    public void IsValidNumber_InvalidOnLengthFracPartMoreThanScale_CorrectNumberValidator()
	    {
	        const string number = "+12345.12345";
	        new NumberValidator(13, 4, true).IsValidNumber(number).Should().Be(false);
        }

        [Test]
	    public void IsValidNumber_InvalidOnSignAndFlagSign_CorrectNumberValidator()
	    {
	        const string number = "-1.1";
	        new NumberValidator(3, 2, true).IsValidNumber(number).Should().Be(false);
	    }

	    [Test]
	    public void IsValidNumber_ValidOnCorrectNumber_CorrectNumberValidator()
	    {
	        const string number = "+123.123";
	        new NumberValidator(7, 4, true).IsValidNumber(number).Should().Be(true);

        }
    }

	public class NumberValidator
	{
		private readonly Regex numberRegex;
		private readonly bool onlyPositive;
		private readonly int precision;
		private readonly int scale;

		public NumberValidator(int precision, int scale = 0, bool onlyPositive = false)
		{
			this.precision = precision;
			this.scale = scale;
			this.onlyPositive = onlyPositive;
			if (precision <= 0)
				throw new ArgumentException("precision must be a positive number");
			if (scale < 0 || scale >= precision)
				throw new ArgumentException("precision must be a non-negative number less or equal than precision");
			numberRegex = new Regex(@"^([+-]?)(\d+)([.,](\d+))?$", RegexOptions.IgnoreCase);
		}

		public bool IsValidNumber(string value)
		{
			// Проверяем соответствие входного значения формату N(m,k), в соответствии с правилом, 
			// описанным в Формате описи документов, направляемых в налоговый орган в электронном виде по телекоммуникационным каналам связи:
			// Формат числового значения указывается в виде N(m.к), где m – максимальное количество знаков в числе, включая знак (для отрицательного числа), 
			// целую и дробную часть числа без разделяющей десятичной точки, k – максимальное число знаков дробной части числа. 
			// Если число знаков дробной части числа равно 0 (т.е. число целое), то формат числового значения имеет вид N(m).

			if (string.IsNullOrEmpty(value))
				return false;

			var match = numberRegex.Match(value);
			if (!match.Success)
				return false;

			// Знак и целая часть
			var intPart = match.Groups[1].Value.Length + match.Groups[2].Value.Length;
			// Дробная часть
			var fracPart = match.Groups[4].Value.Length;

			if (intPart + fracPart > precision || fracPart > scale)
				return false;

			if (onlyPositive && match.Groups[1].Value == "-")
				return false;
			return true;
		}
	}
}