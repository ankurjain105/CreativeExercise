using System;
using Autofac;
using NUnit.Framework;
using StringCalculator.Application;
using StringCalculator.Host;
using TechTalk.SpecFlow;
using StringCalculator = StringCalculator.Application.StringCalculator;

namespace StringCalculator.FunctionalTests.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly ICalculator _calculator;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext)
        {
            var container = Program.BuildContainer();
            _calculator = (ICalculator)container.Resolve(typeof(ICalculator));
            _scenarioContext = scenarioContext;
        }

        [Given("the numbers represented as \"(.*)\" and \"(.*)\"")]
        public void GivenTheFirstNumberIs(string input1, string input2)
        {
            var input = input1 + (string.IsNullOrWhiteSpace(input2) ? "" : ("\n" + input2));
            _scenarioContext.Clear();
            _scenarioContext.Add("input", input);
        }

        [When("calculator add method is invoked")]
        public void WhenTheTwoNumbersAreAdded()
        {
            try
            {
                var result = _calculator.Add(_scenarioContext.Get<string>("input"));
                _scenarioContext.Add("result", result);
            }
            catch (Exception ex)
            {
                _scenarioContext.Add("exception", ex);
            }
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            if (!_scenarioContext.ContainsKey("result"))
            {
                Assert.Fail("Result not returned. Exception - " + _scenarioContext.Get<Exception>("exception").ToString());
            }

            var actual = _scenarioContext.Get<int>("result");
            Assert.That(actual, Is.EqualTo(result));
        }

        [Then("exception should be thrown with message \"(.*)\"")]
        public void ThenExceptionShouldBeThrown(string message)
        {
            if (!_scenarioContext.ContainsKey("exception"))
            {
                Assert.Fail("Exception not thrown. Result - " + _scenarioContext.Get<int>("result").ToString());
            }

            var actual = _scenarioContext.Get<Exception>("exception");
            Assert.That(actual.Message, Is.EqualTo(message));
        }
    }
}
