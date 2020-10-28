using System;
using Autofac;
using StringCalculator.Application;

namespace StringCalculator.Host
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            BuildContainer();

            Console.WriteLine("Enter string to process");
            var input = Console.ReadLine();
            var calculator = (ICalculator)Container.Resolve(typeof(ICalculator));
            var result = calculator.Add(input);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<StringToNumberParser>().As<IStringToNumberParser>();
            builder.RegisterType<Application.StringCalculator>().As<ICalculator>();
            Container = builder.Build();

            return Container;
        }
    }
}
