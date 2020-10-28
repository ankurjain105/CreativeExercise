using System;
using System.Linq;
using StringCalculator.Application.Exceptions;

namespace StringCalculator.Application
{
    public class StringCalculator : ICalculator
    {
        private readonly IStringToNumberParser _parser;

        public StringCalculator(IStringToNumberParser parser)
        {
            _parser = parser;
        }

        public int Add(string numbers)
        {
            var nosToAdd = _parser.Parse(numbers).ToList();

            var negativeNumbers = nosToAdd.Where(x => x < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new CalculatorException("Negatives not allowed - " + string.Join(',', negativeNumbers));
            }

            return nosToAdd.Where(x => x <= 1000).Sum();
        }
    }
}
