using System;
using static System.Console;

namespace builder
{
    class Client
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            IComputerBuilder builder1 = new LowPriceComputerBuilder();
            IComputerBuilder builder2 = new MiddlePriseComputerBuilder();
            IComputerBuilder builder3 = new HighPriseComputerBuilder();

            WriteLine("Select the range you are interested in:\n"+
                "\ta. 3 000 - 10 000 ₴\n\tb. 10 000 - 25 000 ₴\n\tc. 25 000 - 60 000 ₴");
            switch (ReadLine())
            {
                case "a":
                    director.buildComputer(builder1);
                    break;
                case "b":
                    director.buildComputer(builder2);
                    break;
                case "c":
                    director.buildComputer(builder3);
                    break;
                default:
                    WriteLine("Input Error.");
                    break;
            }
        }
    }
}
