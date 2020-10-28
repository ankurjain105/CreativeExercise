using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using StringCalculator.Application.Exceptions;

namespace StringCalculator.Application.UnitTests
{
    public class StringToNumberParserTests
    {
        private StringToNumberParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new StringToNumberParser();
        }

        [Test]
        public void EmptyString_ReturnsEmptyList()
        {
            var result = _parser.Parse(String.Empty);
            result.Should().BeEmpty();
        }

        [Test]
        public void SingleItem_ReturnsCorrectList()
        {
            var result = _parser.Parse("1");
            result.Should().BeEquivalentTo(new int[] { 1 });
        }

        [Test]
        public void MultipleItems_ReturnsCorrectList()
        {
            var result = _parser.Parse("1,2");
            result.Should().BeEquivalentTo(new int[] { 1, 2 });
        }

        [Test]
        public void MultipleItems_DifferentDefaultDelimiters_ReturnsCorrectList()
        {
            var result = _parser.Parse("1\n2,3");
            result.Should().BeEquivalentTo(new int[] { 1, 2, 3 });
        }

        [Test]
        public void MultipleItems_WithTwoConsecutiveDelimiters_ThrowsException()
        {
            _parser.Invoking(y => y.Parse("1,\n2").ToList())
                .Should()
                .Throw<ParseException>()
                .WithMessage("Consecutive Delimiters not allowed.");
        }

        [Test]
        public void NonDefaultDelimiter_ReturnsCorrectList()
        {
            var result = _parser.Parse("//#\n-1#0#-9");
            result.Should().BeEquivalentTo(new int[] { -1, 0, -9 });
        }

        [Test]
        public void NonDefaultMultipleDelimiters_ReturnsCorrectList()
        {
            var result = _parser.Parse("//*%\n1*2%3");
            result.Should().BeEquivalentTo(new int[] {1, 2, 3});
        }
    }
}