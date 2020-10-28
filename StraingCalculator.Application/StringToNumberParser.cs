using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using StringCalculator.Application.Exceptions;

namespace StringCalculator.Application
{
    public class StringToNumberParser : IStringToNumberParser
    {
        private readonly char[] _defaultDelimiters = new[] {',', '\n'};
        public IEnumerable<int> Parse(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
            {
                yield break;
            }

            var delimiters = ParseDelimiters(numbers);

            string numbersPattern = @"^\/\/.*\n(?<numbers>.*)|^(?!\/\/)(?<numbers>(.|\n)*)$";
            var numbersRegex = new Regex(numbersPattern, RegexOptions.Multiline);
            var match = numbersRegex.Match(numbers);
            if (!match.Success && match.Groups.Count > 1 && !string.IsNullOrWhiteSpace(match.Groups["numbers"].Value))
            {
                yield break;
            }

            var split = match.Groups["numbers"].Value.Split(delimiters);
            foreach (var item in split)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    throw new ParseException("Consecutive Delimiters not allowed.");
                }

                if (!int.TryParse(item, out int number))
                {
                    throw new ApplicationException("");
                }

                yield return number;
            }
        }

        private char[] ParseDelimiters(string numbers)
        {
            string pattern = @"^\/\/(.*)\n";
            var matcher = new Regex(pattern);
            var match = matcher.Match(numbers);
            if (!match.Success || match.Groups.Count <= 1 || string.IsNullOrWhiteSpace(match.Groups[1].Value))
            {
                return _defaultDelimiters;
            }

            return match.Groups[1].Value.ToCharArray();
        }

        private string[] SplitString(string numbers, char[] delimiters)
        {
            return new[] {""};
        }
    }
}
