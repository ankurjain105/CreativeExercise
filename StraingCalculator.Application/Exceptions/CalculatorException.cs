using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator.Application.Exceptions
{
    public class CalculatorException : Exception
    {
        public CalculatorException(string message)
            : base(message)
        {

        }

        public CalculatorException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
