using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using StringCalculator.Application.Exceptions;

namespace StringCalculator.Application.UnitTests
{
    public class StringCalculatorTests
    {
        private StringCalculator _calculator;
        private Moq.Mock<IStringToNumberParser> _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new Mock<IStringToNumberParser>();
            _calculator = new StringCalculator(_parser.Object);
        }

        [Test]
        public void EmptyList_ReturnsZero()
        {
            string input = "anyinput";
            _parser.Setup(x => x.Parse(input)).Returns((new int[] { }).AsEnumerable());

            var result = _calculator.Add(input);
            result.Should().Be(0);
        }

        [Test]
        public void NonEmptyList_ReturnsSumOfNos()
        {
            string input = "anyinput";
            _parser.Setup(x => x.Parse(input)).Returns((new int[] {1, 2}).AsEnumerable());

            var result = _calculator.Add(input);
            result.Should().Be(3);
        }

        [Test]
        public void NumbersGreaterThan1000_AreIgnored()
        {
            string input = "anyinput";
            _parser.Setup(x => x.Parse(input)).Returns((new int[] { 1, 2, 1001, 5 }).AsEnumerable());

            var result = _calculator.Add(input);
            result.Should().Be(8);
        }

        [Test]
        public void NegativeNumbers_ThrowException()
        {
            string input = "anyinput";
            _parser.Setup(x => x.Parse(input)).Returns((new int[] { 1, 2, -10, 5, -20 }).AsEnumerable());

            _parser.Invoking(y => _calculator.Add(input))
                .Should()
                .Throw<CalculatorException>()
                .WithMessage("Negatives not allowed - -10,-20");
        }
    }
}
