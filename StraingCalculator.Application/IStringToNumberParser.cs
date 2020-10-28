using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator.Application
{
    public interface IStringToNumberParser
    {
        IEnumerable<int> Parse(string numbers);
    }
}
